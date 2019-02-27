using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Resources;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceFile : Profile
    {
        public ModelToResourceFile()
        {
            CreateMap<Category, CategoryResource>();

        }

    }
}