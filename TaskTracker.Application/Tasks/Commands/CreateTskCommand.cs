using Mapster;
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
    public record CreateTskCommand : IRequest<TskDto>
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public int? ParentTaskId { get; init; } = null;
        public PriorityIdEnum PriorityId { get; init; }
        public StatusIdEnum StatusId { get; init; }
    }

    public class CreateTskCommandHandler : IRequestHandler<CreateTskCommand, TskDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TskDto> Handle(CreateTskCommand request, CancellationToken cancellationToken)
        {
            var tsk = new Tsk();
            tsk.Title = request.Title;
            tsk.Description = request.Description;
            tsk.StatusId = request.StatusId;
            tsk.PriorityId = request.PriorityId;
            tsk.ParentTaskId = request.ParentTaskId;

            await _unitOfWork.Tsk.AddAsync(tsk);
            await _unitOfWork.CommitAsync();

            return tsk.Adapt<TskDto>();
        }
    }
}
