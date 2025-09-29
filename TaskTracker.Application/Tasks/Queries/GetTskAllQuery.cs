using Mapster;
using MediatR;
using System.Collections.Generic;
using TaskTracker.Application.Tasks.Dto;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public record GetTskAllQuery : IRequest<IEnumerable<TskDto>>;

    public class GetTskAllQueryHandler : IRequestHandler<GetTskAllQuery, IEnumerable<TskDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TskDto>> Handle(GetTskAllQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.Tsk.GetAllIncludeAsync(f => f.RelatedTasks)).Adapt<TskDto[]>();
        }
    }
}
