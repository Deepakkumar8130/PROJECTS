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
        
        
        public async Task<Tuple<string, List<ClaimRequest>>> GetAllPendingRequests(int UserId, string Role)
        {
            string message = string.Empty;
            List<ClaimRequest> claimRequests = new List<ClaimRequest>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_PENDING_REQUEST";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@userId";
                parameter.SqlValue = UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@role";
                parameter.SqlValue = Role;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();

                while(result.Read())
                {
                    ClaimRequest claimRequest = new ClaimRequest();
                    claimRequest.ClaimId = Convert.ToInt32(result["ClaimId"]);
                    claimRequest.EmployeeName = result["EmployeeName"].ToString();
                    claimRequest.ClaimTitle = result["ClaimTitle"].ToString();
                    claimRequest.ClaimReason = result["ClaimReason"].ToString();
                    claimRequest.ClaimAmount = result["ClaimAmount"].ToString();
                    claimRequest.ClaimEvidence = result["ClaimEvidence"].ToString();
                    claimRequest.ClaimExpenseDt = result["ClaimExpenseDt"].ToString();
                    claimRequest.ClaimDescription = result["ClaimDescription"].ToString();
                    claimRequest.ClaimDt = result["ClaimDt"].ToString();
                    claimRequest.CurrentStatus = result["CurrentStatus"].ToString();
                    claimRequests.Add(claimRequest);
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

            return new Tuple<string, List<ClaimRequest>>(message, claimRequests);
        }


        public async Task<string> ActionOnRequest(ClaimAction claim)
        {
            string message = string.Empty;

            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_UPDATE_CLAIM";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@claimId";
                parameter.SqlValue = claim.ClaimId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@userId";
                parameter.SqlValue = claim.UserId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@action";
                parameter.SqlValue = claim.Action;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@role";
                parameter.SqlValue = claim.Role;
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.ParameterName = "@remark";
                parameter.SqlValue = claim.Remarks;
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


        public async Task<Tuple<string, List<ClaimActionHistory>>> GetClaimHistory(int ClaimId)
        {
            string message = string.Empty;
            List<ClaimActionHistory> claimHistorys = new List<ClaimActionHistory>();
            try
            {
                var command = _context.Database.GetDbConnection().CreateCommand();
                command.CommandText = "USP_GET_CLAIM_ACTION_HISTORY";
                command.CommandType = CommandType.StoredProcedure;

                var parameter = new SqlParameter();


                parameter = new SqlParameter();
                parameter.ParameterName = "@claimId";
                parameter.SqlValue = ClaimId;
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                command.Parameters.Add(parameter);

                _context.Database.OpenConnection();
                var result = command.ExecuteReader();

                while (result.Read())
                {
                    ClaimActionHistory claimHistory = new ClaimActionHistory();
                    claimHistory.Action = result["Action"].ToString();
                    claimHistory.ActionBy = result["Name"].ToString();
                    claimHistory.Remark = result["Remark"].ToString();
                    claimHistory.ActionDt = result["Action Date"].ToString();
                    claimHistorys.Add(claimHistory);
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

            return new Tuple<string, List<ClaimActionHistory>>(message, claimHistorys);
        }
    }
}
