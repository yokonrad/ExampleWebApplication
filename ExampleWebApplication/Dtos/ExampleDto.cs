namespace ExampleWebApplication.Dtos
{
    public record ExampleDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public bool Visible { get; init; }
    }
}