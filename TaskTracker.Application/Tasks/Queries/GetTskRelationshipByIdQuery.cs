using MediatR;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskRelationshipByIdQuery(int Id) : IRequest<TskRelationship?>;

    public class GetTskRelationshipByIdQueryHandler : IRequestHandler<GetTskRelationshipByIdQuery, TskRelationship?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskRelationshipByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TskRelationship?> Handle(GetTskRelationshipByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TskRelationship.GetByIdAsync(request.Id);
        }
    }
}
