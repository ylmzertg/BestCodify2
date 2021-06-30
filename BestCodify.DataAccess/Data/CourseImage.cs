using System.ComponentModel.DataAnnotations.Schema;

namespace BestCodify.DataAccess.Data
{
    public class CourseImage
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseImageUrl { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
    }
}
