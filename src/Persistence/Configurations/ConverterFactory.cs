using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.Configurations
{
    public static class ConverterFactory
    {
        public static ValueConverter<Type, string> CreateTypeToStringConverter()
        {
            return new(
                type => $"{type.FullName}, {type.Assembly.FullName}",
                typeFullName => Type.GetType(typeFullName)
            );
        }
    }
}