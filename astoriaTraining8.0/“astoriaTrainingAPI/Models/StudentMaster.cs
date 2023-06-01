using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace _astoriaTrainingAPI.Models
{
    public partial class StudentMaster
    {
        public long StdRollNo { get; set; }
        public string StdFname { get; set; }
        public string StdLname { get; set; }
        public string StdGender { get; set; }
        public string StdCourceId { get; set; }
        public DateTime StdJoiningDate { get; set; }
        public DateTime? StdResignationDate { get; set; }

        public virtual Cources StdCource { get; set; }
    }
}
