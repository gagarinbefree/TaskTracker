using MediatR;
using System.Collections.Generic;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskRelationshipAllQuery : IRequest<IEnumerable<TskRelationship>>;

    public class GetTskRelationshipAllQueryHandler : IRequestHandler<GetTskRelationshipAllQuery, IEnumerable<TskRelationship>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskRelationshipAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TskRelationship>> Handle(GetTskRelationshipAllQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TskRelationship.GetAllAsync();

        }
    }
}

