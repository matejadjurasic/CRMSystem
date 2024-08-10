namespace CRMSystemAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
        public ProjectStatus Status { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
