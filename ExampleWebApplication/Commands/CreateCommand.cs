using AutoMapper;
using ExampleWebApplication.Dtos;
using ExampleWebApplication.Notifications;
using ExampleWebApplication.Repositories;
using FluentResults;
using FluentValidation;
using MediatR;

namespace ExampleWebApplication.Commands;

public class CreateCommand : IRequest<Result<ExampleDto?>>
{
    public string Name { get; init; }
}

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
    }
}

public class CreateCommandHandler(IMapper mapper, IMediator mediator, IValidator<CreateCommand> validator, ExampleRepository exampleRepository) : IRequestHandler<CreateCommand, Result<ExampleDto?>>
{
    public async Task<Result<ExampleDto?>> Handle(CreateCommand createCommand, CancellationToken cancellationToken)
    {
        var validated = validator.Validate(createCommand);

        if (!validated.IsValid) return Result.Fail(validated.Errors.ConvertAll(x => x.ErrorMessage));

        var exampleDto = await exampleRepository.Create(mapper.Map<CreateExampleDto>(createCommand));

        await mediator.Publish(new CreatedNotification(exampleDto), cancellationToken);

        return Result.Ok(exampleDto);
    }
}