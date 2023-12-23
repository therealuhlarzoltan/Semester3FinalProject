using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Logic;
using Models;

namespace Endpoint
{
    [ApiController]
    [Route("/Grades")]
    public class GradeController : Controller
    {
        private IGradeLogic gradeLogic;
        
        public GradeController(IGradeLogic gradeLogic)
        {
            this.gradeLogic = gradeLogic;
        }

        [HttpGet("{id}")]
        public Grade Get(int id)
        {
            return gradeLogic.GetGrade(id);
        }

        [HttpGet]
        public IEnumerable<Grade> GetAll()
        {
            return gradeLogic.GetAllGrades();
        }

        [HttpPost]
        public void Create([FromBody] Grade grade)
        {
            gradeLogic.AddGrade(grade);
        }

        [HttpPut]
        public void Edit([FromBody] Grade grade)
        {
            gradeLogic.UpdateGrade(grade);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           gradeLogic.RemoveGrade(id);
        }

        [Route("Semester/Statistics/{year}")]
        [HttpGet]
        public SemesterStatistics GetSemesterStatistics(string year)
        {
            year = year.Replace("-", "/");
            return gradeLogic.GetSemesterStatistics(year);
        }

        [Route("Semester/Statistics")]
        [HttpGet]
        public IEnumerable<SemesterStatistics> GetAllSemesterStatistics()
        {
            return gradeLogic.GetSemesterStatistics();
        }

        [Route("Subjects/Statistics/{id}")]
        [HttpGet]
        public SubjectStatistics GetSubjectStatistics(int id)
        {
            return gradeLogic.GetSubjectStatistics(id);
        }
    }
}
