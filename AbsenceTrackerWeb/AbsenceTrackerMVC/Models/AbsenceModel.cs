using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenceTrackerMVC.Models
{
    public class AbsenceModel
    {
        [Display(Name = "Absence Type")]
        public string AbsenceType { get; set; }

        [Display(Name = "Effective from")]
        public DateTime EffectiveFrom { get; set; } = DateTime.Now.Date;

        [Display(Name = "Total work days")]
        public int WorkDaysTotal { get; set; } = 0;

        [Display(Name = "Single work day?")]
        public bool IsSingleDay { get; set; } = false;

        [Display(Name = "Total work hours")]
        public int WorkHoursTotal { get; set; } = 0;
    }
}
