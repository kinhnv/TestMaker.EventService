using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TM.EventService.Infrastructure.Attributes;

namespace TM.EventService.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(NameAttribute)) is NameAttribute attr)
                    {
                        return attr.Name;
                    }
                }
            }
            return null;
        }
    }
}
