using Application.Features.Contacts.Command;
using Application.Features.Contacts.Query;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateContactCommand, Contact>().ReverseMap();
            CreateMap<DeleteContactCommand, Contact>().ReverseMap();
            CreateMap<UpdateContactCommand, Contact>().ReverseMap();
            CreateMap<Contact, ContactReadModel>().ReverseMap();
        }
    }
}
