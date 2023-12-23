using System;

namespace Models
{
    public class FormatAttribute : Attribute
    {
        public FormatAttribute(string formatDescription)
        {
            FormatDescription = formatDescription;
        }

        public string FormatDescription { get; set; }
    }
}