using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BestCodify.DataAccess.Data
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        //Kullanım suresi
        //TODO:usage rate sen kursu cekerken occupancy degıstır adını
        public int Occupancy { get; set; }

        [Required]
        //Normal Ücret
        public double RegularRate { get; set; }
        public string Details { get; set; }

        //Bu olmayabilir.
        public string SqFt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<CourseImage> CourseImages { get; set; }
    }
}
