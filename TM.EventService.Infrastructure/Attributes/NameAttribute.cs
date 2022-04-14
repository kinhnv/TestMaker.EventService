using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.EventService.Infrastructure.Attributes
{
    public class NameAttribute : Attribute
    {
        private readonly string _name = string.Empty;

        public NameAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
    }
}
