using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Entity
{
    public class Course
    {
        public Course()
        {
            CourseTutor     =   new Tutor();
            CourseSubject   =   new Subject();
            Enrollments     =   new List<Enrollment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Double Duration { get; set; }
        public string Description { get; set; }

        public Tutor CourseTutor { get; set; }
        public Subject CourseSubject { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
