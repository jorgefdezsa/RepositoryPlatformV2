namespace DEMO.API.SQL.Repositories
{
    using DEMO.API.Functions.Helpers.Types.Config;
    using DEMO.API.Functions.Helpers.Types.Extensions;
    using System.Data;
    using System.Data.SqlClient;

    public class LogRequestRepository : ILogRequestRepository
    {
        private readonly IAppConfiguration _config;
        public LogRequestRepository(IAppConfiguration config)
        {
            _config = config;
        }

        #region Public Methods
        public long? CreateLogRequest(string path, string json, string method, long? requestOriginal)
        {
            return UpsertLogRequest_Internal(null, path, json, method, null, requestOriginal);

        }

        public void UpdateLogRequest(long id, int httpStatus)
        {
            UpsertLogRequest_Internal(id, string.Empty, string.Empty, string.Empty, httpStatus, null);
        }

        #endregion

        #region Private

        private long? UpsertLogRequest_Internal(long? id, string path, string method, string json, int? httpStatus,
            long? requestOriginal)
        {
            using (SqlConnection _conn = new SqlConnection(_config.GetConfiguration().SQLConnectionString))
            {
                _conn.Open();

                using (var cmd = new SqlCommand("[dbo].[upsert_request_input]", _conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@requestId", SqlDbType.BigInt);
                    if (id.HasValue)
                        cmd.Parameters["@requestId"].Value = id.Value;
                    else
                        cmd.Parameters["@requestId"].Value = DBNull.Value;

                    cmd.Parameters.Add("@json", SqlDbType.NVarChar, 4000);
                    if (!string.IsNullOrEmpty(json))
                        cmd.Parameters["@json"].Value = json;
                    else
                        cmd.Parameters["@json"].Value = DBNull.Value;

                    cmd.Parameters.Add("@path", SqlDbType.NVarChar, 150);
                    if (!string.IsNullOrEmpty(path))
                        cmd.Parameters["@path"].Value = path;
                    else
                        cmd.Parameters["@path"].Value = DBNull.Value;

                    cmd.Parameters.Add("@method", SqlDbType.NVarChar, 50);
                    if (!string.IsNullOrEmpty(method))
                        cmd.Parameters["@method"].Value = method;
                    else
                        cmd.Parameters["@method"].Value = DBNull.Value;

                    cmd.Parameters.Add("@httpstatus", SqlDbType.Int);
                    if (httpStatus.HasValue)
                        cmd.Parameters["@httpstatus"].Value = httpStatus.Value;
                    else
                        cmd.Parameters["@httpstatus"].Value = DBNull.Value;

                    cmd.Parameters.Add("@requestoriginal", SqlDbType.BigInt);
                    if (requestOriginal.HasValue)
                        cmd.Parameters["@requestoriginal"].Value = requestOriginal.Value;
                    else
                        cmd.Parameters["@requestoriginal"].Value = DBNull.Value;

                    var resultBD = cmd.ExecuteScalar();
                    if (resultBD != null)
                        return Convert.ToInt64(resultBD);
                    else return null;
                }

            }
        }

        public void CreateLogError(long requestId, Exception ex)
        {
            try
            {
                using (SqlConnection _conn = new SqlConnection(_config.GetConfiguration().SQLConnectionString))
                {
                    _conn.Open();

                    using (var cmd = new SqlCommand("[dbo].[log_error]", _conn))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        //RequestId
                        cmd.Parameters.Add("@request_id", SqlDbType.BigInt);
                        cmd.Parameters["@request_id"].Value = requestId;


                        //error
                        cmd.Parameters.Add("@error", SqlDbType.NVarChar, 4000);
                        cmd.Parameters["@error"].Value = ex.Message.Truncate(4000);

                        var resultBD = cmd.ExecuteNonQuery();
                    }

                }
            }
            catch 
            {
            }
        }


        #endregion

    }
}
