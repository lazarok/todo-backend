using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Exceptions;
using ToDo.Application.Handlers.ToDoLists.Commands.DeleteToDoList;
using ToDo.Application.Interfaces;
using ToDo.Application.Models.Dtos;

namespace ToDo.Application.Handlers.ToDoLists.Commands.UpdateToDoList;

public class UpdateToDoListCommandHandler : IRequestHandler<UpdateToDoListCommand, ToDoListDto>
{
    private readonly IToDoUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateToDoListCommandHandler> _logger;
    
    public UpdateToDoListCommandHandler(IToDoUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateToDoListCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ToDoListDto> Handle(UpdateToDoListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _unitOfWork.ToDoListRepository.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException("ToDoList", request.Id.ToString());
            }
            
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                entity.Title = request.Title;
            }
            
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                entity.Description = request.Description;
            }
           
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            var dto = _mapper.Map<ToDoListDto>(entity);
            
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}