using AbsenceTrackerLibrary.Models;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AbsenceTrackerLibrary.DataAccess
{
    //TODO replace all the raw Sql with StoredProc calls
    internal class PostgresqlDataAccess : IDataAccess
    {
        private string ParamChar => "_";

        //TODO mapping using structs is ugly, Dapper is said to have a mechanism for non-name-to-name mapping, research
        private struct AbsenceTypeMapper
        {
            public int absence_type_id;
            public string absence_type_name;
            public int is_day_off;
            public int is_overtime;
        }

        private struct AbsenceMapper
        {
            public int absence_id;
            public int absence_type_id;
            public int person_id;
            public DateTime effective_from;
            public int work_hours_total;
        }

        private struct PersonMapper
        {
            public int person_id;
            public string aspnetuser_id;
            public string first_name;
            public string last_name;
        }

        public void DeleteAbsence(AbsenceModel absence)
        {
            using (IDbConnection connection = ConnectionFactory())
            {
                var param = new DynamicParameters();
                param.Add($"{ParamChar}absence_id", absence.Id);
                connection.Execute("public.sp_delete_absence", param, commandType: CommandType.StoredProcedure);
            }
        }

        public AbsenceTypeModel GetAbsenceType(int id)
        {
            using (IDbConnection connection = ConnectionFactory())
            {
                var absenceTypeMappers = connection.Query<AbsenceTypeMapper>(
                    $"select * from public.absence_type where absence_type_id = '{id}';").ToList();
                if (absenceTypeMappers.Count == 0)
                {
                    return null;
                }
                return new AbsenceTypeModel
                {
                    Id = absenceTypeMappers[0].absence_type_id,
                    Name = absenceTypeMappers[0].absence_type_name,
                    IsDayOff = absenceTypeMappers[0].is_day_off == 0 ? false : true,
                    IsOvertime = absenceTypeMappers[0].is_overtime == 0 ? false : true
                };
            }
        }

        public PersonModel GetPerson(string aspNetUserId)
        {
            using (IDbConnection connection = ConnectionFactory())
            {
                var personMappers = connection.Query<PersonMapper>(
                    $"select * from public.person where username = '{aspNetUserId}'").ToList();
                if (personMappers.Count == 0)
                {
                    return null;
                }
                return new PersonModel
                {
                    Id = personMappers[0].person_id,
                    AspNetUserId = personMappers[0].aspnetuser_id,
                    FirstName = personMappers[0].first_name,
                    LastName = personMappers[0].last_name,
                    Absences = GetAbsences(personMappers[0].person_id)
                };
            }
        }

        private List<AbsenceModel> GetAbsences(int personId)
        {
            using (IDbConnection connection = ConnectionFactory())
            {
                var absenceMappers = connection.Query<AbsenceMapper>(
                    $"select * from public.absence where person_id = '{ personId }'");
                var absences = absenceMappers.Select(
                    _ => new AbsenceModel
                    {
                        Id = _.absence_id,
                        AbsenceType = GetAbsenceType(_.absence_type_id),
                        EffectiveFrom = _.effective_from,
                        WorkHoursTotal = _.work_hours_total
                    }).ToList();
                absences.Sort();
                return absences;
            }
        }

        public void SaveAbsence(AbsenceModel absence, int personId)
        {
            using (IDbConnection connection = ConnectionFactory())
            {
                var param = new DynamicParameters();
                param.Add($"{ParamChar}person_id", personId);
                param.Add($"{ParamChar}absence_type_id", absence.AbsenceType.Id);
                param.Add($"{ParamChar}effective_from", absence.EffectiveFrom);
                param.Add($"{ParamChar}work_hours_total", absence.WorkHoursTotal);
                if (absence.Id == -1)
                {
                    param.Add($"{ParamChar}absence_id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("public.sp_insert_absence", param, commandType: CommandType.StoredProcedure);
                    absence.Id = param.Get<int>($"{ParamChar}absence_id");
                }
                else
                {
                    param.Add($"{ParamChar}absence_id", absence.Id, dbType: DbType.Int32);
                    connection.Execute("public.sp_update_absence", param, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void SavePerson(PersonModel personModel)
        {
            using (IDbConnection connection = ConnectionFactory())
            {
                var param = new DynamicParameters();
                param.Add($"{ParamChar}username", personModel.AspNetUserId);
                param.Add($"{ParamChar}first_name", personModel.FirstName);
                param.Add($"{ParamChar}last_name", personModel.LastName);
                if (personModel.Id == -1)
                {
                    param.Add($"{ParamChar}person_id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    connection.Execute("public.sp_insert_person", param, commandType: CommandType.StoredProcedure);
                    personModel.Id = param.Get<int>($"{ParamChar}person_id");
                }
                else
                {
                    param.Add($"{ParamChar}person_id", personModel.Id, dbType: DbType.Int32);
                    connection.Execute("public.sp_update_person", param, commandType: CommandType.StoredProcedure);
                }
                return;
            }
        }

        private IDbConnection ConnectionFactory() =>
            new NpgsqlConnection(Helpers.ConnectionStringsHelper.GetEnvironmentConnectionString());
    }
}
