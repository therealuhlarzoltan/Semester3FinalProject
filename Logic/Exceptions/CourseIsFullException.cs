using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Logic
{
    public class CourseIsFullException : Exception
    {
        public CourseIsFullException(Course course) : base($"Couldn't register for course -- {course} is full") 
        {

        }
    }
}
