using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceTrackerMVC.Models
{
    public class TeamModel
    {
        public string Name { get; set; }
        public PersonModel Head { get; set; }
        public List<PersonModel> Personnel { get; set; }
    }
}
