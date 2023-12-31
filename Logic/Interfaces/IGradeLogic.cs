﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Models;

namespace Logic
{
    public interface IGradeLogic
    {
        void AddGrade(Grade grade);
        void RemoveGrade(int id);
        void UpdateGrade(Grade grade);
        Grade GetGrade(int id);
        IEnumerable<Grade> GetAllGrades();
        IEnumerable<SemesterStatistics> GetSemesterStatistics();
        SemesterStatistics GetSemesterStatistics(string semester);
        SubjectStatistics GetSubjectStatistics(int subjectId);
        static bool ValidateGrade(Grade grade)
        {
            Type type = grade.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "Semester")
                {
                    string value = (string)property.GetValue(grade);
                    string regEx = "^\\d{4}/\\d{2}/\\d$";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(value, regEx))
                    {
                        return false;
                    }
                }
                var attributes = property.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    var requiredAttr = attribute as RequiredAttribute;
                    var lentgthAttr = attribute as StringLengthAttribute;
                    var rangeAttr = attribute as RangeAttribute;

                    if (requiredAttr != null)
                    {
                        if (property.GetValue(grade) == null)
                        {
                            return false;
                        }
                    }

                    if (lentgthAttr != null)
                    {
                        string propertyValue = (string)property.GetValue(grade);
                        if (propertyValue.Length > lentgthAttr.MaximumLength || propertyValue.Length < lentgthAttr.MinimumLength)
                        {
                            return false;
                        }
                    }

                    if (rangeAttr != null)
                    {
                        int propertyValue = (int)property.GetValue(grade);
                        if (propertyValue > (int)rangeAttr.Maximum || propertyValue < (int)rangeAttr.Minimum)
                        {
                            return false;
                        }
                    }
                }

            }
            return true;
        }

    }
}
