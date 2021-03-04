using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WorkflowFramework.Domain.ProcessAggregate
{
    public class TypeMetadata
    {
        private const BindingFlags PublicInstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public;
        
        private readonly List<ValueAccessor> _valueAccessors;
        public ValueAccessor[] ValueAccessors => _valueAccessors.ToArray();
        
        public Type Type { get; }
        
        public TypeMetadata(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            _valueAccessors = new List<ValueAccessor>();

            _valueAccessors.AddRange(GetPropertiesValueAccessors());
            _valueAccessors.AddRange(GetMethodsValueAccessors());
        }

        private IEnumerable<ValueAccessor> GetMethodsValueAccessors()
        {
            return Type.GetMethods(PublicInstanceBindingFlags)
                .Where(x => !x.IsSpecialName)
                .Select(x =>
            {
                var arguments = x.GetParameters().Select(y => new MemberDescriptor(y.Name, Type))
                    .ToArray();

                return new ValueAccessor(x.Name, Type, arguments);
            });
        }

        private IEnumerable<ValueAccessor> GetPropertiesValueAccessors()
        {
            return Type.GetProperties(PublicInstanceBindingFlags)
                .Select(x => new ValueAccessor(x.Name, Type));
        }
    }
}