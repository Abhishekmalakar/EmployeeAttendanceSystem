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
    public class EmployeeAttendancesController : ControllerBase
    {
        private readonly astoriaTraining80Context _context;

        public EmployeeAttendancesController(astoriaTraining80Context context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is used to get List of Allattendance of Employee
        /// </summary>
        /// <param name="FilterClockDate"> DataTime Parameter required</param>
        /// <param name="FilterCompanyID"> integer Parameter required</param>
        /// <returns></returns>
        // GET: api/EmployeeAttendances
        [HttpGet("allattendanceList")]
        [ProducesResponseType(typeof(IEnumerable<Attendance>), StatusCodes.Status200OK)]
        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetEmployeeAttendance(string FilterClockDate,int FilterCompanyID)
        {
           
            try
            {
                //Attendance.ModifiedDate = DateTime.Now;
                var EmpAtt = from emp in _context.EmployeeMaster.Where(e => e.EmpCompanyId == FilterCompanyID && (e.EmpResignationDate == null || e.EmpResignationDate >= Convert.ToDateTime(FilterClockDate)))
                             join att in _context.EmployeeAttendance.Where(x => x.ClockDate == Convert.ToDateTime(FilterClockDate).Date)
                             on emp.EmployeeKey equals att.EmployeeKey
                             into grouping
                             from g in grouping.DefaultIfEmpty()
                                 //  where emp.EmpCompanyId == FilterCompanyID
                             select new Attendance
                             {
                                 EmployeeKey = emp.EmployeeKey,
                                 EmployeeID = emp.EmployeeId,
                                 EmployeeName = (emp.EmpFirstName + emp.EmpLastName),
                                 ClockDate = Convert.ToDateTime(FilterClockDate).Date.ToString("yyyy-MM-dd"),
                                
                                 TimeIn = g.TimeIn == null ? string.Empty : g.TimeIn.ToString("hh:mm"),
                                 TimeOut = g.TimeOut == null ? string.Empty : g.TimeOut.ToString("hh:mm"),
                                 Remarks = g.Remarks == null ? string.Empty : g.Remarks
                                
                             };
                return await EmpAtt.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used get attendance of perticular employee
        /// </summary>
        /// <param name="id"> integer parameter required</param>
        /// <returns> EmployeeAttendance entity</returns>
        // GET: api/EmployeeAttendances/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeAttendance>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeAttendance>> GetEmployeeAttendance(long id)
        {
            try
            {

                var employeeAttendance = await _context.EmployeeAttendance.FindAsync(id);

                if (employeeAttendance == null)
                {
                    return NotFound();
                }

                return employeeAttendance;

            }
            catch(Exception ex)
            {
                throw ex;
            }
         }

        // PUT: api/EmployeeAttendances/5
       /// <summary>
       /// Update the status of employee attendance
       /// </summary>
       /// <param name="id"> Integer parameter required</param>
       /// <param name="employeeAttendance"></param>
       /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeAttendance>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
    
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutEmployeeAttendance(long id, EmployeeAttendance employeeAttendance)
        {
            
            
                if (id != employeeAttendance.EmployeeKey)
                {
                    return BadRequest();
                }
            

            _context.Entry(employeeAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAttendanceExists(id))
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

        /// <summary>
        /// This method is used to save attendances of employee
        /// </summary>
        /// <param name="lstemployeeAttendance"></param>
        /// <returns> employee list entity</returns>
        [HttpPost("AddAttendance")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeAttendance>> PostEmployeeAttendance(List<EmployeeAttendance> lstemployeeAttendance)
        {
            if(lstemployeeAttendance.Count == 0)
            {
                return BadRequest("Employee List is NULL");
            }
            try
            {
                foreach (EmployeeAttendance empA in lstemployeeAttendance)
                {
                    EmployeeAttendance empattendance = _context.EmployeeAttendance.Where(e => e.EmployeeKey == empA.EmployeeKey
                    && e.ClockDate == empA.ClockDate).FirstOrDefault();
                    //Put atterndance
                    if (empattendance != null)
                    {
                       // empattendance.employeeKey = empA.employeeKey;
                       
                        empattendance.TimeIn = empA.TimeIn;
                        empattendance.TimeOut = empA.TimeOut;
                        empattendance.Remarks = empA.Remarks;
                        empattendance.ModifiedDate = DateTime.Now;
                        _context.Entry(empattendance).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                    //Posting attendance
                    else
                    {
                        empA.CreationDate = DateTime.Now;
                        empA.ModifiedDate = DateTime.Now;
                        _context.EmployeeAttendance.Add(empA);
                        await _context.SaveChangesAsync();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to delete the attendance of perticular employee 
        /// </summary>
        /// <param name="id">Integer parameter required</param>
        /// <returns> Emploee entity </returns>
    // DELETE: api/EmployeeAttendances/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeAttendance>> DeleteEmployeeAttendance(long id)
        {try
            {
                var employeeAttendance = await _context.EmployeeAttendance.FindAsync(id);
                if (employeeAttendance == null)
                {
                    return NotFound();
                }
                _context.EmployeeAttendance.Remove(employeeAttendance);
                await _context.SaveChangesAsync();
                return employeeAttendance;
            }
            catch (Exception ex) { throw ex; } 
        }

        private bool EmployeeAttendanceExists(long id)
        {
            return _context.EmployeeAttendance.Any(e => e.EmployeeKey == id);
        }
    }
}
