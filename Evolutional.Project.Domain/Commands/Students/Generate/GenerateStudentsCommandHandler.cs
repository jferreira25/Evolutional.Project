using Evolutional.Project.Domain.Dto;
using Evolutional.Project.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Commands.Students.Generate
{
    public class GenerateStudentsCommandHandler : IRequestHandler<GenerateStudentsCommand, Unit>
    {
        private SemaphoreSlim semaphore;
        private List<string> lessonsNames = new List<string>() { "Matemática", "Português", "História", "Geografia", "Inglês", "Biologia", "Filosofia", "Física", "Química" };

        private readonly ILessonsRepository _lessonsRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly IStudentsLessonsProcedure _studentsLessonsProcedure;

        public GenerateStudentsCommandHandler(
             ILessonsRepository lessonsRepository,
             IStudentsRepository studentsRepository,
             IStudentsLessonsProcedure studentsLessonsProcedure)
        {
            _lessonsRepository = lessonsRepository;
            _studentsRepository = studentsRepository;
            _studentsLessonsProcedure = studentsLessonsProcedure;
        }

        public async Task<Unit> Handle(GenerateStudentsCommand request, CancellationToken cancellationToken)
        {
            foreach (var lesson in lessonsNames)
            {
                var getLessons = await _lessonsRepository.GetByNameAsync(lesson);

                if (getLessons == null)
                    await _lessonsRepository.AddAsync(
                        new Entities.Lesson()
                        {
                            Name = lesson
                        });
            }

            var lessons = await _lessonsRepository.GetAllAsync();

            var studentsLessons = new List<string>();

            var batches = Enumerable.Range(1000, 1000);

            semaphore = new SemaphoreSlim(1, 1);
            IEnumerable<Task> e;

            var batchesTasks = batches.Select(async batch =>
            {
                try
                {
                    semaphore.Wait();
                    var name = await GenerateName(10);

                    foreach (var lesson in lessons)
                    {
                        var studentsLessonsDto = new StudentsLessonsDto()
                        {
                            LessonId = lesson.Id,
                            StudentName = name,
                            SchoolGrades = RandomNumber(0, 10)
                        };

                        var lessonsAndStudents = await _studentsLessonsProcedure.AddAsync(studentsLessonsDto);

                        if (lessonsAndStudents == 0)
                            await _studentsLessonsProcedure.AddAsync(studentsLessonsDto);
                    }

                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(batchesTasks);

            return Unit.Value;
        }

        public Task<string> GenerateName(int len)
        {
            var random = new Random();

            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vogals = { "a", "e", "i", "o", "u", "ae", "y" };

            var Name = string.Empty;

            Name += consonants[random.Next(consonants.Length)].ToUpper();
            Name += vogals[random.Next(vogals.Length)];
            int repeatLetter = 2;

            while (repeatLetter < len)
            {
                Name += consonants[random.Next(consonants.Length)];
                repeatLetter++;
                Name += vogals[random.Next(vogals.Length)];
                repeatLetter++;
            }

            return Task.FromResult(Name);
        }

        private decimal RandomNumber(int min, int max)
        {
            Random rand = new Random();
            return NextDouble(rand, 6.32, 9.5, 2);

        }

        public decimal NextDouble(Random rand, double minValue, double maxValue, int decimalPlaces)
        {
            var randNumber = rand.NextDouble() * (maxValue - minValue) + minValue;
            return Convert.ToDecimal(randNumber.ToString("f" + decimalPlaces));
        }
    }
}
