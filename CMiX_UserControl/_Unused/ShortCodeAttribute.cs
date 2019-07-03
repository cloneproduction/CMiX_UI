using System;

namespace CMiX.ViewModels
{
    public class ShortCodeAttribute : Attribute
    {
        public string ShortName { get; }
        public string LongName { get; }

        public ShortCodeAttribute(string shortName, string longName)
        {
            ShortName = shortName;
            LongName = longName;
        }
    }
}