using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using traningday2.DTO;
using traningday2.Models;

namespace traningday2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _schoolContext;
        private readonly IMapper _mapper;

        public StudentsController(SchoolContext schoolContext, IMapper mapper)
        {
            _schoolContext = schoolContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string? search)
        {            
            var students = _schoolContext.Students.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(x => x.FirstMidName.ToLower().Contains(search.ToLower()));
            }
            return Ok(_mapper.Map<List<StudentDTO>>(students));
        }

        [HttpGet("searhbyname/{search}")]
        public IActionResult Search(string search)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _schoolContext.Students.Where(x => x.ID == id)
                .Include(x => x.Enrollments)
                .ThenInclude(x => x.Course)
                .IgnoreQueryFilters()
                .FirstOrDefault();
            if (student == null) return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult PostStudent(StudentParamDTO studentParam)
        {
            var student = _mapper.Map<Student>(studentParam);
            var studentExist = _schoolContext.Students.Any(x => x.FirstMidName == student.FirstMidName && x.LastName == student.LastName);
            if (studentExist) return BadRequest(new
            {
                Message = "user sudah ada"
            });
            student.EnrollmentDate = DateTime.Now;

            _schoolContext.Students.Add(student);

            _schoolContext.SaveChanges();
            return Ok(_mapper.Map<StudentDTO>(student));
        }

        [HttpPost("{id}/Enrollment")]
        public IActionResult AddEnrollment(int id,Enrollment enrollment)
        {
            var course = _schoolContext.Courses.Find(enrollment.CourseID);
            if (course == null) return BadRequest(" Course not found");
            enrollment.StudentID = id;
            _schoolContext.Enrollments.Add(enrollment);
            _schoolContext.SaveChanges();
            return Ok(enrollment);
        }

        [HttpPut("{id}")]   
        public IActionResult UpdateStudent(int id, Student student)
        {
            var studentInDb = _schoolContext.Students.Find(id);
            if (studentInDb == null) return NotFound();
            studentInDb.FirstMidName = student.FirstMidName;
            studentInDb.LastName = student.LastName;
            _schoolContext.Students.Update(studentInDb);
            _schoolContext.SaveChanges();
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id) 
        {
            var student = _schoolContext.Students.Find(id);
            if (student == null) return NotFound();
            _schoolContext.Students.Remove(student);
            _schoolContext.SaveChanges();
            return Ok(student);
        }
    }
}
