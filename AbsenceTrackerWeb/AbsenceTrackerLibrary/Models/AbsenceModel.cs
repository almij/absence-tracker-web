using System;

namespace AbsenceTrackerLibrary.Models
{
    public class AbsenceModel : IComparable<AbsenceModel>
    {
        public int Id { get; set; } = -1;
        public AbsenceTypeModel AbsenceType { get; set; }
        public DateTime EffectiveFrom { get; set; } = DateTime.Now.Date;
        public int WorkHoursTotal { get; set; } = 8;

        public int CompareTo(AbsenceModel other)
        {
            return EffectiveFrom.CompareTo(other.EffectiveFrom);
        }
    }
}
