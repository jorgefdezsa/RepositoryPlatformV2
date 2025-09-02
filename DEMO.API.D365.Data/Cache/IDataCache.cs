namespace DEMO.API.D365.Data.Cache
{
    using DEMO.API.Core.Domain.Entities;
    using DEMO.API.Core.Domain.Entities.Masters;

    public interface IDataCache
    {
        Task<Country?> GetCountryByCode(string countryCode);
        Task<LegalEntity?> GetLegalEntityByCode(string code);
    }
}
