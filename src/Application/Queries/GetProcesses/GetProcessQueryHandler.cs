using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.GetProcesses
{
    // ReSharper disable once UnusedMember.Global
    public class GetProcessQueryHandler : IRequestHandler<GetProcessesQuery, IEnumerable<ProcessDTO>>
    {
        private readonly IProcessRepository _processRepository;
        private readonly IMapper _mapper;

        public GetProcessQueryHandler(IProcessRepository processRepository, IMapper mapper)
        {
            _processRepository = processRepository;
            _mapper = mapper;
        }
        
        public Task<IEnumerable<ProcessDTO>> Handle(GetProcessesQuery request, CancellationToken cancellationToken)
        {
            var processes = _processRepository
                .GetAll()
                .Select(process => _mapper.Map<ProcessDTO>(process));

            return Task.FromResult(processes);
        }
    }
}