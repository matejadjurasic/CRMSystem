namespace CRMSystemAPI.Models.DataTransferModels.UserTransferModels
{
    public class UserReadDto : UserBaseDto
    {
        public int Id { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
