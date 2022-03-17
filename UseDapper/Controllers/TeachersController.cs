using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseDapper.Models;
using UseDapper.Repositories;

namespace UseDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {

        private readonly ITeacherRepository _teacherRepository;


        public TeachersController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Teacher>> GetTeachers()
        {

            return await _teacherRepository.Get();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeachers(int id)
        {

            return await _teacherRepository.Get(id);
        }


        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher([FromBody] Teacher teacher)
        {

            var newTeacher = await _teacherRepository.Create(teacher);

            return CreatedAtAction(nameof(GetTeachers), new { id = newTeacher.Id }, newTeacher);
        }


        [HttpPut]
        public async Task<ActionResult> PutTeacher(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            await _teacherRepository.Update(teacher);

            return NoContent();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var teacherDelete = await _teacherRepository.Get(id);

            if (teacherDelete == null)
            {
                return NotFound();
            }

            await _teacherRepository.Delete(teacherDelete.Id);
            return NoContent();
        }


    }

}
