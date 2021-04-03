using Evolutional.Project.Admin.Filter;
using Evolutional.Project.Controllers;
using Evolutional.Project.Domain.Commands.Lessons.Create;
using Evolutional.Project.Domain.Commands.Lessons.Delete;
using Evolutional.Project.Domain.Commands.Lessons.Update;
using Evolutional.Project.Domain.Queries.Lessons.GetFilterAllLessons;
using Evolutional.Project.Domain.Queries.Lessons.GetLessonsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Evolutional.Project.Admin.Controllers
{
    [Route("api/lessons")]
    public class LessonsController : BaseController<LessonsController>
    {
        public LessonsController(IMediator mediatorService) : base(mediatorService)
        {
        }

        [HttpGet("filter")]
        [HeaderContext]
        public async Task<IActionResult> GetFilterAll(GetFilterAllLessonsQuery query)
        {
            return await GenerateResponseAsync(async () => await MediatorService.Send(query));
        }

        [HttpPost]
        [HeaderContext]
        public async Task<IActionResult> CreateLessonsAsync([FromBody] CreateLessonsCommand command)
        {
            return await GenerateResponseAsync(async () => await MediatorService.Send(command));
        }

        [HttpGet("{id}")]
        [HeaderContext]
        public async Task<IActionResult> GetLesonsAsync(long id)
        {
            return await GenerateResponseAsync(async () => await MediatorService.Send(new GetLessonsByIdQuery(id)));
        }

        [HttpPut("{id}")]
        [HeaderContext]
        public async Task<IActionResult> UpdateLessonsAsync(long id,[FromBody] UpdateLessonsCommand command)
        {
            command.Id = id;
            return await GenerateResponseAsync(async () => await MediatorService.Send(command));
        }

        [HttpDelete("{id}")]
        [HeaderContext]
        public async Task<IActionResult> DeleteLessonsAsync(long id)
        {
            return await GenerateResponseAsync(async () => await MediatorService.Send(new DeleteLessonsCommand(id)));
        }
    }
}
