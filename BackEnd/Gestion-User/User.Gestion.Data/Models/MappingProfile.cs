#pragma warning disable CS8618 // Suppress "Non-nullable property must contain a non-null value" warnings


using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Gestion.Data.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Sinistre, SinistreDto>().ReverseMap();
        }
    }

}
#pragma warning restore CS8618
