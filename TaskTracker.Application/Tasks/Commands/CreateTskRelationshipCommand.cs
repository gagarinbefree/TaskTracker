using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Commands
{
    public record CreateTskRelationshipCommand : IRequest<TskRelationship>
    {
        public int SourceTaskId { get; init; }
        public int TargetTaskId { get; init; }
        public RelationshipTypeIdEnum TypeId { get; set; }
    }

    public class CreateTskRelationshipCommandHandler : IRequestHandler<CreateTskRelationshipCommand, TskRelationship>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTskRelationshipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TskRelationship> Handle(CreateTskRelationshipCommand request, CancellationToken cancellationToken)
        {
            var relationship = new TskRelationship();
            relationship.SourceTaskId = request.SourceTaskId;
            relationship.TargetTaskId = request.TargetTaskId;
            relationship.TypeId = request.TypeId;

            await _unitOfWork.TskRelationship.AddAsync(relationship);
            await _unitOfWork.CommitAsync();

            return relationship;
        }
    }
}
