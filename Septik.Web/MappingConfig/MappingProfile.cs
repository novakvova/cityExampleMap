﻿using AutoMapper;
using Septik.Web.Data.Entities;
using Septik.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Septik.Web.MappingConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<City, CityAddEditVM>();
            CreateMap<CityAddEditVM, City>();
        }
    }
}
