using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CounterIntelligenceCommand.Domain.Core
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(T value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return value.ToString();
        }

        public static IDictionary<T, string> ToDictionary<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T is not an Enum type");
            }

            var values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(k => k, GetDescription);
            return values;
        }

    }

   
}
