using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class SubjectRepository : Repository<Subject>, IRepository<Subject>
    {
        public SubjectRepository(UniversityDatabaseContext universityDatabaseContext) : base(universityDatabaseContext) { }

        public override Subject Read(int id)
        {
            return universityDatabaseContext.Subjects.FirstOrDefault(s => s.SubjectId == id);
        }

        public override void Update(Subject entity)
        {
            var old = Read(entity.SubjectId);
            CopyPropertyValues(entity, old);
            old.RegisteredStudents = entity.RegisteredStudents;
            universityDatabaseContext.SaveChanges();
        }
    }
}
