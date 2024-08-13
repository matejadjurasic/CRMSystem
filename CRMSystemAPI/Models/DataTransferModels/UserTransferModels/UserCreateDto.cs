namespace CRMSystemAPI.Models.DataTransferModels.UserTransferModels
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
