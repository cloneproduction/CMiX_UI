using System;

namespace CMiX.Studio.ViewModels
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