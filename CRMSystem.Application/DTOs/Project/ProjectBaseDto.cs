using CRMSystem.Domain.Enums;

namespace CRMSystem.Application.DTOs.Project
{
    public class ProjectBaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public DateTime Deadline { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
