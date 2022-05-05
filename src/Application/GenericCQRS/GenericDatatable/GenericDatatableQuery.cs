using AutoMapper.QueryableExtensions;
using CleanArchitectureTemplate.Application.Common.Interfaces;

namespace PersonalApp.Application.GenericCQRS.GenericDatatable;

public class GenericDatatableQuery
    <TEntity, TDatatableDTO> : BaseLongIdDTO
    , IRequest<List<TDatatableDTO>>
    where TDatatableDTO : IDatatableDTO
    where TEntity : BaseEntity
{
}


public class GenericDatatableQueryHandler<TEntity, TDatatableDTO> : BaseGenericCQRS,
    IRequestHandler<GenericDatatableQuery<TEntity, TDatatableDTO>, List<TDatatableDTO>>
    where TDatatableDTO : IDatatableDTO
    where TEntity : BaseEntity
{
    public GenericDatatableQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<List<TDatatableDTO>> Handle(GenericDatatableQuery<TEntity, TDatatableDTO> request, CancellationToken cancellationToken)
    {
        //Note: you can implement some pagination base on your business in this method
        return _dbContext
            .GetEntity<TEntity>()
            .ProjectTo<TDatatableDTO>(_mapper.ConfigurationProvider)
            .ToList()
        ;
    }
}