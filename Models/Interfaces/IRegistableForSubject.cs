using System.Collections.Generic;

namespace Models
{
    public interface IRegistableForSubject
    {
        ICollection<Subject> RegisteredSubjects { get; set; }
    }
}