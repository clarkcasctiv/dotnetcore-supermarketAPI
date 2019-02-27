using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Resources;

namespace Supermarket.API.Mapping
{
    public class ModelToResouceFile : Profile
    {
        public ModelToResouceFile()
        {
            CreateMap<Category, CategoryResource>();

        }

    }
}