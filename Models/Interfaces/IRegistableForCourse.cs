using System.Collections.Generic;

namespace Models
{
    public interface IRegistableForCourse
    {
        ICollection<Course> RegisteredCourses { get; set; }
    }
}