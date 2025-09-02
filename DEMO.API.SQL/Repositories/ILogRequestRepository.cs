namespace DEMO.API.SQL.Repositories
{
    public interface ILogRequestRepository
    {
        public long? CreateLogRequest(string path, string method, string json, long? requestOriginal);


        public void UpdateLogRequest(long id, int httpStatus);
        public void CreateLogError(long requestId, Exception ex);

    }
}
