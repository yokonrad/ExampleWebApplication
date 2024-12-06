using ExampleWebApplication.Dtos;
using ExampleWebApplication.Options;
using ExampleWebApplication.Repositories;
using MediatR;

namespace ExampleWebApplication.Queries;

public class GetAllQuery : IRequest<IEnumerable<ExampleDto>>
{
    public GetAllOrderByOptions OrderBy { get; init; }
}

public class GetAllQueryHandler(ExampleRepository exampleRepository) : IRequestHandler<GetAllQuery, IEnumerable<ExampleDto>>
{
    public async Task<IEnumerable<ExampleDto>> Handle(GetAllQuery getAllQuery, CancellationToken cancellationToken)
    {
        var getAll = await exampleRepository.GetAll();

        return getAllQuery.OrderBy switch
        {
            GetAllOrderByOptions.None => getAll,
            GetAllOrderByOptions.ById => getAll.OrderBy(x => x.Id),
            GetAllOrderByOptions.ByName => getAll.OrderBy(x => x.Name).ThenBy(x => x.Id),
            GetAllOrderByOptions.ByDescription => getAll.OrderBy(x => x.Description).ThenBy(x => x.Id),
            GetAllOrderByOptions.ByVisible => getAll.OrderBy(x => x.Visible).ThenBy(x => x.Id),
            _ => throw new NotImplementedException(),
        };
    }
}