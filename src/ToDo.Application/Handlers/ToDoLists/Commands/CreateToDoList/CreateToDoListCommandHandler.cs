using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Interfaces;
using ToDo.Application.Models.Dtos;
using ToDo.Domain.Entities;

namespace ToDo.Application.Handlers.ToDoLists.Commands.CreateToDoList;

public class CreateToDoListCommandHandler : IRequestHandler<CreateToDoListCommand, ToDoListDto>
{
    private readonly IToDoUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateToDoListCommandHandler> _logger;
    
    public CreateToDoListCommandHandler(IToDoUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateToDoListCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ToDoListDto> Handle(CreateToDoListCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var entity = _mapper.Map<ToDoList>(command);
        
            _unitOfWork.ToDoListRepository.Add(entity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            var result = _mapper.Map<ToDoListDto>(entity);
        
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}