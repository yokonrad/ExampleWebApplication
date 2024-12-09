using AutoMapper;
using ExampleWebApplication.Dtos;
using ExampleWebApplication.Notifications;
using ExampleWebApplication.Repositories;
using FluentResults;
using FluentValidation;
using MediatR;

namespace ExampleWebApplication.Commands;

public class UpdateCommand : IRequest<Result<ExampleDto?>>
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public bool Visible { get; init; }
}

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name).MinimumLength(3);
        RuleFor(x => x.Description).MinimumLength(3);
    }
}

public class UpdateCommandHandler(IMapper mapper, IMediator mediator, IValidator<UpdateCommand> validator, IExampleRepository exampleRepository) : IRequestHandler<UpdateCommand, Result<ExampleDto?>>
{
    public async Task<Result<ExampleDto?>> Handle(UpdateCommand updateCommand, CancellationToken cancellationToken)
    {
        var validated = validator.Validate(updateCommand);

        if (!validated.IsValid) return Result.Fail(validated.Errors.ConvertAll(x => x.ErrorMessage));

        var exampleDto = await exampleRepository.Update(updateCommand.Id, mapper.Map<UpdateExampleDto>(updateCommand));

        await mediator.Publish(new UpdatedNotification(exampleDto), cancellationToken);

        return Result.Ok(exampleDto);
    }
}