namespace DEMO.API.D365.Services.Customer.Data.Responses
{
    using DEMO.API.D365.Services.Common;

    public class UpsertCustomerResponse : BaseServiceResponseMessage
    {
        public Guid pkCustomer { get; }


        public UpsertCustomerResponse(OperationType operation, IEnumerable<Error> errors, bool success = false, string message = null) : base(operation, errors, success, message)
        {

        }

        public UpsertCustomerResponse(OperationType operation, Guid idCustomer, bool success = false, string message = "") : base(operation, success, message)
        {
            pkCustomer = idCustomer;
        }
    }
}
