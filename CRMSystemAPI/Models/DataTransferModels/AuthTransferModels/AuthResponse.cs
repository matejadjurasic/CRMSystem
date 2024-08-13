namespace CRMSystemAPI.Models.DataTransferModels.AuthTransferModels
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public string Token { get; set; }
    }
}
