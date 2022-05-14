using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ToDo.Application.Exceptions;
using ToDo.Application.Handlers.ToDoLists.Commands.CreateToDoList;
using ToDo.Application.Interfaces;
using ToDo.Application.Models.Dtos;
using ToDo.Domain.Entities;

namespace ToDo.Application.Handlers.ToDoLists.Commands.DeleteToDoList;

public class DeleteToDoListCommandHandler : IRequestHandler<DeleteToDoListCommand>
    {
    private readonly IToDoUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteToDoListCommandHandler> _logger;
    
    public DeleteToDoListCommandHandler(IToDoUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteToDoListCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _unitOfWork.ToDoListRepository.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException("ToDoList", request.Id.ToString());
            }
            
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            _unitOfWork.ToDoListRepository.Delete(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}