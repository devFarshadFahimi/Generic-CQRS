namespace PersonalApp.Application.GenericCQRS.GenericDelete;

public record GenericDeleteAllCommand<TEntity>(List<long> ids)
: IRequest<Result>
where TEntity : BaseEntity;


public class GenericDeleteAllCommandHandler<TEntity> : BaseGenericCQRS, IRequestHandler<GenericDeleteAllCommand<TEntity>, Result>
     where TEntity : BaseEntity
{
    public GenericDeleteAllCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(GenericDeleteAllCommand<TEntity> request, CancellationToken cancellationToken)
    {
        try
        {
            var items = _dbContext.GetEntity<TEntity>().Where(p => request.ids.Contains(p.Id)).ToList();
            _dbContext.GetEntity<TEntity>().RemoveRange(items);
            return Result.Success();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
