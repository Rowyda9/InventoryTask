
using AutoMapper;
using BackCore.BLL.ViewModels;
using BackCore.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;


namespace BackCore.API.AutoMapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegisterViewModel, User>(MemberList.None); 
            CreateMap<User, UserForDetailedViewModel>(MemberList.None);

            CreateMap<Category, CategoryViewModel>(MemberList.None).ReverseMap();
            CreateMap<Product, ProductViewModel>(MemberList.None).ReverseMap();
            CreateMap<ProductViewModel, ProductFormViewModel>(MemberList.None).ReverseMap();
            CreateMap<Status, StatusViewModel>(MemberList.None).ReverseMap();

        }
    }
}

