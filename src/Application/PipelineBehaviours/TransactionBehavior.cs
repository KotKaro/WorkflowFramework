using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using MediatR;

namespace Application.PipelineBehaviours
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            await _unitOfWork.SaveAsync();                
            return response;
        }
    }
}