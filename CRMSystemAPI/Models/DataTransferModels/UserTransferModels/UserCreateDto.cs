namespace CRMSystemAPI.Models.DataTransferModels.UserTransferModels
{
    public class UserCreateDto : UserBaseDto
    {
        public List<string> Roles { get; set; } = new List<string>();
    }
}
