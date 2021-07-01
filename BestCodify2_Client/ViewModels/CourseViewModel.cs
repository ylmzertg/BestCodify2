using System;

namespace BestCodify2_Client.ViewModels
{
    public class CourseViewModel
    {
        public DateTime  CourseCreateDate{ get; set; }
        public string CourseName { get; set; }
        public string CourseSubTitle { get; set; }
        public decimal CoursePrice { get; set; }
        public float RegularRate { get; set; }
    }
}
