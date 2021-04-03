using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Evolutional.Project.Infrastructure.Service
{
    public static class GeneratorHelper
    {
        public static string GetDescriptionOrPropertyName(this PropertyInfo property)
        {
            var attrType = typeof(DescriptionAttribute);
            var descriptionAttribute = (DescriptionAttribute)property.GetCustomAttributes(attrType, false).FirstOrDefault();
            return descriptionAttribute?.Description ?? property.Name;
        }

        public static string GetValueFromPropertyInfo(this PropertyInfo property, object instance)
        {
            return Convert.ToString(property.GetValue(instance, null));
        }

        public static PropertyInfo[] GetPropertiesInfo(object instance)
        {
            return instance.GetType().GetProperties();
        }
    }
}
