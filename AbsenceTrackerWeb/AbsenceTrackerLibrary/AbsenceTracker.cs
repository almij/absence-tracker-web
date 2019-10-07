using AbsenceTrackerLibrary.DataAccess;
using AbsenceTrackerLibrary.Models;

namespace AbsenceTrackerLibrary
{
    public static class AbsenceTracker
    {
        private static IDataAccess Database { get; set; } = new PostgresqlDataAccess();

        public static void SaveUserData(PersonModel person)
        {
            Database.SavePerson(person);
        }

        public static void SaveAbsence(AbsenceModel absence, int personId)
        {
            Database.SaveAbsence(absence, personId);
        }

        public static void RemoveAbsence(AbsenceModel absence)
        {
            Database.DeleteAbsence(absence);
        }
    }
}
