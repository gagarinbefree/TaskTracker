using Mapster;
using MediatR;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskRelationshipByIdQuery(int Id) : IRequest<TskRelationshipDto?>;

    public class GetTskRelationshipByIdQueryHandler : IRequestHandler<GetTskRelationshipByIdQuery, TskRelationshipDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskRelationshipByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TskRelationshipDto?> Handle(GetTskRelationshipByIdQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.TskRelationship.GetByIdAsync(request.Id))?.Adapt<TskRelationshipDto>();
        }
    }
}
