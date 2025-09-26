using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Commands
{
    public record CreateTskCommand : IRequest<int>
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public TskPriority Priority { get; init; }
    }


    public class CreateTskCommandHandler : IRequestHandler<CreateTskCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateTskCommand request, CancellationToken cancellationToken)
        {
            Tsk tsk = new Tsk();
            tsk.Title = request.Title;
            tsk.Description = request.Description;
            tsk.Priority = request.Priority;

            await _unitOfWork.Tsk.AddAsync(tsk);
            await _unitOfWork.CommitAsync();

            return tsk.Id;
        }
    }
}
