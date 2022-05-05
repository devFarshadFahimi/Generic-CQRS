using AutoMapper;
using CleanArchitectureTemplate.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureTemplate.Application.GenericCQRS.Base;
public abstract class BaseGenericCQRS
{
    protected readonly IApplicationDbContext _dbContext;
    protected readonly IMapper _mapper;

    public BaseGenericCQRS(IServiceProvider serviceProvider)
    {
        _dbContext = serviceProvider.GetRequiredService<IApplicationDbContext>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

}
