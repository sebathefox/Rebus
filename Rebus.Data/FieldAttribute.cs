using System;
using System.Data;

namespace Rebus.Data
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldAttribute : Attribute
    {
        public FieldAttribute(DbType type, string name)
        {
            Field = new Field(type, name);
        }

        public Field Field { get; private set; }
    }
}