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
    public record DeleteTskCommand(int Id) : IRequest<bool>;

    public class DeleteTskCommandHandler: IRequestHandler<DeleteTskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTskCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Tsk.RemoveByIdAsync(request.Id);
            await _unitOfWork.CommitAsync();

            return result;
        }
    }
}
