using AutoMapper;
using ExampleWebApplication.Commands;
using ExampleWebApplication.Dtos;
using ExampleWebApplication.Entities;

namespace ExampleWebApplication.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Example, ExampleDto>();
            CreateMap<ExampleDto, Example>();

            CreateMap<CreateExampleDto, Example>();
            CreateMap<CreateCommand, CreateExampleDto>();

            CreateMap<UpdateExampleDto, Example>();
            CreateMap<UpdateCommand, UpdateExampleDto>();
        }
    }
}