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
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            //DTO to Domain
            CreateMap<Contact, ContactResponseDTO>().ReverseMap();

            //Command to Domain
            CreateMap<Contact, CreateContactCommand>().ReverseMap();

            CreateMap<Contact, UpdateContactCommand>().ReverseMap();

        }
    }
}
