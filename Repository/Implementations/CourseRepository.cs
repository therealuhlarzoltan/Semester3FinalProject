using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class CourseRepository : Repository<Course>, IRepository<Course>
    {
        public CourseRepository(UniversityDatabaseContext universtiyDatabaseContext) : base(universtiyDatabaseContext) { }

        public override Course Read(int id)
        {
            return universityDatabaseContext.Courses.FirstOrDefault(c => c.CourseId == id);
        }

        public override void Update(Course entity)
        {
            var old = Read(entity.CourseId);
            CopyPropertyValues(entity, old);
            universityDatabaseContext.SaveChanges();
        }
    }
}
