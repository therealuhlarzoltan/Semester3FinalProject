using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class TeacherRepository : Repository<Teacher>, IRepository<Teacher>
    {
        public TeacherRepository(UniversityDatabaseContext universityDatabaseContext) : base(universityDatabaseContext) { }

        public override Teacher Read(int id)
        {
            return universityDatabaseContext.Teachers.FirstOrDefault(t => t.TeacherId == id);
        }

        public override void Update(Teacher entity)
        {
            var old = Read(entity.TeacherId);
            CopyPropertyValues(entity, old);
            universityDatabaseContext.SaveChanges();

        }
    }
}
