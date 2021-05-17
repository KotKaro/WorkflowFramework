using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.ProcessAggregate
{
    public class TypeMetadata
    {
        private const BindingFlags PublicInstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public;

        private List<ValueProvider> _valueProviders;
        public IReadOnlyCollection<ValueProvider> ValueProviders => _valueProviders.ToArray();

        public Type Type { get; private set; }

        public TypeMetadata(string typeName, string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(typeName));
            }

            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            }

            try
            {
                Type = Type.GetType($"{typeName}, {assemblyName}");
                InitializeValueProviders();
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    $"Cannot find type with name: {typeName} - in assembly: {assemblyName}"
                );
            }
        }
        
        public TypeMetadata(Type type) : this(type?.FullName, type?.Assembly.FullName)
        {
        }
        
        private void InitializeValueProviders()
        {
            _valueProviders = new List<ValueProvider>();

            _valueProviders.AddRange(GetPropertiesValueProviders());
            _valueProviders.AddRange(GetMethodsValueProviders());
        }

        private IEnumerable<ValueProvider> GetMethodsValueProviders()
        {
            return Type.GetMethods(PublicInstanceBindingFlags)
                .Where(x => !x.IsSpecialName)
                .Select(x =>
                {
                    var arguments = x.GetParameters().Select(y => new MemberDescriptor(y.Name, y.ParameterType))
                        .ToArray();

                    return new ValueProvider(x.Name, Type, x.ReturnType, arguments);
                });
        }

        private IEnumerable<ValueProvider> GetPropertiesValueProviders()
        {
            return Type.GetProperties(PublicInstanceBindingFlags)
                .Select(x => new ValueProvider(x.Name, Type, x.PropertyType));
        }
    }
}