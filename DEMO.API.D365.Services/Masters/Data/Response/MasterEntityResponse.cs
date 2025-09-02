namespace DEMO.API.D365.Services.Masters.Data.Response
{
    using DEMO.API.D365.Services.Common;

    public class MasterEntityResponse : BaseServiceResponseMessage
    {

        public Guid PkMaster { get; set; }


        public MasterEntityResponse(OperationType operation, IEnumerable<Error> errors, bool success = false, string message = null) : base(operation, errors, success, message)
        {

        }

        public MasterEntityResponse(OperationType operation, Guid idCrm, bool success = false, string message = "") : base(operation, success, message)
        {
            PkMaster = idCrm;
        }
    }
}
