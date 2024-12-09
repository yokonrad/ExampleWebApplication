using ExampleWebApplication.Dtos;

namespace ExampleWebApplication.Repositories
{
    public interface IExampleRepository
    {
        Task<ExampleDto?> Create(CreateExampleDto createExampleDto);
        Task<ExampleDto?> Delete(int id);
        Task<IEnumerable<ExampleDto>> GetAll();
        Task<ExampleDto?> GetById(int id);
        Task<ExampleDto?> Update(int id, UpdateExampleDto updateExampleDto);
    }
}