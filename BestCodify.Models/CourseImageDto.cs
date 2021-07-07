using System.ComponentModel.DataAnnotations.Schema;

namespace BestCodify.Models
{
    public class CourseImageDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseImageUrl { get; set; }
        [ForeignKey("CourseId")]
        public virtual CourseDto Course { get; set; }
    }
}
