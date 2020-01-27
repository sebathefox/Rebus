using System.Data;

namespace Rebus.Data
{
    public struct Field
    {
        public Field(DbType type, string name)
        {
            Type = type;
            Name = name;
        }

        public DbType Type { get; private set; }

        public string Name { get; private set; }
    }
}