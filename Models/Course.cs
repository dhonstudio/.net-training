using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace traningday2.Models
{
    public class Course : BaseEntity
    {
        public int CourseID { get; set; }
        public string? Title { get; set; }
        public int Credits { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
