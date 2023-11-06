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
    public class ClaimService : IClaimService
    {
        private ApplicationDbContext _context { get; }

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> RaiseClaimRequest(ClaimRequestModel claim)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_RAISE_CLAIM_REQUEST";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@UserId";
                parameter.SqlValue = claim.UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Claim_Title";
                parameter.SqlValue = claim.ClaimTitle;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Claim_Reason";
                parameter.SqlValue = claim.ClaimReason;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Amount";
                parameter.SqlValue = Convert.ToDecimal(claim.ClaimAmount);
                parameter.SqlDbType = SqlDbType.Decimal;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Evidence";
                parameter.SqlValue = claim.Evidence;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@ExpenseDt";
                parameter.SqlValue = claim.ExpenseDt;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@Claim_Description";
                parameter.SqlValue = claim.ClaimDescription;
                parameter.SqlDbType = SqlDbType.VarChar;
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
