using System;

namespace Application.Commands.CreateTypeMetadata
{
    public static class CreateTypeMetadataCommandExtensions
    {
        public static Type FindType(this CreateTypeMetadataCommand @this)
        {
            return Type.GetType($"{@this.TypeFullName},{@this.AssemblyFullName}", false, false);
        }
    }
}