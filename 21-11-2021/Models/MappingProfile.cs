using _21_11_2021.Areas.admin.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _21_11_2021.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(x => x.SDT));

            CreateMap<ExternalLogin, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(u => u.HoTen, opt => opt.MapFrom(x => x.Principal.FindFirst(ClaimTypes.Name).Value));
            //CreateMap<UserRegistrationModel, KhachHang>()
            //    .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email))
            //    .ForMember(u => u.HoTen, opt => opt.MapFrom(x => x.HoTen))
            //    .ForMember(u => u.SDT, opt => opt.MapFrom(x => x.SDT))
            //    .ForMember(u => u.GioiTinh, opt => opt.MapFrom(x => x.GioiTinh))
            //    .ForMember(u => u.DiaChi, opt => opt.MapFrom(x => x.DiaChi));
        }


    }
}
