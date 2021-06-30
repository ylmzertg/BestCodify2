using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestCodify.Models
{
    public class CourseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Course Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Occupancy")]
        //Kullanım suresi
        public int Occupancy { get; set; }

        [Required]
        [Range(1, 3000, ErrorMessage = "Value must be between 1 and 3000")]
        //Normal Ücret
        public double RegularRate { get; set; }
        public string Details { get; set; }

        //Bu olmayabilir.
        public string SqFt { get; set; }
        public virtual ICollection<CourseImageDto> CourseImageDto { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
