using System;

namespace Application.Commands.CreateProcessRun
{
    public class ArgumentDto
    {
        public Guid MemberDescriptorId { get; set; }
        
        public object Value { get; set; }
    }
}