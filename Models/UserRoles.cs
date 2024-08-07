namespace traningday2.Models
{
    public class UserRoles
    {
        public int ID { get; set; }
        public int IDUser { get; set; }
        public int IDRole { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
