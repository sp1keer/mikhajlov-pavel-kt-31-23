using Microsoft.AspNetCore.Mvc;
using mikhajlov_pavel_kt_31_23.Filters;
using mikhajlov_pavel_kt_31_23.Services.Interfaces;
using mikhajlov_pavel_kt_31_23.Models;

namespace mikhajlov_pavel_kt_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IGroupDisciplineService _groupDisciplineService;

        public GroupsController(
            IGroupService groupService,
            IGroupDisciplineService groupDisciplineService)
        {
            _groupService = groupService;
            _groupDisciplineService = groupDisciplineService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetGroups([FromQuery] GroupFilter filter, CancellationToken cancellationToken)
        {
            var groups = await _groupService.GetGroupsAsync(filter ?? new GroupFilter(), cancellationToken);
            var result = string.Join(", ", groups.Where(g => !string.IsNullOrEmpty(g.Name)).Select(g => g.Name));
            return Ok(result);
        }

        [HttpGet("average-grades")]
        public async Task<ActionResult<string[]>> GetAverageGradesByGroupName([FromQuery] string groupName, CancellationToken cancellationToken)
        {
            var result = await _groupDisciplineService.GetAverageGradesByGroupNameAsync(groupName, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> AddGroup([FromBody] Group group, CancellationToken cancellationToken)
        {
            var created = await _groupService.AddGroupAsync(group, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> UpdateGroup(int id, [FromBody] Group group, CancellationToken cancellationToken)
        {
            var updated = await _groupService.UpdateGroupAsync(id, group, cancellationToken);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id, CancellationToken cancellationToken)
        {
            await _groupService.DeleteGroupAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
