using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rebus.Data.Model
{
    /// <summary>
    /// Defines the base structure of a Database Model.
    /// </summary>
    public abstract class Model
    {
        private List<Field> _fields;
        
        protected Model()
        {
            _fields = new List<Field>();
            AddFields();
        }

        #region Static Methods

        //public static void FindBy()

        #endregion

        #region Internal Methods

        internal void AddFields()
        {
            foreach (Type @interface in GetType().GetInterfaces())
            {
                foreach (PropertyInfo property in @interface.GetProperties())
                {
                    Field field = property.GetCustomAttribute<FieldAttribute>().Field;
                    if(!_fields.Contains(field))
                        _fields.Add(field);
                }
            }
            
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                Field field = property.GetCustomAttribute<FieldAttribute>().Field;
                if(!_fields.Contains(field))
                    _fields.Add(field);
            }
        }

        #endregion
    }
}