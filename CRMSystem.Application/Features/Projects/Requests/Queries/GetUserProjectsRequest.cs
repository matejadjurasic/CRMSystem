﻿using CRMSystem.Application.DTOs.Project;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Application.Features.Projects.Requests.Queries
{
    public class GetUserProjectsRequest : IRequest<List<ProjectReadDto>>
    {
        public int UserId { get; set; }
    }
}