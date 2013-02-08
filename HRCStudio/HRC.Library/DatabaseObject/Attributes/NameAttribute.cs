using System;

namespace HRC.Library.DatabaseObject.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NameAttribute:Attribute
    {
        public string Name { get; set; }
        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
