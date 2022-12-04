using AutoMapper;
using ContactList.Application.Commands.Villain.Create;
using ContactList.Application.Commands.Villain.Update;
using ContactList.Application.DTOs;
using ContactList.Core.Entities;


namespace ContactList.Application.Mapper
{
    public class SupperVillainMappingProfile : Profile
    {
        public SupperVillainMappingProfile()
        {
            CreateMap<SuperVillain, SuperVillainResponse>().ReverseMap();
            CreateMap<SuperVillain, CreateSuperVillainCommand>().ReverseMap();
            CreateMap<SuperVillain, EditSuperVillainCommand>().ReverseMap();
        }
    }
}
