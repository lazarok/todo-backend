using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Application.Models.Dtos;
using ToDo.Application.Pagination;
using ToDo.Domain.Entities;

namespace ToDo.Application.Handlers.ToDoLists.Queries.GetAllToDoList;

public class GetAllToDoListQueryHandler : IRequestHandler<GetAllToDoListQuery, ListItems<ToDoListDto>>
{
    private readonly IToDoUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllToDoListQueryHandler(IToDoUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ListItems<ToDoListDto>> Handle(GetAllToDoListQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.ToDoListRepository.GetAllQueryable(cancellationToken);
        
        if (!string.IsNullOrEmpty(request.Filters.Search))
        {
            query = query.Where(_ => request.Filters.Search.Contains(_.Title,StringComparison.InvariantCultureIgnoreCase) || request.Filters.Search.Contains(_.Description,StringComparison.InvariantCultureIgnoreCase));
        }

        var paginate = await PaginatedList<ToDoList>.CreateAsync(query.AsNoTracking(), request.Filters.Page, request.Filters.Take);

        var list = new ListItems<ToDoListDto>
        {
            Items = _mapper.Map<List<ToDoListDto>>(paginate.Items),
            TotalCount = paginate.TotalCount,
            PageSize = paginate.PageSize,
            CurrentPage = paginate.CurrentPage,
            TotalPages = paginate.TotalPages,
            HasNextPage = paginate.HasNextPage,
            HasPreviousPage = paginate.HasPreviousPage
        };

        return list;
    }
}