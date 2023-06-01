using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _astoriaTrainingAPI.Models
{
    public class StudentData
    {
        public long StdRollNo { get; set; }
        public string StdName { get; set; }
    //    public string StdLname { get; set; }
        public string StdGender { get; set; }
        public int StdCourceId { get; set; }
        public DateTime StdJoiningDate { get; set; }
        public int CourceId { get; set; }
        public string CourceName { get; set; }
        public int CourceFees { get; set; }
    }
}
