using MediatR;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTaskByIdQuery(Guid Id) : IRequest<Tsk?>;

    public class GetTskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Tsk?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Task<Tsk?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Tsk.GetByIdAsync(request.Id);
        }
    }      
}
