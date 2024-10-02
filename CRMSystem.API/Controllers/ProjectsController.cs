using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using CRMSystem.Application.DTOs.Project;
using CRMSystem.Application.Features.Projects.Requests.Queries;
using CRMSystem.Application.Features.Projects.Requests.Commands;

namespace CRMSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Projects
        [Authorize(Roles = "Admin,Client")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetProjects()
        {
            var userProjects = await _mediator.Send(new GetUserProjectsRequest { UserId = CurrentUserId });
            return Ok(userProjects);
        }

        // GET: api/Projects/all 
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetAllProjects()
        {
            var projects = await _mediator.Send(new GetProjectListRequest());
            return Ok(projects);
        }


        // GET: api/Projects/5
        [Authorize(Roles = "Admin,Client")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDto>> GetProject(int id)
        {
            var project = await _mediator.Send(new GetProjectRequest{ Id = id, UserId = CurrentUserId });
            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, [FromBody] ProjectUpdateDto updateProjectDto)
        {
            await _mediator.Send(new UpdateProjectCommand { Id = id, ProjectUpdateDto = updateProjectDto, UserId = CurrentUserId });
            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Client")]
        [HttpPost]
        public async Task<ActionResult> PostProject([FromBody] ProjectCreateDto createProjectDto)
        {
            var response = await _mediator.Send(new CreateProjectCommand { ProjectCreateDto = createProjectDto, UserId = CurrentUserId});
            return Ok(response);
        }

        // DELETE: api/Projects/5
        [Authorize(Roles = "Admin,Client")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _mediator.Send(new DeleteProjectCommand { Id = id, UserId = CurrentUserId });
            return NoContent();
        }
    }
}
