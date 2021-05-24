using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate;
using Domain.Repositories;
using MediatR;
using Process = System.Diagnostics.Process;

namespace Application.Commands.CreateProcessRun
{
    public class CreateProcessRunCommandHandler : IRequestHandler<CreateProcessRunCommand>
    {
        private readonly IProcessRepository _processRepository;
        private readonly IProcessRunRepository _processRunRepository;
        private readonly IMemberDescriptorRepository _memberDescriptorRepository;

        public CreateProcessRunCommandHandler(
            IProcessRepository processRepository,
            IProcessRunRepository processRunRepository,
            IMemberDescriptorRepository memberDescriptorRepository
        )
        {
            _processRepository = processRepository;
            _processRunRepository = processRunRepository;
            _memberDescriptorRepository = memberDescriptorRepository;
        }

        public async Task<Unit> Handle(CreateProcessRunCommand request, CancellationToken cancellationToken)
        {
            var process = await _processRepository.GetByName(request.ProcessName);
            if (process is null)
            {
                throw new ObjectNotFoundException(request.ProcessName, typeof(Process));
            }

            if (!process.GotStep(request.StartStepId))
            {
                throw new StepNotInProcessException(process, request.StartStepId);
            }

            var arguments = request.ArgumentDTOs?.Select(x =>
            {
                var memberDescriptor = _memberDescriptorRepository.GetByIdAsync(x.MemberDescriptorId).Result 
                                       ?? throw new ObjectNotFoundException(
                                           x.MemberDescriptorId,
                                           typeof(MemberDescriptor)
                                        );

                return new Argument(memberDescriptor, x.Value);
            }).ToArray() ?? Array.Empty<Argument>();

            var processRun = new ProcessRun(process, process.GetStep(request.StartStepId), arguments);
            await _processRunRepository.CreateAsync(processRun);

            return Unit.Value;
        }
    }
}