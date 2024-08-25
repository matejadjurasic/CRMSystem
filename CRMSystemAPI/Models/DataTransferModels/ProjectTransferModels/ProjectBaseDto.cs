using CRMSystemAPI.Models.Enums;

namespace CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels
{
    public class ProjectBaseDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public DateTime Deadline { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
