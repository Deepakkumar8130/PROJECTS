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
    public class ProgramService : IProgramService
    {
        private ApplicationDbContext _context { get; }

        public ProgramService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserProgram>> GetProgramsById(int UserId)
        {
            var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "USP_GET_PROGRAMS";
            command.CommandType = CommandType.StoredProcedure;

            var userId = new SqlParameter("@userId", UserId);
            command.Parameters.Add(userId);
            _context.Database.OpenConnection();
            var result = command.ExecuteReader();
            List<UserProgram> Programs = new List<UserProgram>();
            while (result.Read())
            {
                UserProgram program = new UserProgram();
                program.Id = result["Id"].ToString();
                program.Title = result["Title"].ToString();
                program.Path = result["Path"].ToString();
                program.Description = result["Description"].ToString();
                Programs.Add(program);
            }
            return Programs;
        }

        public async Task<UserVM> GetUserByEmailId(string Useremail)
        {
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_USER_BY_EMAIL";
                command.CommandType = CommandType.StoredProcedure;

                var email = new SqlParameter("@email", Useremail);

                command.Parameters.Add(email);
                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
                //UserVM UserInfo = await _context.Database.SqlQueryRaw<UserVM>($"EXEC USP_GET_USER_BY_EMAIL @email={email}").FirstOrDefaultAsync();
                result.Read();
                UserVM user = new UserVM();
                user.Id = result["Id"].ToString();
                user.Email = result["Email"].ToString();
                user.Mobile = result["Mobile"].ToString();
                user.UserName = result["Nm"].ToString();
                user.ManagerId = result["Manager_Id"].ToString();
                user.Role = result["Role"].ToString();
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }


        public async Task<Tuple<string, List<ProgramMaster>>> GetData()
        {
            string message = string.Empty;
            List<ProgramMaster> listsOfPrograms = new List<ProgramMaster>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_PROGRAM";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "getall";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    ProgramMaster program = new ProgramMaster();
                    program.Id = result["Id"].ToString();
                    program.Title = result["Title"].ToString();
                    program.Path = result["Path"].ToString();
                    program.Description = result["Descr"].ToString();
                    program.Disp_Sequence = result["Sequence"].ToString();
                    program.Status = result["Status"].ToString();
                    listsOfPrograms.Add(program);
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

            return new Tuple<string, List<ProgramMaster>>(message, listsOfPrograms);
        }


        public async Task<Tuple<string, ProgramMaster>> GetSingleProg(int ProgId)
        {
            string message = string.Empty;

            ProgramMaster program = new ProgramMaster();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_PROGRAM";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "get";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.SqlValue = ProgId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
                result.Read();
                program.Id = result["Id"].ToString();
                program.Title = result["Title"].ToString();
                program.Path = result["Path"].ToString();
                program.Description = result["Descr"].ToString();
                program.Disp_Sequence = result["Sequence"].ToString();
                program.Status = result["Status"].ToString();
                
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

            return new Tuple<string, ProgramMaster>(message, program);
        }

        public async Task<string> AddProg(ProgramMaster program)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_PROGRAM";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "create";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Title";
                parameter.SqlValue = program.Title;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Path";
                parameter.SqlValue = program.Path;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Descr";
                parameter.SqlValue = program.Description;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Status";
                parameter.SqlValue = program.Status;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteScalar();
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

            return message;
        }


        public async Task<string> UpdateProg(ProgramMaster program)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_PROGRAM";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "update";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.SqlValue = program.Id;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Title";
                parameter.SqlValue = program.Title;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Path";
                parameter.SqlValue = program.Path;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Descr";
                parameter.SqlValue = program.Description;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Status";
                parameter.SqlValue = program.Status;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);


                parameter = new SqlParameter();
                parameter.ParameterName = "@Updated_Sequence";
                parameter.SqlValue = program.Disp_Sequence;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteScalar();
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

            return message;
        }
    }
}
