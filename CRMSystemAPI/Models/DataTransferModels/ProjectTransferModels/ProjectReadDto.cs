using CRMSystemAPI.Models.Enums;

namespace CRMSystemAPI.Models.DataTransferModels.ProjectTransferModels
{
    public class ProjectReadDto : ProjectBaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
