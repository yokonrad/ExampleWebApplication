using ExampleWebApplication.Dtos;
using ExampleWebApplication.Repositories;
using FluentResults;
using FluentValidation;
using MediatR;

namespace ExampleWebApplication.Queries;

public class GetByIdQuery : IRequest<Result<ExampleDto?>>
{
    public int Id { get; init; }
}

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}

public class GetByIdQueryHandler(IValidator<GetByIdQuery> validator, IExampleRepository exampleRepository) : IRequestHandler<GetByIdQuery, Result<ExampleDto?>>
{
    public async Task<Result<ExampleDto?>> Handle(GetByIdQuery getByIdQuery, CancellationToken cancellationToken)
    {
        var validated = validator.Validate(getByIdQuery);

        if (!validated.IsValid) return Result.Fail(validated.Errors.ConvertAll(x => x.ErrorMessage));

        return await exampleRepository.GetById(getByIdQuery.Id);
    }
}