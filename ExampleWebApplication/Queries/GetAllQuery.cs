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
            GetAllOrderByOptions.ByName => getAll.OrderBy(x => x.Name),
            _ => throw new NotImplementedException(),
        };
    }
}