using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Extensions;
using Supermarket.API.Resources;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceFile : Profile
    {
        public ModelToResourceFile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Product, ProductResource>().ForMember(src => src.UnitOfMeasurement,
            opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));

        }

    }
}