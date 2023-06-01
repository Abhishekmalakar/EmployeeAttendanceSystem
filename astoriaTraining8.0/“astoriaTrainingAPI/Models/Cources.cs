using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace _astoriaTrainingAPI.Models
{
    public partial class Cources
    {
        public Cources()
        {
            StudentMaster = new HashSet<StudentMaster>();
        }

        public string CourceId { get; set; }
        public string CourceName { get; set; }
        public int CourceFees { get; set; }
        public string CourceDuration { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<StudentMaster> StudentMaster { get; set; }
    }
}
