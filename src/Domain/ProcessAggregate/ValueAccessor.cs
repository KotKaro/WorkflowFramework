using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Common;
using Domain.Exceptions;

namespace Domain.ProcessAggregate
{
    public class ValueAccessor : MemberDescriptor
    {
        private const BindingFlags PublicInstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public;

        public Type OwningType { get; private set; }
        public IReadOnlyCollection<MemberDescriptor> MethodArguments { get; private set; }
        private ValueAccessor() { }

        public ValueAccessor(string name, Type owningType, params MemberDescriptor[] methodArguments)
            : base(name)
        {
            OwningType = owningType;
            MethodArguments = methodArguments;
        }

        public object GetValue(object instance, params Argument[] arguments)
        {
            if (instance == null) return NullValue.Create();

            return GetValueFromProperty(instance)
                   ?? GetValueFromMethod(instance, arguments)
                   ?? throw GetValueNotReachableException(instance);
        }

        private object GetValueFromMethod(object instance, params Argument[] arguments)
        {
            var method = GetMethodWithNameAndArguments(instance, arguments);

            return method?.Invoke(instance, GetMethodArguments(arguments, method));
        }

        private static object[] GetMethodArguments(IEnumerable<Argument> arguments, MethodBase method)
        {
            var parameterNames = method.GetParameters()
                .Select(x => x.Name)
                .ToList();

            return arguments
                .Where(x => parameterNames.Contains(x.MemberDescriptor.Name))
                .OrderBy(x => parameterNames.IndexOf(x.MemberDescriptor.Name))
                .Select(x => x.Value)
                .ToArray();
        }

        private object GetValueFromProperty(object instance)
        {
            var property = GetPropertyWithName(instance);

            return property?.GetValue(instance);
        }

        private MethodInfo GetMethodWithNameAndArguments(object instance, IEnumerable<Argument> arguments)
        {
            return instance.GetType()
                .GetMethods(PublicInstanceBindingFlags)
                .Where(x => x.Name == Name)
                .Where(x => x.GetParameters().All(y => arguments.Any(v => v.MemberDescriptor.Name == y.Name)))
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault();
        }

        private PropertyInfo GetPropertyWithName(object instance)
        {
            return instance.GetType().GetProperties(PublicInstanceBindingFlags)
                .FirstOrDefault(x => x.Name == Name);
        }

        private Exception GetValueNotReachableException(object instance)
        {
            if (MethodArguments.Any())
            {
                return new ValueNotReachableException(
                    Name,
                    instance
                );
            }

            return new ValueNotReachableException(
                Name,
                MethodArguments.Select(x => x.Name),
                instance
            );
        }
    }
}