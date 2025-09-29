using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain;

namespace TaskTracker.Application.Tasks.Commands
{
    public record DeleteTskRelationshipCommand(int Id) : IRequest<bool>;

    public class DeleteTskRelationshipCommandHandler : IRequestHandler<DeleteTskRelationshipCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTskRelationshipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTskRelationshipCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TskRelationship.RemoveByIdAsync(request.Id);
            await _unitOfWork.CommitAsync();

            return result;
        }
    }
}
