using Mapster;
using MediatR;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskByIdQuery(int Id) : IRequest<TskDto?>;

    public class GetTskByIdQueryHandler : IRequestHandler<GetTskByIdQuery, TskDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TskDto?> Handle(GetTskByIdQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.Tsk.GetByExpressionIncludeAsync(f => f.Id == request.Id, f => f.RelatedTasks))?.Adapt<TskDto>();
        }
    }      
}
