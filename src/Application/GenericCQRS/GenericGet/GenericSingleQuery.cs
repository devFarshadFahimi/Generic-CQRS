
using AutoMapper.QueryableExtensions;
using CleanArchitectureTemplate.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace PersonalApp.Application.GenericCQRS.GenericGet;

public record GenericSingleQuery<TEntity, TQueryResponseModel>(long id)
    : IRequest<Result>
    where TQueryResponseModel : BaseLongIdDTO
    where TEntity : BaseEntity;


public class GenericSingleQueryHandler<TEntity, TQueryResponseModel>
      : BaseGenericCQRS, IRequestHandler<GenericSingleQuery<TEntity, TQueryResponseModel>, Result>
      where TQueryResponseModel : BaseLongIdDTO
      where TEntity : BaseEntity
{
    public GenericSingleQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(GenericSingleQuery<TEntity, TQueryResponseModel> request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.GetEntity<TEntity>().Where(p => p.Id == request.id)
            .ProjectTo<TQueryResponseModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);

        return data == null
            ? throw new NotFoundException()
            : Result.Success(data);
    }
}