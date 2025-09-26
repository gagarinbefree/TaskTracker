using MediatR;
using System.Collections.Generic;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Tasks.Queries
{
    public class GetTskAllQuery : IRequest<IEnumerable<Tsk>>
    { }

    public class GetTskAllQueryHandler : IRequestHandler<GetTskAllQuery, IEnumerable<Tsk>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTskAllQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Tsk>> Handle(GetTskAllQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Tsk.GetAllAsync();

        }
    }
}
