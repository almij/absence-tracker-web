using System.Collections.Generic;
using System.Linq;

namespace AbsenceTrackerLibrary.Models
{
    public class PersonModel
    {
        public int Id { get; set; } = -1;
        public string AspNetUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TeamModel> TeamMembership { get; set; } = new List<TeamModel>();
        public List<AbsenceModel> Absences { get; set; } = new List<AbsenceModel>();

        public int TotalWorkHoursOffBalance
        {
            get
            {
                var surplus = Absences.Where(_ => _.AbsenceType.IsOvertime).Sum(_ => _.WorkHoursTotal);
                var deficit = Absences.Where(_ => _.AbsenceType.IsDayOff).Sum(_ => _.WorkHoursTotal);
                return surplus - deficit;
            }
        }

        public int WorkDaysOffBalance => TotalWorkHoursOffBalance / 8;
        public int WorkHoursOffBalance => TotalWorkHoursOffBalance % 8;
    }
}
