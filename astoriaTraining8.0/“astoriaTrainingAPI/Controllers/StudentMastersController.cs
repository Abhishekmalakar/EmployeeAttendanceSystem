using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _astoriaTrainingAPI.Models;

namespace _astoriaTrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StudentMastersController : ControllerBase
    {
        private readonly CivilServicesContext _context;

        public StudentMastersController(CivilServicesContext context)
        {
            _context = context;
        }

        // GET: api/StudentMasters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDetails>>> GetStudentMaster()
        {
            return await _context.StudentDetails.ToListAsync();
        }

         [HttpGet("StudentList")]
         public async Task<ActionResult<IEnumerable<StudentData>>> GetStudentDataList()
        {
            try
            {
                var Std = (from ED in _context.StudentDetails
                           join EC in _context.EnrollCources
                           on ED.StdCourceId equals EC.CourceId
                           select new StudentData()
                           {
                               StdRollNo = ED.StdRollNo,
                               StdCourceId=ED.StdCourceId,
                               StdName = ED.StdFname + " " + ED.StdLname,
                               StdGender = ED.StdGender,
                               StdJoiningDate = ED.StdJoiningDate,
                               CourceName = EC.CourceName,
                               CourceFees = EC.CourceFees
                           }).ToListAsync();
                var std=await Std;
                if (std.Count > 0)
                {
                    return Ok(std);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        // GET: api/StudentMasters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDetails>> GetStudentMaster(long id)
        {
            var studentDetail = await _context.StudentDetails.FindAsync(id);

            if (studentDetail == null)
            {
                return NotFound();
            }

            return studentDetail;
        }

        // PUT: api/StudentMasters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentMaster(long id, StudentMaster studentMaster)
        {
            if (id != studentMaster.StdRollNo)
            {
                return BadRequest();
            }

            _context.Entry(studentMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentMasters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<EmployeeMaster>), StatusCodes.Status200OK)]
        public async Task<ActionResult<StudentMaster>> PostStudentMaster(StudentMaster studentMaster)
        {
            _context.StudentMaster.Add(studentMaster);
            await _context.SaveChangesAsync();
           
            return CreatedAtAction("GetStudentMaster", new { id = studentMaster.StdRollNo }, studentMaster);
            
        }

        // DELETE: api/StudentMasters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentMaster>> DeleteStudentMaster(long id)
        {
            var studentMaster = await _context.StudentMaster.FindAsync(id);
            if (studentMaster == null)
            {
                return NotFound();
            }

            _context.StudentMaster.Remove(studentMaster);
            await _context.SaveChangesAsync();

            return studentMaster;
        }

        private bool StudentMasterExists(long id)
        {
            return _context.StudentMaster.Any(e => e.StdRollNo == id);
        }
    }
}
