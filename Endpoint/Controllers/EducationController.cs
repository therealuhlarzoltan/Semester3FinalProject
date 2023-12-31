﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Logic;
using Models;

namespace Endpoint
{
    [ApiController]
    [Route("[controller]")]
    public class EducationController : Controller
    {
        private IEducationLogic educationLogic;

        public EducationController(IEducationLogic educationLogic)
        {
            this.educationLogic = educationLogic;
        }

        [Route("Subjects")]
        [HttpGet]
        public IEnumerable<Subject> GetAllSubjects()
        {
            return educationLogic.GetAllSubjects();
        }

        [Route("Subjects/{id}")]
        [HttpGet]
        public Subject GetSubject(int id)
        {
            return educationLogic.GetSubject(id);
        }

        [Route("Subjects")]
        [HttpPost]
        public void CreateSubject([FromBody] Subject subject)
        {
            educationLogic.AddSubject(subject);
        }

        [Route("Subjects")]
        [HttpPut]
        public void UpdateSubject([FromBody] Subject subject)
        {
            educationLogic.UpdateSubject(subject);
        }

        [Route("Subjects/{id}")]
        [HttpDelete]
        public void DeleteSubject(int id)
        {
            educationLogic.RemoveSubject(id);
        }

        [Route("Courses")]
        [HttpGet]
        public IEnumerable<Course> GetAllCourses()
        {
            return educationLogic.GetAllCourses();
        }

        [Route("Courses/{id}")]
        [HttpGet]
        public Course GetCourse(int id)
        {
            return educationLogic.GetCourse(id);
        }

        [Route("Courses")]
        [HttpPost]
        public void CreateCourse([FromBody] Course course)
        {
            educationLogic.AddCourse(course);
        }

        [Route("Courses")]
        [HttpPut]
        public void UpdateCourse([FromBody] Course course)
        {
            educationLogic.UpdateCourse(course);
        }

        [Route("Courses/{id}")]
        [HttpDelete]
        public void DeleteCourse(int id)
        {
            educationLogic.RemoveCourse(id);
        }

        [Route("Subjects/{subjectId}/Register/{studentId}")]
        [HttpPost]
        public void RegisterForSubject(int subjectId, int studentId)
        {
            educationLogic.RegisterStudentForSubject(studentId, subjectId);
        }

        [Route("Subjects/{subjectId}/Register/{studentId}")]
        [HttpDelete]
        public void UnregisterFromSubject(int subjectId, int studentId)
        {
            educationLogic.RemoveStudentFromSubject(studentId, subjectId);
        }

        [Route("Courses/{courseId}/Register/{studentId}")]
        [HttpPost]
        public void RegisterForCourse(int courseId, int studentId)
        {
            educationLogic.RegisterStudentForCourse(studentId, courseId);
        }

        [Route("Courses/{courseId}/Register/{studentId}")]
        [HttpDelete]
        public void UnregisterFromCourse(int courseId, int studentId)
        {
            educationLogic.RemoveStudentFromCourse(studentId, courseId);
        }

        [Route("Semester/Reset")]
        [HttpPost]
        public void ResetSemester()
        {
            educationLogic.ResetSemester();
        }
    }
}
