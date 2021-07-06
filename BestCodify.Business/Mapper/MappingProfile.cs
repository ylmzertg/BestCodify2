using AutoMapper;
using BestCodify.DataAccess.Data;
using BestCodify.Models;

namespace BestCodify.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<CourseImageDto, CourseImage>().ReverseMap();
            CreateMap<CourseOrderInfoDto, CourseOrderInfo>().ReverseMap();
        }
    }
}
