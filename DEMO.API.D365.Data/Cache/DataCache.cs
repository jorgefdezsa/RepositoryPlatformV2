namespace DEMO.API.D365.Data.Cache
{
    using DEMO.API.Core.Domain.Entities;
    using DEMO.API.Core.Domain.Entities.Masters;
    using DEMO.API.D365.Data.Data.Repositories;
    using LazyCache;
    using Microsoft.Extensions.DependencyInjection;

    public class DataCache : IDataCache
    {
        private readonly IAppCache _appCache;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DataCache(IServiceScopeFactory serviceScopeFactory)
        {
            _appCache = new CachingService();
            _serviceScopeFactory = serviceScopeFactory;
        }


        #region Public

        public async Task<Country?> GetCountryByCode(string countryCode)
        {
            var countries = await GetCountries();
            if (countries.Any())
            {
                return countries.FirstOrDefault(p => !string.IsNullOrEmpty(p.Code) && p.Code.Equals(countryCode));
            }
            else
                return null;

        }

        public async Task<LegalEntity?> GetLegalEntityByCode(string code)
        {
            var legalEntities = await GetLegalEntities();
            if (legalEntities.Any())
            {
                return legalEntities.FirstOrDefault(p => !string.IsNullOrEmpty(p.Code) && p.Code.Equals(code));
            }
            else
                return null;
        }

        #endregion

        #region Private

        private async Task<IEnumerable<Country>> GetCountries()
        {
            var items = await _appCache.GetAsync<IList<Country>>("Countries");
            if (items == null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetService<IRepository<Country>>();
                    return await _appCache.GetOrAddAsync("Countries", repo.GetAllActiveAsync, DateTimeOffset.Now.AddHours(8));
                }
            }
            else
                return items;
        }

        private async Task<IEnumerable<LegalEntity>> GetLegalEntities()
        {
            var items = await _appCache.GetAsync<IList<LegalEntity>>("jfs_legalentities");
            if (items == null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetService<IRepository<LegalEntity>>();
                    return await _appCache.GetOrAddAsync("jfs_legalentities", repo.GetAllActiveAsync, DateTimeOffset.Now.AddHours(8));
                }
            }
            else
                return items;
        }

        #endregion
    }
}
