namespace ExampleWebApplication.Dtos
{
    public record CreateExampleDto
    {
        public required string Name { get; init; }
        public string? Description { get; init; }
        public bool Visible { get; init; }
    }
}