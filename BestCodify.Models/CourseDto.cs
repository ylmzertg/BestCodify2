using System;
using System.Collections.Generic;
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

        public DateTime CreateDate { get; set; }
        public int TotalCount { get; set; }
        public virtual ICollection<CourseImageDto> CourseImageDto { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
