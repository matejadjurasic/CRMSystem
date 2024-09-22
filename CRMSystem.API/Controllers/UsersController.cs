using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRMSystem.Application.DTOs.User;
using MediatR;
using CRMSystem.Application.Features.Users.Requests.Queries;
using CRMSystem.Application.Features.Users.Requests.Commands;

namespace CRMSystemAPI.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            var users = await _mediator.Send(new GetUserListRequest());
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(int id)
        {
            var user = await _mediator.Send(new GetUserRequest { Id = id });
            return Ok(user);          
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromBody] UserUpdateDto updateUserDto)
        {
            await _mediator.Send(new UpdateUserCommand { Id = id, UserUpdateDto = updateUserDto});
            return NoContent();      
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] UserCreateDto userCreateDto)
        {
            var result = await _mediator.Send(new CreateUserCommand { UserCreateDto = userCreateDto });
            return Ok(result);
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteUserCommand { Id = id });
            return NoContent();
        }

    }
}
