namespace ExampleWebApplication.Dtos;

public record CreateExampleDto
{
    public required string Name { get; init; }
}