namespace DEMO.API.D365.Services.Customer
{
    using AutoMapper;
    using DEMO.API.Core.Domain.Entities;
    using DEMO.API.Core.Domain.Entities.Customer;
    using DEMO.API.D365.Data.Cache;
    using DEMO.API.D365.Data.Data.Repositories.Customer;
    using DEMO.API.D365.Services.Common;
    using DEMO.API.D365.Services.Customer.Data.Requests;
    using DEMO.API.D365.Services.Customer.Data.Responses;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDataCache _cache;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IDataCache cache)
        {
            _customerRepository = customerRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpsertCustomerRequest message, IPresenter<UpsertCustomerResponse> presenter)
        {
            var currentCustomer = await _customerRepository.GetCustomerByErpId(message.IdErp);
            LegalEntity? legalEntity = null;
            if (!string.IsNullOrEmpty(message.LegalEntityId))
                legalEntity = await _cache.GetLegalEntityByCode(message.LegalEntityId);
            if (currentCustomer != null) 
            {
                if (!currentCustomer.CanIntegrate(message.IntegrationTimeStamp))
                {
                    var responseIgnoreMessage = new UpsertCustomerResponse(OperationType.Update, currentCustomer.CrmId.Value, true);
                    await presenter.Handle(responseIgnoreMessage);
                    return true;
                }

                var mapCustomer = _mapper.Map<Customer>(message);
                mapCustomer.LegalEntity = legalEntity == null ? null : legalEntity.CrmId;
                currentCustomer.Merge(mapCustomer);

                await _customerRepository.SaveAsync();
                var response = new UpsertCustomerResponse(OperationType.Update, currentCustomer.CrmId.Value, true);
                await presenter.Handle(response);
                return true;
            }
            else 
            {
                var mapCustomer = _mapper.Map<Customer>(message);
                mapCustomer.LegalEntity = legalEntity == null ? null : legalEntity.CrmId;
                _customerRepository.Create(mapCustomer);
                await _customerRepository.SaveAsync();

                var response = new UpsertCustomerResponse(OperationType.Create, mapCustomer.CrmId.Value, true);
                await presenter.Handle(response);
                return true;
            }
        }
    }
}
