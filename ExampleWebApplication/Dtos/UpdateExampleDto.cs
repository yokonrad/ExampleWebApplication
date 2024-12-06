namespace ExampleWebApplication.Dtos
{
    public record UpdateExampleDto
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public bool Visible { get; init; }
    }
}