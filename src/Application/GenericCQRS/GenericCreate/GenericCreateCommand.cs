namespace PersonalApp.Application.GenericCQRS.GenericCreate;

public class GenericCreateCommand<TEntity, TCommand>
       : IRequest<Result>
       where TEntity : BaseEntity
       where TCommand : IRequest<Result>
{
    public GenericCreateCommand(TCommand command)
    {
        Command = command;
    }
    public TCommand Command { get; set; }
}

public class GenericCreateCommandHandler<TEntity, TCommand> : BaseGenericCQRS, IRequestHandler<GenericCreateCommand<TEntity, TCommand>, Result>
  where TEntity : BaseEntity
    where TCommand : IRequest<Result>
{
    public GenericCreateCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(GenericCreateCommand<TEntity, TCommand> request, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.GetEntity<TEntity>().AddAsync(_mapper.Map<TEntity>(request.Command), cancellationToken);
            return Result.Success();
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
}