using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMSystemAPI.Data;
using CRMSystemAPI.Models.DatabaseModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CRMSystemAPI.Services.ProjectServices;
using CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels;

namespace CRMSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        
        // GET: api/Projects
        [Authorize(Roles = "Admin,Client")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetProjects()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var userProjects = await _projectService.GetUserProjectsAsync(int.Parse(userId));
            return Ok(userProjects);
        }

        // GET: api/Projects
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }


        // GET: api/Projects/5
        [Authorize(Roles = "Admin,Client")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectReadDto>> GetProject(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var project = await _projectService.GetProjectByIdAsync(id, int.Parse(userId));
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, [FromBody] ProjectUpdateDto updateProjectDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var updatedProject = await _projectService.UpdateProjectAsync(id, updateProjectDto, int.Parse(userId));
            if (updatedProject == null)
            {
                return NotFound();
            }

            return Ok(updatedProject);
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Client")]
        [HttpPost]
        public async Task<ActionResult<ProjectReadDto>> PostProject([FromBody] ProjectCreateDto createProjectDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var createdProject = await _projectService.CreateProjectAsync(createProjectDto, int.Parse(userId));
                return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Projects/5
        [Authorize(Roles = "Admin,Client")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var success = await _projectService.DeleteProjectAsync(id, int.Parse(userId));
            if (!success)
            {
                return NotFound();
            }

            return Ok("Project deleted successfully.");
        }
    }
}
