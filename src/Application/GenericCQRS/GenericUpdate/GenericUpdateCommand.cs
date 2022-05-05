using CleanArchitectureTemplate.Application.Common.Exceptions;

namespace PersonalApp.Application.GenericCQRS.GenericUpdate;

public class GenericUpdateCommand<TEntity, TCommand>
       : IRequest<Result>
       where TEntity : BaseEntity
       where TCommand : IRequest<Result>
{
    public GenericUpdateCommand(TCommand command)
    {
        Command = command;
    }
    public TCommand Command { get; set; }
}

public class GenericUpdateCommandHandler<TEntity, TCommand> : BaseGenericCQRS, IRequestHandler<GenericUpdateCommand<TEntity, TCommand>, Result>
  where TEntity : BaseEntity
    where TCommand : BaseLongIdDTO, IRequest<Result>
{
    public GenericUpdateCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(GenericUpdateCommand<TEntity, TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            var dbSet = _dbContext.GetEntity<TEntity>();
            var item = dbSet.Find(request.Command.Id);
            if (item != null)
            {
                var updatedItem = _mapper.Map(request.Command, item);
                dbSet.Update(updatedItem);
                return Result.Success();
            }
            throw new NotFoundException();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
