using AutoMapper;
using MediatR;
using ToDo.Application.Exceptions;
using ToDo.Application.Interfaces;
using ToDo.Application.Models.Dtos;

namespace ToDo.Application.Handlers.ToDoLists.Queries.GetToDoListById;

public class GetToDoListByIdQueryHandler : IRequestHandler<GetToDoListByIdQuery, ToDoListDto>
{
    private readonly IToDoUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetToDoListByIdQueryHandler(IToDoUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ToDoListDto> Handle(GetToDoListByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ToDoListRepository.FindAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException("ToDoList", request.Id.ToString());
        }

        var result = _mapper.Map<ToDoListDto>(entity);
        return result;
    }
}