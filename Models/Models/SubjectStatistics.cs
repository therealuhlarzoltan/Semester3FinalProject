using System;
using System.Collections.Generic;

namespace Models
{
    public class SubjectStatistics
    {
        public Subject Subject { get; set; }
        public int NumberOfRegistrations { get; set; }
        public double PassPerRegistrationRatio { get; set; }
        public double Avg {  get; set; }

        public override bool Equals(object obj)
        {
            return obj is SubjectStatistics statistics &&
                   EqualityComparer<Subject>.Default.Equals(Subject, statistics.Subject) &&
                   NumberOfRegistrations == statistics.NumberOfRegistrations &&
                   PassPerRegistrationRatio == statistics.PassPerRegistrationRatio &&
                   Avg == statistics.Avg;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subject, NumberOfRegistrations, PassPerRegistrationRatio, Avg);
        }

        public override string ToString()
        {
            return $"{Subject};{NumberOfRegistrations};{PassPerRegistrationRatio};{Avg}";
        }
    }
}
