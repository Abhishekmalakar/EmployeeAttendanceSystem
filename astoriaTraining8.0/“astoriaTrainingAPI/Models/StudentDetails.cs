using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace _astoriaTrainingAPI.Models
{
    public partial class StudentDetails
    {
        public long StdRollNo { get; set; }
        public string StdFname { get; set; }
        public string StdLname { get; set; }
        public string StdGender { get; set; }
        public int StdCourceId { get; set; }
        public DateTime StdJoiningDate { get; set; }
        public DateTime? StdResignationDate { get; set; }

        public virtual EnrollCources StdCource { get; set; }
    }
}
