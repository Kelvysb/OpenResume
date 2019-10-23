using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenResumeAPI.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableName : Attribute
    {
        public TableName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnName : Attribute
    {
        public ColumnName(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
