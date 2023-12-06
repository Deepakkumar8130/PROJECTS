using BAL.Interfaces;
using DAL.Data;
using MAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementations
{
    public class ProgramRightsServices : IProgramRights
    {
        private ApplicationDbContext _context { get; }

        public ProgramRightsServices(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Tuple<string, List<ProgramRight>>> GetProgramsRightsByUserId(int UserId)
        {
            string message = string.Empty;
            List<ProgramRight> programRights = new List<ProgramRight>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_PROGRAMS_RIGHTS_BY_USERID";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@UserId";
                parameter.SqlValue = UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    ProgramRight programRight = new ProgramRight();
                    programRight.Id = result["Id"].ToString();
                    programRight.Title = result["P_Title"].ToString();
                    programRight.Descr = result["Descr"].ToString();
                    programRight.IsChecked = result["IsChecked"].ToString();
                    programRights.Add(programRight);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            return new Tuple<string, List<ProgramRight>>(message, programRights);
        }


        public async Task<Tuple<string>> AssignIndividualProgramsRights(int UserId, string XML)
        {
            string message = string.Empty;
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_PROGRAMS_RIGHTS_BY_USERID";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@UserId";
                parameter.SqlValue = UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@ProgramRights";
                parameter.SqlValue = XML;
                parameter.SqlDbType = SqlDbType.Xml;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            return new Tuple<string>(message);
        }


        public async Task<Tuple<string>> AssignGroupProgramsRights(int RoleId, string XML)
        {
            string message = string.Empty;
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_PROGRAMS_RIGHTS_BY_USERID";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@RoleId";
                parameter.SqlValue = RoleId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@ProgramRights";
                parameter.SqlValue = XML;
                parameter.SqlDbType = SqlDbType.Xml;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (Exception ex)
            {

                message = ex.Message;
            }
            finally
            {
                _context.Database.CloseConnection();
            }

            return new Tuple<string>(message);
        }
    }
}
