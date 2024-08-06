using AutoMapper;
using traningday2.DTO;
using traningday2.Models;

namespace traningday2.Mappers
{
    public class StudentMapper : Profile
    {
        public StudentMapper() 
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(x => x.Name, src => src.MapFrom(x => $"{x.FirstMidName} {x.LastName}"));
            CreateMap<StudentParamDTO, Student>();
        }
    }
}
