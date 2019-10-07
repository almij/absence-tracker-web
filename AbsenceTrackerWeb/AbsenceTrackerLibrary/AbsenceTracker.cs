using AbsenceTrackerLibrary.DataAccess;
using AbsenceTrackerLibrary.Models;
using System.Collections.Generic;

namespace AbsenceTrackerLibrary
{
    public static class AbsenceTracker
    {
        private static IDataAccess Database { get; set; } = new PostgresqlDataAccess();

        public static PersonModel GetPerson(string aspNetUserId)
        {
            return Database.GetPerson(aspNetUserId);
        }

        public static List<AbsenceModel> GetAbsences(int person_id)
        {
            return Database.GetAbsences(person_id);
        }

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
