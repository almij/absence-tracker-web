using AbsenceTrackerLibrary.Models;
using System.Collections.Generic;

namespace AbsenceTrackerLibrary.DataAccess
{
    interface IDataAccess
    {
        void SavePerson(PersonModel personModel);
        void SaveAbsence(AbsenceModel absenceModel, int personId);
        PersonModel GetPerson(string aspNetUserId);
        void DeleteAbsence(AbsenceModel absence);
        AbsenceTypeModel GetAbsenceType(int id);
    }
}
