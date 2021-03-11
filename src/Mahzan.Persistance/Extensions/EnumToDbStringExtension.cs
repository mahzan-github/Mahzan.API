using System;
using System.Data;
using System.Reflection;

namespace Mahzan.Persistance.Extensions
{
    /// <summary>
    /// </summary>
    public static class EnumToDbStringExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ToDbString(this Enum value)
        {
            Type type = value.GetType();

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DbStringValueAttribute), false);

                if (attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DbStringValueAttribute)attrs[0]).Value;
                }
            }

            //If we have no DbString attribute, just return the ToString of the enum
            throw new ConstraintException($"The value {value} has no valid {nameof(DbStringValueAttribute)} attribute.");
        }
    }
}