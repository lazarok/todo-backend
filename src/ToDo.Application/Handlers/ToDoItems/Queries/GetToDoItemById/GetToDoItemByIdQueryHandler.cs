using AutoMapper;
using MediatR;
using ToDo.Application.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;

namespace ToDo.Application.Handlers.ToDoItems.Queries.GetToDoItemById;

public class GetToDoItemByIdQueryHandler : IRequestHandler<GetToDoItemByIdQuery, GetToDoItemByIdDto>
{
    private readonly IToDoUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetToDoItemByIdQueryHandler(IToDoUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetToDoItemByIdDto> Handle(GetToDoItemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ToDoItemRepository
            .GetByCriteriaAsync(_ => _.Id == request.Id, cancellationToken);

        entity = new ToDoItem()
        {
            Done = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = Guid.NewGuid().ToString(),
            Id = Guid.NewGuid(),
            LastModifiedAt = DateTime.UtcNow,
            LastModifiedBy = Guid.NewGuid().ToString(),
            Note = "hof afasdk Una NOTA",
            Priority = PriorityLevel.Medium,
            Scheduled = DateTime.UtcNow, Title = "Un titulo"
        };
        
        if (entity == null)
        {
            throw new NotFoundException("ToDoItem", request.Id.ToString());
        }

        var result = _mapper.Map<GetToDoItemByIdDto>(entity);
        return result;
    }
}