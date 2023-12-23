using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(int id, Type type) : base($"{type.Name} with ID of {id} was not found")
        { 
        }
    }
}
