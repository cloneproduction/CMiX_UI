using System;

namespace CMiX.Services
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OSCAttribute : Attribute
    {
        public OSCAttribute()
        {
        }
    }
}