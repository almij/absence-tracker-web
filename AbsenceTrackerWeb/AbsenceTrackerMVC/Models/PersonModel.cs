using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceTrackerMVC.Models
{
    public class PersonModel
    {
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public List<TeamModel> TeamsMembership { get; set; } = new List<TeamModel>();
        public List<AbsenceModel> AbsenceModels { get; set; } = new List<AbsenceModel>();
    }
}
