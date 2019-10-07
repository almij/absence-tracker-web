using System.Collections.Generic;

namespace AbsenceTrackerLibrary.Models
{
    public class TeamModel
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public PersonModel Head { get; set; }
        public List<PersonModel> Personnel { get; set; }
    }
}
