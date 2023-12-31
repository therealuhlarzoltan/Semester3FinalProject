﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository
{
    public class GradeRepository : Repository<Grade>, IRepository<Grade>
    {
        public GradeRepository(UniversityDatabaseContext universityDatabaseContext) : base(universityDatabaseContext) { }

        public override Grade Read(int id)
        {
            return universityDatabaseContext.GradeBook.FirstOrDefault(g  => g.GradeId == id);
        }

        public override void Update(Grade entity)
        {
            var old = Read(entity.GradeId);
            CopyPropertyValues(entity, old);
            universityDatabaseContext.SaveChanges();
        }
    }
}
