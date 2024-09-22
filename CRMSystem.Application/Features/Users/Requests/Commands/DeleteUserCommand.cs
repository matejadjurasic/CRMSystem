using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Users.Requests.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
