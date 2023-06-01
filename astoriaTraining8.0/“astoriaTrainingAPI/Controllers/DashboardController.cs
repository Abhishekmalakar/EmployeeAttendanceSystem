using _astoriaTrainingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _astoriaTrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DashboardController : ControllerBase
    {
        private astoriaTraining80Context _context;

        public DashboardController(astoriaTraining80Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Count the total active Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet("EmployeeCount")]
        [ProducesResponseType(typeof(IEnumerable<Employee>), StatusCodes.Status200OK)]
        public int[] GetEmployeeCount()
        {
            try
            {


                int employeeCount = _context.EmployeeMaster.Count();
                int Resignation = _context.EmployeeMaster.Where(e => e.EmpResignationDate < DateTime.Now).Count();
                int Activetotal = employeeCount - Resignation;

                return new int[] { Activetotal, Resignation };
            }
            catch(Exception ex)
            {
               throw ex;
            }

        }

        /// <summary>
        /// This method is used Sum of the total working hour of Employees
        /// </summary>
        /// <returns>Dashbord entity </returns>
        [HttpGet("EmployeeWorkingHours")]
        [ProducesResponseType(typeof(IEnumerable<Dashboard>), StatusCodes.Status200OK)]
        public IEnumerable<Dashboard> GetEmployeeHour()
        {
            try
            {


                var empWorkingHour = _context.EmployeeAttendance.AsEnumerable().GroupBy(e => e.ClockDate)
                                     .Select(e => new Dashboard()
                                     {
                                         Date = e.Key.ToString("dd MMM yyyy"),
                                         ClockDate = e.Key.Date,
                                         TotalWorkingHours = e.Sum(e => e.TimeOut.Hour - e.TimeIn.Hour)
                                     }).OrderByDescending(e => e.ClockDate).Take(5).ToList();
                return empWorkingHour;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Calculating the total Salary of employee acording to working hours
        /// </summary>
        /// <returns> Dashbord entity</returns>
        [HttpGet("EmployeeTotalSalary")]
        [ProducesResponseType(typeof(IEnumerable<Dashboard>), StatusCodes.Status200OK)]
        public IEnumerable<Dashboard> GetTotalSalary()
        {
            try
            {

                var EmpSallry = (from EM in _context.EmployeeMaster
                                 join EA in _context.EmployeeAttendance
                                 on EM.EmployeeKey equals EA.EmployeeKey
                                 select new { EM, EA }).GroupBy(e => e.EA.ClockDate).Select(e => new Dashboard()
                                 {
                                     ClockDate = e.Key.Date,
                                     TotalSalary = e.Sum(e => (e.EA.TimeOut.Hour - e.EA.TimeIn.Hour) * e.EM.EmpHourlySalaryRate)
                                 }).OrderByDescending(e => e.ClockDate).Take(5);
                var EmpAllowance = (from EA in _context.EmployeeAttendance
                                    join EAD in _context.EmployeeAllowanceDetail
                                    on new { EA.EmployeeKey, EA.ClockDate } equals new { EAD.EmployeeKey, EAD.ClockDate }
                                    into g
                                    from i in g.DefaultIfEmpty()
                                    select new { EA, i }).GroupBy(e => e.EA.ClockDate).Select(e => new Dashboard()
                                    {
                                        ClockDate = e.Key.Date,
                                        TotalSalary = e.Sum(e => e.i.AllowanceAmount == null ? 0 : e.i.AllowanceAmount)

                                    }).OrderByDescending(e => e.ClockDate).Take(5);
                var TotalEmployeeSalary = (from s in EmpSallry
                                           from a in EmpAllowance
                                           where s.ClockDate == a.ClockDate
                                           select new Dashboard()
                                           {
                                               ClockDate = s.ClockDate,
                                               TotalSalary = s.TotalSalary + a.TotalSalary
                                           }).ToList();

                return TotalEmployeeSalary;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
 }

    