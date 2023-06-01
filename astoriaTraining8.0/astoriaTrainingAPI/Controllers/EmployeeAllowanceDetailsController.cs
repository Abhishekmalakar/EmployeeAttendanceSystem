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
    public class EmployeeAllowanceDetailsController : ControllerBase
    {
        private readonly astoriaTraining80Context _context;
        private DateTime TodayDate;

        public EmployeeAllowanceDetailsController(astoriaTraining80Context context)
        {
            _context = context;
        }

        //// GET: api/EmployeeAllowanceDetails
        ///// <summary>
        ///// This method is used to get Data of EmployeeAllowance 
        ///// </summary>
        ///// <returns>EmployeeAllowanceDetail entity</returns>
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<AllowanceMaster>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IEnumerable<EmployeeAllowanceDetail>>> GetEmployeeAllowanceDetail()
        //{
        //    return await _context.EmployeeAllowanceDetail.ToListAsync();
        //}

       //// GET: api/EmployeeAllowanceDetails
       ///// <summary>
       ///// Getting the Allowance Data of perticular Employee
       ///// </summary>
       ///// <param name="id"> Integer Parameter is required</param>
       ///// <returns> Allowance Allowance Detail</returns>
        // [HttpGet("{id}")]
        //[ProducesResponseType(typeof(IEnumerable<EmployeeAllowanceDetail>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<EmployeeAllowanceDetail>> GetEmployeeAllowanceDetail(long id)
        //{
        //    var employeeAllowanceDetail = await _context.EmployeeAllowanceDetail.FindAsync(id);

        //    if (employeeAllowanceDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return employeeAllowanceDetail;
        //}

        /// <summary>
        /// Getting UserName and password from UserInfotable
        /// </summary>
        /// <param name="UserName"> String paramerter required</param>
        /// <param name="Password"> String paraamerter required</param>
        /// <returns> UserInfo Entity</returns>
        [HttpGet("Userinfo")]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UserInfo(string UserName, string Password)

        {
            bool info= await _context.UserInfo.AnyAsync(e => e.UserName == UserName && e.Password == Password);

            if (info == null)
            {
                return NoContent();
            }
            else
            {
                return info;
            }
        }

        /// <summary>
        /// This method used to get allowance Name from Allowance master table
        /// </summary>
        /// <returns> AllowanceMaster entity</returns>
        [HttpGet("allAllowanceName")]
        [ProducesResponseType(typeof(IEnumerable<AllowanceMaster>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AllowanceMaster>>> GetAllowanceMaster()
        {
            var CmCount = _context.AllowanceMaster.Count();

            if (CmCount > 0)
            {
                return await _context.AllowanceMaster.ToListAsync();
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Method is used to get present employeelist
        /// </summary>
        /// <returns> Allowance Attendance entity</returns>
        [HttpGet("PresentEmployee")]
        [ProducesResponseType(typeof(IEnumerable<AllowancesAttendance>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AllowancesAttendance>>> GetEmployeeAttendance()
        {
            try
            {

                //Attendance.ModifiedDate = DateTime.Now
                var EmpAtt = (from emp in _context.EmployeeMaster
                              join att in _context.EmployeeAttendance.Where(x => x.ClockDate.Date == DateTime.Now.Date)
                              on emp.EmployeeKey equals att.EmployeeKey
                              select new AllowancesAttendance
                              {
                                  EmployeeKey = emp.EmployeeKey,
                                  //  EmployeeID = emp.EmployeeId,
                                  EmployeeName = (emp.EmpFirstName + emp.EmpLastName),
                                  //ClockDate = Convert.ToDateTime(ClockDate).Date.ToString("yyyy-MM-dd"),
                              }).ToListAsync();
                //  return await EmpAtt.ToListAsync();
                var Allw = await EmpAtt;
                if (Allw.Count > 0)
                {
                    return Ok(Allw);
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

        /// <summary>
        /// This method is used for Post/save Allowance of employee 
        /// </summary>
        /// <param name="employeeAllowanceDetailList"></param>
        /// <returns></returns>
        [HttpPost("PostAllowance")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeAllowanceDetail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Boolean>> PostEmployeeAllowanceDetail (List<EmployeeAllowanceDetail> employeeAllowanceDetailList)
        {
            try
            {
                foreach(EmployeeAllowanceDetail empAllw in employeeAllowanceDetailList)
                {
                    if (empAllw.AllowanceAmount > 0)
                    {
                        EmployeeAllowanceDetail empAllowance = _context.EmployeeAllowanceDetail.Where(e => e.EmployeeKey == empAllw.EmployeeKey
                                                    && e.ClockDate == empAllw.ClockDate && e.AllowanceId == empAllw.AllowanceId).FirstOrDefault();


                        if (empAllowance != null)
                        {
                            empAllowance.AllowanceAmount = empAllw.AllowanceAmount;
                            empAllowance.ModifiedDate = DateTime.Now;
                            _context.Entry(empAllowance).State = EntityState.Modified;
                   
                        }
                        else
                        {
                            empAllw.CreationDate = empAllw.ModifiedDate = DateTime.Now;
                            _context.EmployeeAllowanceDetail.Add(empAllw);
                        }
                        await _context.SaveChangesAsync();
                     }
                    
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

     //   // PUT: api/EmployeeAllowanceDetails/5
     //  /// <summary>
     //  /// Method used for Update the Details of allowance
     //  /// </summary>
     //  /// <param name="id"> Integer Parametr is required</param>
     //  /// <param name="employeeAllowanceDetail"></param>
     //  /// <returns></returns>
     //   [HttpPut("{id}")]
     //   [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
     //   [ProducesResponseType(StatusCodes.Status400BadRequest)]
     //   [ProducesResponseType(StatusCodes.Status404NotFound)]
     //   [ProducesResponseType(StatusCodes.Status204NoContent)]
     //   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     //   public async Task<IActionResult> PutEmployeeAllowanceDetail(long id, EmployeeAllowanceDetail employeeAllowanceDetail)
     //   {
     //       if (id != employeeAllowanceDetail.EmployeeKey)
     //       {
     //           return BadRequest();
     //       }

     //       _context.Entry(employeeAllowanceDetail).State = EntityState.Modified;

     //       try
     //       {
     //           await _context.SaveChangesAsync();
     //       }
     //       catch (DbUpdateConcurrencyException)
     //       {
     //           if (!EmployeeAllowanceDetailExists(id))
     //           {
     //               return NotFound();
     //           }
     //           else
     //           {
     //               throw;
     //           }
     //       }

     //       return NoContent();
     //   }

     //   // POST: api/EmployeeAllowanceDetails
     ///// <summary>
     ///// save the 
     ///// </summary>
     ///// <param name="employeeAllowanceDetail"></param>
     ///// <returns></returns>
     //   [HttpPost]
     //   [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
     //   [ProducesResponseType(StatusCodes.Status409Conflict)]
     //   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     //   public async Task<ActionResult<EmployeeAllowanceDetail>> PostEmployeeAllowanceDetail(EmployeeAllowanceDetail employeeAllowanceDetail)
     //   {
     //       _context.EmployeeAllowanceDetail.Add(employeeAllowanceDetail);
     //       try
     //       {
     //           await _context.SaveChangesAsync();
     //       }
     //       catch (DbUpdateException)
     //       {
     //           if (EmployeeAllowanceDetailExists(employeeAllowanceDetail.EmployeeKey))
     //           {
     //               return Conflict();
     //           }
     //           else
     //           {
     //               throw;
     //           }
     //       }

     //       return CreatedAtAction("GetEmployeeAllowanceDetail", new { id = employeeAllowanceDetail.EmployeeKey }, employeeAllowanceDetail);
     //   }

     //   // DELETE: api/EmployeeAllowanceDetails/5
     //   [HttpDelete("{id}")]
     //   [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
     //   [ProducesResponseType(StatusCodes.Status404NotFound)]
     //   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     //   public async Task<ActionResult<EmployeeAllowanceDetail>> DeleteEmployeeAllowanceDetail(long id)
     //   {
     //       var employeeAllowanceDetail = await _context.EmployeeAllowanceDetail.FindAsync(id);
     //       if (employeeAllowanceDetail == null)
     //       {
     //           return NotFound();
     //       }

     //       _context.EmployeeAllowanceDetail.Remove(employeeAllowanceDetail);
     //       await _context.SaveChangesAsync();

     //       return employeeAllowanceDetail;
     //   }

        private bool EmployeeAllowanceDetailExists(long id)
        {
            return _context.EmployeeAllowanceDetail.Any(e => e.EmployeeKey == id);
        }
    }
}
