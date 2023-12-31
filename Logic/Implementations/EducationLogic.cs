﻿using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Repository;

namespace Logic
{
    public class EducationLogic : IEducationLogic
    {
        private IRepository<Student> studentRepository;
        private IRepository<Course> courseRepository;
        private IRepository<Subject> subjectRepository;

        public EducationLogic(IRepository<Student> studentRepository, IRepository<Course> courseRepository, IRepository<Subject> subjectRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
            this.subjectRepository = subjectRepository;
        }

        public void AddCourse(Course course)
        {
            courseRepository.Create(course);
        }


        public void AddSubject(Subject subject)
        {
            bool isValid = IEducationLogic.ValidateObject<Subject>(subject);
            if (!isValid)
            {
                throw new ArgumentException("Invalid argument(s) provided!");
            }
            try
            {
                subjectRepository.Create(subject);
            }
            catch (Exception) {
                throw new ArgumentException("Failed to update database");
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return courseRepository.ReadAll();
        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            return subjectRepository.ReadAll();
        }

        public Course GetCourse(int id)
        {
            var course = courseRepository.Read(id);
            if (course == null)
            {
                throw new ObjectNotFoundException(id, typeof(Course));
            }
            return course;
        }

        public Subject GetSubject(int id)
        {
            var subject = subjectRepository.Read(id);
            if (subject == null)
            {
                throw new ObjectNotFoundException(id, typeof(Subject));
            }
            return subject;
        }

        public void RegisterStudentForCourse(int studentId, int courseId)
        {
            var student = studentRepository.Read(studentId);
            if (student == null)
            {
                throw new ObjectNotFoundException(studentId, typeof(Student));
            }

            var course = courseRepository.Read(courseId);
            if (course == null)
            {
                throw new ObjectNotFoundException(courseId, typeof(Course));
            }

            if (!course.Subject.RegisteredStudents.Contains(student))
            {
                throw new NotRegisteredForSubjectException(student, course.Subject);
            }

            if (course.EnrolledStudents.Count < course.CourseCapacity)
            {
                course.EnrolledStudents.Add(student);
                courseRepository.Update(course);
            } 
            else
            {
                throw new CourseIsFullException(course);
            }
        }

        public void RegisterStudentForSubject(int studentId, int subjectId)
        {
            var student = studentRepository.Read(studentId);
            if (student == null)
            {
                throw new ObjectNotFoundException(studentId, typeof(Student));
            }

            var subject = subjectRepository.Read(subjectId);
            if (subject == null)
            {
                throw new ObjectNotFoundException(subjectId, typeof(Subject));
            }

            if (student.CurriculumId != subject.CurriculumId)
            {
                throw new PreRequirementsNotMetException(student, subject);
            }

            if (subject.PreRequirement != null)
            {
                var newestGrade = student.Grades.Where(grade => grade.SubjectId ==  subject.PreRequirementId)
                    .OrderByDescending(grade => int.Parse(grade.Semester.Split('/')[0]))
                    .ThenByDescending(grade => int.Parse(grade.Semester.Split('/')[1]))
                    .ThenByDescending(grade => int.Parse(grade.Semester.Split('/')[2]))
                    .FirstOrDefault();
                if (newestGrade == null || newestGrade.Mark == 1)
                    throw new PreRequirementsNotMetException(student, subject);
                subject.RegisteredStudents.Add(student);
                subjectRepository.Update(subject);
            } 
            else
            {
                subject.RegisteredStudents.Add(student);
                subjectRepository.Update(subject);
            }
        }

        public void RemoveCourse(int id)
        {
            var course = courseRepository.Read(id);
            if (course == null)
                throw new ObjectNotFoundException(id, typeof(Course));
            try
            {
                course.EnrolledStudents.Clear();
                courseRepository.Update(course);
            }
            catch 
            {
                throw new ArgumentException("Failed to update database");
            }
            try
            {
                courseRepository.Delete(id);
            }
            catch 
            {
                throw new ArgumentException("Failed to update database");
            }
            
        }

        public void RemoveStudentFromCourse(int studentId, int courseId)
        {
            var student = studentRepository.Read(studentId);
            if (student == null)
            {
                throw new ObjectNotFoundException(studentId, typeof(Student));
            }

            var course = courseRepository.Read(courseId);
            if (course == null)
            {
                throw new ObjectNotFoundException(courseId, typeof(Course));
            }

            if (course.EnrolledStudents.Contains(student))
            {
                course.EnrolledStudents.Remove(student);
                courseRepository.Update(course);
            }
        }

        public void RemoveStudentFromSubject(int studentId, int subjectId)
        {
            var student = studentRepository.Read(studentId);
            var subject = subjectRepository.Read(subjectId);
            if (student == null)
                throw new ObjectNotFoundException(studentId, typeof(Student));
            if (subject == null)
                throw new ObjectNotFoundException(subjectId, typeof(Subject));
            if (!subject.RegisteredStudents.Contains(student))
                throw new NotRegisteredForSubjectException(student, subject);
            foreach (var course in student.RegisteredCourses)
            {
                if (course.Subject == subject)
                {
                    student.RegisteredCourses.Remove(course);
                }
            }
            subject.RegisteredStudents.Remove(student);
            subjectRepository.Update(subject);
            studentRepository.Update(student);
        }

        public void RemoveSubject(int id)
        {
            try
            {
                subjectRepository.Delete(id);
            }
            catch (ArgumentNullException)
            {
                throw new ObjectNotFoundException(id, typeof(Subject));
            }
        }

        public void UpdateCourse(Course course)
        {
            bool isValid = IEducationLogic.ValidateObject<Course>(course);
            if (!isValid)
            {
                throw new ArgumentException("Invalid argument(s) provided!");
            }
            var old = courseRepository.Read(course.CourseId);
            if (old == null)
                throw new ObjectNotFoundException(course.CourseId, typeof(Course));
            try
            {
                courseRepository.Update(course);
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to update database");
            }
        }


        public void UpdateSubject(Subject subject)
        {
            bool isValid = IEducationLogic.ValidateObject<Subject>(subject);
            if (!isValid)
            {
                throw new ArgumentException("Invalid argument(s) provided!");
            }
            var old = subjectRepository.Read(subject.SubjectId);
            if (old == null)
                throw new ObjectNotFoundException(subject.SubjectId, typeof(Subject));
            try
            {
                subjectRepository.Update(subject);
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to update database");
            }
        }

        public void ResetSemester()
        {
            foreach (var subject in subjectRepository.ReadAll())
            {
                foreach (var course in subject.SubjectCourses)
                {
                    course.EnrolledStudents.Clear();
                    course.CourseRegistrations.Clear();
                    courseRepository.Update(course);
                }
                subject.SubjectRegistrations.Clear();
                subject.RegisteredStudents.Clear();
                subjectRepository.Update(subject);
            }
        }
    }
}
