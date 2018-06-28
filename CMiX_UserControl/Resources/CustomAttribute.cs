using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OSCAttribute : Attribute
    {
        public OSCAttribute(OSCType TypeName)
        {
            Type = TypeName;
        }

        public OSCType Type;
    }
}