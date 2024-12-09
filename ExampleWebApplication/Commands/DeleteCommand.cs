using ExampleWebApplication.Dtos;
using ExampleWebApplication.Notifications;
using ExampleWebApplication.Repositories;
using FluentResults;
using FluentValidation;
using MediatR;

namespace ExampleWebApplication.Commands;

public class DeleteCommand : IRequest<Result<ExampleDto?>>
{
    public int Id { get; init; }
}

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}

public class DeleteCommandHandler(IMediator mediator, IValidator<DeleteCommand> validator, IExampleRepository exampleRepository) : IRequestHandler<DeleteCommand, Result<ExampleDto?>>
{
    public async Task<Result<ExampleDto?>> Handle(DeleteCommand deleteCommand, CancellationToken cancellationToken)
    {
        var validated = validator.Validate(deleteCommand);

        if (!validated.IsValid) return Result.Fail(validated.Errors.ConvertAll(x => x.ErrorMessage));

        var exampleDto = await exampleRepository.Delete(deleteCommand.Id);

        await mediator.Publish(new DeletedNotification(exampleDto), cancellationToken);

        return Result.Ok(exampleDto);
    }
}