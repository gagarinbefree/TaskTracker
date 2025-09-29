using Mapster;
using MediatR;
using System.Collections.Generic;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskRelationshipAllQuery : IRequest<IEnumerable<TskRelationshipDto>>;

    public class GetTskRelationshipAllQueryHandler : IRequestHandler<GetTskRelationshipAllQuery, IEnumerable<TskRelationshipDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskRelationshipAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TskRelationshipDto>> Handle(GetTskRelationshipAllQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.TskRelationship.GetAllAsync()).Adapt<TskRelationshipDto[]>();

        }
    }
}

