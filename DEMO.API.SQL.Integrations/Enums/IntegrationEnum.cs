namespace DEMO.API.SQL.Integrations.Enums
{
    public enum IntegrationCatalogEnum
    {
        UpsertEmployee,
        DeleteEmployee,
        UpsertEmployeeCertification,
        UpsertJobRole,
        UpsertCertificationType,
        UpsertOrganizationUnit,
        UpsertCustomer,
        BatchCustomer
    }

    public enum IntegrationSystemEnum
    {
        SYSTEM1,
        SYSTEM2,
        SYSTEM3,
        SYSTEM4,
        SYSTEM5,
        SYSTEM6
    }

    public enum IntegrationProcessEnum
    {
        Process1,
        Process2,
        Process3
    }

    public enum IntegrationTriggerEnum
    {
        HttpTrigger,
        ServiceBusTrigger
    }

    public enum IntegrationOperationEnum
    {
        Create,
        Read,
        Update,
        Delete
    }

}
