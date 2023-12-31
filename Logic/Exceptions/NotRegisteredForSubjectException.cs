﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Logic
{
    public class NotRegisteredForSubjectException : Exception
    {
        public NotRegisteredForSubjectException(Student student, Subject subject) : base($"Student {student} isn't registered for subject {subject}")
        {
        }
    }
}
