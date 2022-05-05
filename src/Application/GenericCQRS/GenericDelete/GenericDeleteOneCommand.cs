namespace PersonalApp.Application.GenericCQRS.GenericDelete;

public record GenericDeleteOneCommand<TEntity>(long id)
    : IRequest<Result>
    where TEntity : BaseEntity;

public class GenericDeleteOneCommandHandler<TEntity> : BaseGenericCQRS, IRequestHandler<GenericDeleteOneCommand<TEntity>, Result>
     where TEntity : BaseEntity
{
    public GenericDeleteOneCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(GenericDeleteOneCommand<TEntity> request, CancellationToken cancellationToken)
    {
        try
        {
            var item = _dbContext.GetEntity<TEntity>().Where(p => p.Id == request.id).SingleOrDefault();
            if (item != null)
            {
                _dbContext.GetEntity<TEntity>().Remove(item);
                return Result.Success();
            }
            return Result.Failure(new List<string> { "Item not found...!" });
        }
        catch (Exception)
        {
            throw;
        }
    }
}