using Evolutional.Project.Domain.Dto;
using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Services.Xlsx;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Evolutional.Project.Domain.Queries.Students.ExportStudents
{
    public class ExportStudentsQueryHandler : IRequestHandler<ExportStudentsQuery, ExportStudentsQueryResponse>
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly ISheetsService _sheetsService;

        public ExportStudentsQueryHandler(
            ISheetsService sheetsService,
            IStudentsRepository studentsRepository
            )
        {
            _sheetsService = sheetsService;
            _studentsRepository = studentsRepository;
        }

        public async Task<ExportStudentsQueryResponse> Handle(ExportStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentsRepository.GetAllAsync();

            var sheetWorks = students.GroupBy(s => s.Name).Select(g =>
            {

                var schoolGrades = g.Select(i => i.SchoolGrades);

                var schoolGradesJoin = string.Join("|", schoolGrades).Split('|');
                var lessonNames = string.Join("|", g.Select(i => i.LessonName)).Split('|');

                return new ExcelDto()
                {
                    Nome = g.Key,
                    Media = Math.Round(schoolGrades.Average(),2),
                    Materia1 = lessonNames[0],
                    Nota1 = schoolGradesJoin[0],
                    Materia2 = lessonNames[1],
                    Nota2 = schoolGradesJoin[1],
                    Materia3 = lessonNames[2],
                    Nota3 = schoolGradesJoin[2],
                    Materia4 = lessonNames[3],
                    Nota4 = schoolGradesJoin[3],
                    Materia5 = lessonNames[4],
                    Nota5 = schoolGradesJoin[4],
                    Materia6 = lessonNames[5],
                    Nota6 = schoolGradesJoin[5],
                    Materia7 = lessonNames[6],
                    Nota7 = schoolGradesJoin[6],
                    Materia8 = lessonNames[7],
                    Nota8 = schoolGradesJoin[7],
                    Materia9 = lessonNames[8],
                    Nota9 = schoolGradesJoin[8]
                };
            });

            byte[] data;

            data = _sheetsService.Generate("Students", sheetWorks);

            return new ExportStudentsQueryResponse(data);
        }
    }
}
