using System;

namespace Mahzan.Persistance.Extensions
{
    /// <summary>
    ///     Allows to define the string representation that will be stored in the
    ///     database for a enum value.
    /// </summary>
    public class DbStringValueAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        public DbStringValueAttribute(string value)
        {
            Value = value;
        }

        /// <summary>
        /// </summary>
        public string Value { get; }
    }
}