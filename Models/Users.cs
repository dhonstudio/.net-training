namespace traningday2.Models
{
    public class Users : BaseEntity
    {
        public int ID { get; set; }
        public string FirstMidName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Enrollment>? Enrollments{ get; set; }
    }
}
