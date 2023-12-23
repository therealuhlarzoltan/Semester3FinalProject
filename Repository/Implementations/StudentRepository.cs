using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class StudentRepository : Repository<Student>, IRepository<Student>
    {
        public StudentRepository(UniversityDatabaseContext universityDatabaseContext) : base(universityDatabaseContext) {}

        public override Student Read(int id)
        {
            return this.universityDatabaseContext.Students.FirstOrDefault(s => s.StudentId == id);
        }

        public override void Update(Student entity)
        {
            var old = Read(entity.StudentId);
            CopyPropertyValues(entity, old);
            universityDatabaseContext.SaveChanges();
        }
    }
}
