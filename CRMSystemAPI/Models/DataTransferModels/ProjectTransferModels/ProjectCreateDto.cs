using CRMSystemAPI.Models.Enums;

namespace CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels
{
    public class ProjectCreateDto : ProjectBaseDto
    {
        public int UserId { get; set; }
    }
}
