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
    public class RoleService : IRole
    {

        private ApplicationDbContext _context { get; }

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tuple<string, List<Role>>> GetData()
        {
            string message = string.Empty;
            List<Role> listsOfRoles = new List<Role>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_ROLE";
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
                    Role role = new Role();
                    role.Id = Convert.ToInt32(result["Id"]);
                    role.RoleName = result["Role"].ToString();
                    role.Status = result["Status"].ToString();
                    listsOfRoles.Add(role);
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

            return new Tuple<string, List<Role>>(message, listsOfRoles);
        }


        public async Task<Tuple<string, Role>> GetSingleRole(int RoleId)
        {
            string message = string.Empty;
            Role role = new Role();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_ROLE";
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
                parameter.SqlValue = RoleId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);


                _context.Database.OpenConnection();
                var result = command.ExecuteReader();
                result.Read();
                role.Id = Convert.ToInt32(result["Id"]);
                role.RoleName = result["Role"].ToString();
                role.Status = result["Status"].ToString();

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

            return new Tuple<string, Role>(message, role);
        }


        public async Task<string> AddData(Role role)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_ROLE";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Action";
                parameter.SqlValue = "create";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@RoleName";
                parameter.SqlValue = role.RoleName;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Status";
                parameter.SqlValue = role.Status;
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


        public async Task<string> UpdateData(Role role)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_MANAGED_ROLE";
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
                parameter.SqlValue = role.Id;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@RoleName";
                parameter.SqlValue = role.RoleName;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Status";
                parameter.SqlValue = role.Status;
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


       /* public async Task<Tuple<string, List<Entity>>> GetEntities(int UserId, string role)
        {
            string message = string.Empty;
            List<Entity> RoleEntities = new List<Entity>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_USER_BY_ROLE";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();

                parameter = new SqlParameter();
                parameter.ParameterName = "@Role";
                parameter.SqlValue = role;
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
                    Entity entity = new Entity();
                    entity.Id = Convert.ToInt32(result["Id"]);
                    entity.Name = result["Name"].ToString();
                    RoleEntities.Add(entity);
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

            return new Tuple<string, List<Entity>>(message, RoleEntities);
        }*/

    }
}
