using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Commands
{
    public record UpdateTskCommand : IRequest<TskDto?>
    {
        public int Id { get; set; }
        public string? Title { get; init; } = null;
        public string? Description { get; init; } = null;
        public int? ParentTaskId { get; init; } = null;
        public PriorityIdEnum? PriorityId { get; init; } = null;
        public StatusIdEnum? StatusId { get; init; } = null;
    }

    public class UpdateTskCommandHandler : IRequestHandler<UpdateTskCommand, TskDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TskDto?> Handle(UpdateTskCommand request, CancellationToken cancellationToken)
        {
            Tsk? tsk = await _unitOfWork.Tsk.GetByIdAsync(request.Id);
            if (tsk == null)
                return null;

            tsk.Title = request.Title ?? tsk.Title;
            tsk.Description = request.Description ?? tsk.Description;
            tsk.ParentTaskId = request.ParentTaskId ?? request.ParentTaskId;    
            tsk.PriorityId = request.PriorityId ?? tsk.PriorityId;
            tsk.StatusId = request.StatusId ?? tsk.StatusId;

            await _unitOfWork.CommitAsync();

            return tsk.Adapt<TskDto>();
        }
    }
}
