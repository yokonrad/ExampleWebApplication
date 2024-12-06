using ExampleWebApplication.Options;

namespace ExampleWebApplication.Dtos
{
    public record GetAllDto
    {
        public required GetAllOrderByOptions OrderBy { get; init; }
    }
}