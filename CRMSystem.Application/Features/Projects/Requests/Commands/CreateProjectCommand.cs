using CRMSystem.Application.DTOs.Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Projects.Requests.Commands
{
    public class CreateProjectCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public ProjectCreateDto? ProjectCreateDto { get; set; }
    }
}
