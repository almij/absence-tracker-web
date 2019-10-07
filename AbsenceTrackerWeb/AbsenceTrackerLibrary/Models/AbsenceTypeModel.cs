namespace AbsenceTrackerLibrary.Models
{
    public class AbsenceTypeModel
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public bool IsDayOff { get; set; } = false;
        public bool IsOvertime { get; set; } = false;

        public override string ToString()
        {
            return Name;
        }
    }
}
