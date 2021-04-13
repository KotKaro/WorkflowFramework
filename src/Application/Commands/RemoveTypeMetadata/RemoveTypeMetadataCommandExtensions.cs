using System;

namespace Application.Commands.RemoveTypeMetadata
{
    public static class RemoveTypeMetadataCommandExtensions
    {
        public static Type FindType(this RemoveTypeMetadataCommand @this)
        {
            return Type.GetType($"{@this.TypeFullName},{@this.AssemblyFullName}", false, false);
        }
    }
}