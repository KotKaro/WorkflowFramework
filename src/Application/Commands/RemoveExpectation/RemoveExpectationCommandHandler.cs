using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.ProcessAggregate.Expectations;
using Domain.Repositories;
using MediatR;

namespace Application.Commands.RemoveExpectation
{
    public class RemoveExpectationCommandHandler : IRequestHandler<RemoveExpectationCommand>
    {
        private readonly IExpectationRepository _expectationRepository;

        public RemoveExpectationCommandHandler(IExpectationRepository expectationRepository)
        {
            _expectationRepository = expectationRepository;
        }

        public async Task<Unit> Handle(RemoveExpectationCommand request, CancellationToken cancellationToken)
        {
            var expectation = await _expectationRepository.GetByIdAsync(request.ExpectationId);

            if (expectation is null)
            {
                throw new ObjectNotFoundException(request.ExpectationId, typeof(Expectation));
            }

            _expectationRepository.Remove(expectation);

            return Unit.Value;
        }
    }
}