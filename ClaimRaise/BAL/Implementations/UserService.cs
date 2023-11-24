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
    public class UserService : ICRUD<UserServiceModel>
    {

        private ApplicationDbContext _context { get; }

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tuple<string, List<UserServiceModel>>> GetData(int UserId)
        {
            string message = string.Empty;
            List<UserServiceModel> listsOfUsers = new List<UserServiceModel>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_USER";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "getall";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.SqlValue = UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);


                _context.Database.OpenConnection();
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    UserServiceModel user = new UserServiceModel();
                    user.Id = result["Id"].ToString();
                    user.UserName = result["Name"].ToString();
                    user.Email = result["Email"].ToString();
                    user.Mobile = result["Mobile"].ToString();
                    user.ManagerName = result["Manager"].ToString();
                    user.Status = result["Status"].ToString();
                    listsOfUsers.Add(user);
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

            return new Tuple<string, List<UserServiceModel>>(message, listsOfUsers);
        }


        public async Task<Tuple<string, UserServiceModel>> GetSingleUSer(int UserId)
        {
            string message = string.Empty;
            UserServiceModel user = new UserServiceModel();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_USER";
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
                parameter.SqlValue = UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);


                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
                result.Read();
                user.Id = result["Id"].ToString();
                user.UserName = result["Name"].ToString();
                user.Email = result["Email"].ToString();
                user.Mobile = result["Mobile"].ToString();
                user.ManagerId = result["Manager_Id"].ToString();
                user.Status = result["Status"].ToString();
                
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

            return new Tuple<string, UserServiceModel>(message, user);
        }


        public async Task<string> AddData(UserServiceModel user)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_USER";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "create";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Name";
                parameter.SqlValue = user.UserName;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Email";
                parameter.SqlValue = user.Email;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Mobile";
                parameter.SqlValue = user.Mobile;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Password";
                parameter.SqlValue = user.Password;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Manager";
                parameter.SqlValue = user.ManagerId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Status";
                parameter.SqlValue = user.Status;
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


        public async Task<string> UpdateData(UserServiceModel user)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_USER";
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
                parameter.SqlValue = user.Id;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Name";
                parameter.SqlValue = user.UserName;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Email";
                parameter.SqlValue = user.Email;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Mobile";
                parameter.SqlValue = user.Mobile;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Password";
                parameter.SqlValue = user.Password;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Manager";
                parameter.SqlValue = user.ManagerId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Status";
                parameter.SqlValue = user.Status;
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
