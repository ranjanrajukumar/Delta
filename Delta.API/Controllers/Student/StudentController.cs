using Delta.Application.DTOs.Student;
using Delta.Application.Interfaces;
using Delta.Domain.Entities;
using Delta.Domain.Entities.Student;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Delta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // POST: api/student
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var student = new Student
            {
                StudentName = dto.StudentName,
                DOB = dto.DOB,
                Age = DateTime.Now.Year - dto.DOB.Year,
                Sex = dto.Sex,

                FatherName = dto.FatherName,
                Relation = dto.Relation,
                FatherOccupation = dto.FatherOccupation,

                MotherName = dto.MotherName,
                MotherOccupation = dto.MotherOccupation,

                Email = dto.Email,
                Phone = dto.Phone,
                Mobile = dto.Mobile,

                Income = dto.Income,
                Status = 1,

                BloodGroup = dto.BloodGroup,
                PAN = dto.PAN,
                ApaarID = dto.ApaarID,          // ✅ FIXED (required by DB)
                BirthID = dto.BirthID,
                AadharNo = dto.AadharNo,
                PassportNo = dto.PassportNo,

                PresentAddress = dto.PresentAddress,
                PerCity = dto.PerCity,
                PerState = dto.PerState,
                PerPIN = dto.PerPIN,
                PerPhone = dto.PerPhone,
                PerCountry = dto.PerCountry,

                CityID = dto.CityID,
                DistID = dto.DistID,
                ReligionID = dto.ReligionID,
                QuotaID = dto.QuotaID,
                PH = dto.PH,

                Photo = dto.Photo
            };

            var result = await _studentRepository.AddAsync(student);

            // ✅ Return safe response DTO
            var response = new StudentDto
            {
                StudentID = result.StudentID,
                StudentName = result.StudentName,
                DOB = result.DOB,
                Age = result.Age,
                Sex = result.Sex,
                Mobile = result.Mobile,
                ApaarID = result.ApaarID,
                Status = result.Status
            };

            return CreatedAtAction(nameof(GetById), new { id = response.StudentID }, response);
        }

        // GET: api/student
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentRepository.GetAllAsync();

            var response = students.Select(x => new StudentDto
            {
                StudentID = x.StudentID,
                StudentName = x.StudentName,
                DOB = x.DOB,
                Age = x.Age,
                Sex = x.Sex,
                Mobile = x.Mobile,
                ApaarID = x.ApaarID,
                Status = x.Status
            });

            return Ok(response);
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");

            var response = new StudentDto
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                DOB = student.DOB,
                Age = student.Age,
                Sex = student.Sex,
                Mobile = student.Mobile,
                ApaarID = student.ApaarID,
                Status = student.Status
            };

            return Ok(response);
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.StudentID)
                return BadRequest("Student ID mismatch");

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                return NotFound("Student not found");

            student.StudentName = dto.StudentName;
            student.DOB = dto.DOB;
            student.Age = DateTime.Now.Year - dto.DOB.Year;
            student.Sex = dto.Sex;
            student.FatherName = dto.FatherName;
            student.FatherOccupation = dto.FatherOccupation;
            student.MotherName = dto.MotherName;
            student.MotherOccupation = dto.MotherOccupation;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.Mobile = dto.Mobile;
            student.Income = dto.Income;
            student.BloodGroup = dto.BloodGroup;
            student.PresentAddress = dto.PresentAddress;
            student.CityID = dto.CityID;
            student.DistID = dto.DistID;
            student.ReligionID = dto.ReligionID;
            student.QuotaID = dto.QuotaID;
            student.PH = dto.PH;
            student.Photo = dto.Photo;

            await _studentRepository.UpdateAsync(student);

            return Ok("Student updated successfully");
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentRepository.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
