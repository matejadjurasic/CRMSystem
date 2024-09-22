using CRMSystem.Application.DTOs.Project;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Projects.Requests.Commands
{
    public class UpdateProjectCommand : IRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ProjectUpdateDto? ProjectUpdateDto { get; set; }
    }
}
