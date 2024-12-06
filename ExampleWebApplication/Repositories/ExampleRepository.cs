using AutoFixture;
using AutoMapper;
using ExampleWebApplication.Dtos;
using ExampleWebApplication.Entities;

namespace ExampleWebApplication.Repositories
{
    public class ExampleRepository(IMapper mapper)
    {
        private readonly List<Example> _examples = new Fixture().Build<Example>().CreateMany(10).ToList();

        public async Task<IEnumerable<ExampleDto>> GetAll()
        {
            return await Task.FromResult(mapper.Map<IEnumerable<ExampleDto>>(_examples));
        }

        public async Task<ExampleDto?> GetById(int id)
        {
            return await Task.FromResult(mapper.Map<ExampleDto>(_examples.FirstOrDefault(x => x.Id == id)));
        }

        public async Task<ExampleDto?> Create(CreateExampleDto createExampleDto)
        {
            var example = mapper.Map<Example>(createExampleDto);

            var random = new Random();
            var id = random.Next(_examples.Min(x => x.Id), _examples.Max(x => x.Id));

            while (_examples.Any(x => x.Id == id))
            {
                id = random.Next(_examples.Min(x => x.Id), _examples.Max(x => x.Id));
            }

            example.Id = id;

            _examples.Add(example);

            return await Task.FromResult(mapper.Map<ExampleDto>(example));
        }

        public async Task<ExampleDto?> Update(int id, UpdateExampleDto updateExampleDto)
        {
            var index = _examples.FindIndex(x => x.Id == id);

            if (index < 0) return null;

            var example = mapper.Map<Example>(updateExampleDto);

            example.Id = id;

            _examples[index] = example;

            return await Task.FromResult(mapper.Map<ExampleDto>(example));
        }

        public async Task<ExampleDto?> Delete(int id)
        {
            var example = _examples.FirstOrDefault(x => x.Id == id);

            if (example is null) return null;

            _examples.Remove(example);

            return await Task.FromResult(mapper.Map<ExampleDto>(example));
        }
    }
}