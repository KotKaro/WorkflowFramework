using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.ProcessAggregate
{
    public class TypeMetadata
    {
        private const BindingFlags PublicInstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public;

        private List<ValueAccessor> _valueAccessors;
        public IReadOnlyCollection<ValueAccessor> ValueAccessors => _valueAccessors.ToArray();

        public Type Type { get; private set; }

        public TypeMetadata(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            InitializeValueAccessors();
        }

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
                InitializeValueAccessors();
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    $"Cannot find type with name: {typeName} - in assembly: {assemblyName}"
                );
            }
        }

        private IEnumerable<ValueAccessor> GetMethodsValueAccessors()
        {
            return Type.GetMethods(PublicInstanceBindingFlags)
                .Where(x => !x.IsSpecialName)
                .Select(x =>
                {
                    var arguments = x.GetParameters().Select(y => new MemberDescriptor(y.Name, y.ParameterType))
                        .ToArray();

                    return new ValueAccessor(x.Name, Type, x.ReturnType, arguments);
                });
        }

        private IEnumerable<ValueAccessor> GetPropertiesValueAccessors()
        {
            return Type.GetProperties(PublicInstanceBindingFlags)
                .Select(x => new ValueAccessor(x.Name, Type, x.PropertyType));
        }

        private void InitializeValueAccessors()
        {
            _valueAccessors = new List<ValueAccessor>();

            _valueAccessors.AddRange(GetPropertiesValueAccessors());
            _valueAccessors.AddRange(GetMethodsValueAccessors());
        }
    }
}