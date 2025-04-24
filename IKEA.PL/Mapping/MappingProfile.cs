using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.PL.ViewModel;
using Microsoft.AspNetCore.Antiforgery;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentVM, CreatedDepartmentDto>().ReverseMap();
            //.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name));
            CreateMap<CreatedDepartmentDto, DepartmentVM>().ReverseMap();

            CreateMap<DepartmentVM, UpdatedDepartmentDto >().ReverseMap();

        }
    }
}
