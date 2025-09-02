using DEMO.API.SQL.Integrations.Enums;
using DEMO.API.SQL.Integrations.Models;
using Microsoft.EntityFrameworkCore;

namespace DEMO.API.SQL.Integrations.Repositories
{
    public class LogIntegrationRepository : ILogIntegrationRepository
    {
        private readonly SqlDbDemoContext _context;
        public LogIntegrationRepository(SqlDbDemoContext context)
        {
            _context = context;
        }


        public async Task<IntegrationInputLog> CreateIntegrationInputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, IntegrationOperationEnum operation, string? jsonRequest = null, string? jsonResponse = null, string? responseCode = null, string? responseBody = null)
        {
            IntegrationInputLog intLog = new IntegrationInputLog();

            int? catalogId = (await _context.IntegrationCatalogs.FirstOrDefaultAsync(x => x.Name == catalog.ToString()))?.Id;
            int? triggerId = (await _context.IntegrationTriggers.FirstOrDefaultAsync(x => x.Name == trigger.ToString()))?.Id;
            int? operationId = (await _context.IntegrationOperations.FirstOrDefaultAsync(x => x.Name == operation.ToString()))?.Id;

            if (catalogId != null && triggerId != null && operationId != null)
            {

                intLog.IntegrationCatalogId = catalogId.Value;
                intLog.IntegrationTriggerId = triggerId.Value;
                intLog.IntegrationOperationId = operationId.Value;
                intLog.IntegrationDate = DateTime.UtcNow;
                intLog.JsonRequest = jsonRequest;
                intLog.JsonResponse = jsonResponse;
                intLog.ResponseCode = responseCode;

                await _context.IntegrationInputLogs.AddAsync(intLog);
                await _context.SaveChangesAsync();
            }

            return intLog;
        }

        public async Task<IntegrationInputLog> CreateIntegrationInputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, IntegrationOperationEnum operation, int retryNumber, string jsonRequest, string jsonResponse, string responseCode,
            int? jobNumber = null, string? pageNumber = null)
        {
            IntegrationInputLog intLog = new IntegrationInputLog();

            int? catalogId = (await _context.IntegrationCatalogs.FirstOrDefaultAsync(x => x.Name == catalog.ToString()))?.Id;
            int? triggerId = (await _context.IntegrationTriggers.FirstOrDefaultAsync(x => x.Name == trigger.ToString()))?.Id;
            int? operationId = (await _context.IntegrationOperations.FirstOrDefaultAsync(x => x.Name == operation.ToString()))?.Id;

            if (catalogId != null && triggerId != null && operationId != null)
            {

                intLog.IntegrationCatalogId = catalogId.Value;
                intLog.IntegrationTriggerId = triggerId.Value;
                intLog.IntegrationOperationId = operationId.Value;
                intLog.IntegrationDate = DateTime.UtcNow;
                intLog.JsonRequest = jsonRequest;
                intLog.JsonResponse = jsonResponse;
                intLog.ResponseCode = responseCode;
                intLog.RetryNumber = retryNumber;
                intLog.JobNumber = jobNumber;
                intLog.PageNumber = pageNumber;

                await _context.IntegrationInputLogs.AddAsync(intLog);
                await _context.SaveChangesAsync();
            }

            return intLog;
        }

        public async Task<IntegrationInputLog> CreateIntegrationInputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, int jobNumber, string pageNumber, int NumRequests, string? jsonRequest = null, string? jsonResponse = null, string? responseCode = null, string? responseBody = null)
        {
            IntegrationInputLog intLog = new IntegrationInputLog();
            int? catalogId = (await _context.IntegrationCatalogs.FirstOrDefaultAsync(x => x.Name == catalog.ToString()))?.Id;
            int? triggerId = (await _context.IntegrationTriggers.FirstOrDefaultAsync(x => x.Name == trigger.ToString()))?.Id;

            if (catalogId != null && triggerId != null)
            {
                intLog.IntegrationCatalogId = catalogId.Value;
                intLog.IntegrationTriggerId = triggerId.Value;
                intLog.IntegrationOperationId = null;
                intLog.IntegrationDate = DateTime.UtcNow;
                intLog.JobNumber = jobNumber;
                intLog.PageNumber = pageNumber;
                intLog.NumberRegisters = NumRequests;
                intLog.JsonRequest = jsonRequest;
                intLog.JsonResponse = jsonResponse;
                intLog.ResponseCode = responseCode;

                await _context.IntegrationInputLogs.AddAsync(intLog);

                await _context.IntegrationCatJobs.AddAsync(new IntegrationCatJob()
                {
                    IntegrationCatalogId = catalogId.Value,
                    JobNumber = jobNumber + 1,
                    ChangeDate = DateTime.UtcNow
                });

                await _context.SaveChangesAsync();

            }

            return intLog;
        }

        public async Task<IntegrationOutputLog> CreateIntegrationOutputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, int retryNumber, string jsonRequest, string jsonResponse, string responseCode, string endpoint)
        {
            IntegrationOutputLog intLog = new IntegrationOutputLog();
            int? catalogId = (await _context.IntegrationCatalogs.FirstOrDefaultAsync(x => x.Name == catalog.ToString()))?.Id;
            int? triggerId = (await _context.IntegrationTriggers.FirstOrDefaultAsync(x => x.Name == trigger.ToString()))?.Id;

            if (catalogId != null && triggerId != null)
            {
                intLog.IntegrationCatalogId = catalogId.Value;
                intLog.IntegrationTriggerId = triggerId.Value;
                intLog.IntegrationDate = DateTime.UtcNow;
                intLog.RetryNumber = retryNumber;
                intLog.JsonRequest = jsonRequest;
                intLog.JsonResponse = jsonResponse;
                intLog.ResponseCode = responseCode;
                intLog.ExternalEndpoint = endpoint;

                await _context.IntegrationOutputLogs.AddAsync(intLog);

                await _context.SaveChangesAsync();
            }

            return intLog;
        }

        public async Task<DateTime?> GetLastIntegrationDate(IntegrationCatalogEnum catalog)
        {
            DateTime? result = null;
            int? catalogId = (await _context.IntegrationCatalogs.FirstOrDefaultAsync(x => x.Name == catalog.ToString()))?.Id;
            if (catalogId != null)
            {
                var intLog = await _context.IntegrationInputLogs.Where(x => x.IntegrationCatalogId == (int)IntegrationCatalogEnum.BatchCustomer).OrderByDescending(x => x.IntegrationDate).FirstOrDefaultAsync();
                if (intLog != null)
                {
                    result = intLog.IntegrationDate;
                }
            }
            return result;
        }


        public async Task<int?> GetLastJobNumber(IntegrationCatalogEnum catalog)
        {
            int? result = null;
            int? catalogId = (await _context.IntegrationCatalogs.FirstOrDefaultAsync(x => x.Name == catalog.ToString()))?.Id;
            if (catalogId != null)
            {
                var catJob = await _context.IntegrationCatJobs.Where(x => x.IntegrationCatalogId == catalogId).OrderByDescending(x => x.JobNumber).FirstOrDefaultAsync();
                if (catJob != null)
                {
                    result = catJob.JobNumber;
                }
                else
                {
                    result = 1;
                    await _context.IntegrationCatJobs.AddAsync(new IntegrationCatJob()
                    {
                        IntegrationCatalogId = catalogId.Value,
                        JobNumber = result.Value,
                        ChangeDate = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();
                }
            }
            return result;
        }

    }
}
