using System.Collections.Generic;
using MediatR;

namespace Application.Queries.GetProcesses
{
    public class GetProcessesQuery : IRequest<IEnumerable<ProcessDTO>>
    {
        
    }
}