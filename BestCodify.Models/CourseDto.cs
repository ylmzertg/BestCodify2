using System;
using System.ComponentModel.DataAnnotations;

namespace BestCodify.Models
{
    public class CourseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Course Name")]
        public string Name { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal CoursePrice{ get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalCount { get; set; } 
        public virtual CourseImageDto CourseImageDto { get; set; }
        public string ImageUrls { get; set; }
    }
}
