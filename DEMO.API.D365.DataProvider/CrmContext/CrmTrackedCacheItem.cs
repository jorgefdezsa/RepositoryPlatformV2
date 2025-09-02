namespace DEMO.API.D365.DataProvider.CrmContext
{
    using AutoMapper;
    using Microsoft.Xrm.Sdk;

    internal class CrmTrackedCacheItem
    {
        public CrmTrackedCacheItem(Entity? entity, ITrackedBusinessObject business, IMapper mapperE2B, IMapper mapperB2E)
        {
            Entity = entity;
            Business = business;
            MapperE2B = mapperE2B;
            MapperB2E = mapperB2E;
        }
        public Entity? Entity { get; set; }
        public ITrackedBusinessObject Business { get; set; }
        public IMapper MapperE2B { get; private set; }
        public IMapper MapperB2E { get; private set; }
    }
}
