using AbsenceTrackerLibrary.Models;
using System.Collections.Generic;

namespace AbsenceTrackerLibrary.DataAccess
{
    interface IDataAccess
    {
        void SavePerson(PersonModel personModel);
        void SaveAbsence(AbsenceModel absenceModel, int personId);
        List<AbsenceModel> GetAbsences(int person_id);
        PersonModel GetPerson(string aspNetUserId);
        void DeleteAbsence(AbsenceModel absence);
        AbsenceTypeModel GetAbsenceType(int id);
    }
}
