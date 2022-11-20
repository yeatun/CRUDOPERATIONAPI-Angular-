using ContactList.Application.Shared.Commands.Contacts;
using ContactList.Application.Shared.DTOs;
using ContactList.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Common.Mappings
{
    public class SuperVillainMappingProfile : Profile
    {
        public SuperVillainMappingProfile()
        {
            //DTO to Domain
            CreateMap<SuperVillain, ContactResponseDTO>().ReverseMap();

            //Command to Domain
            CreateMap<SuperVillain, CreateContactCommand>().ReverseMap();

            CreateMap<SuperVillain, UpdateContactCommand>().ReverseMap();

        }
    }
}
