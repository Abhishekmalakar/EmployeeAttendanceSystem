using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _astoriaTrainingAPI.Models
{
    public class EmployeeAllowanceList
    {

        public long EmployeeKey { get; set; }
        public string EmployeeId { get; set; }

        public string EmployeeName { get; set; }
        public string AllowanceName { get; set; }
        public DateTime EmpJoiningDate { get; set; }
        public int AllowanceId { get; set; }
        public DateTime ClockDate { get; set; }
        public decimal AllowanceAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }



       
    }
}
