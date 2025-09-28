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
        public int? ParentTaskId { get; init; } = null;
        public PriorityIdEnum? PriorityId { get; init; } = null;
        public StatusIdEnum? StatusId { get; init; } = null;
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
            if (request.StatusId != null)
                tsk.StatusId = (StatusIdEnum)request.StatusId;
            if (request.PriorityId != null)
                tsk.PriorityId = (PriorityIdEnum)request.PriorityId;
            tsk.ParentTaskId = request.ParentTaskId;

            await _unitOfWork.Tsk.AddAsync(tsk);
            await _unitOfWork.CommitAsync();

            return tsk.Id;
        }
    }
}
