using MediatR;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskByIdQuery(int Id) : IRequest<Tsk?>;

    public class GetTskByIdQueryHandler : IRequestHandler<GetTskByIdQuery, Tsk?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Tsk?> Handle(GetTskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Tsk.GetByIdAsync(request.Id);
        }
    }      
}
