using BAL.Interfaces;
using ClaimAPI.Enums;
using DAL.Data;
using MAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementations
{
    public class AccountService : IAccountService
    {
        private ApplicationDbContext _context { get; }

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public async Task<LoginResult> Login(UserLogin user)
        {
            try
            {
                var email = new SqlParameter("@email", user.UserId);
                var password = new SqlParameter("@pass", user.Password);
                _context.Database.OpenConnection();
                var result = await _context.Database.SqlQuery<int>($"EXEC USP_GET_AUTHENTICATE @email={email}, @pass={password}").ToListAsync();
                LoginResult loginResult = (LoginResult)result[0];
                return loginResult;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }


        public async Task<Tuple<string, string>> pageRights(int UserId, string path)
        {
            string message = string.Empty;
            string result = string.Empty;
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_CHECK_PROGRAM_RIGHTS";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@UserId";
                parameter.SqlValue = UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@path";
                parameter.SqlValue = path;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                result = command.ExecuteScalar().ToString();

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

            return new Tuple<string, string>(message, result);
        }
    }
}
