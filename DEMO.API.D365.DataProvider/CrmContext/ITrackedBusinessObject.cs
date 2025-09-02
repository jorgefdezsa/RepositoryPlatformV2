namespace DEMO.API.D365.DataProvider.CrmContext
{
    using AutoMapper.Configuration.Annotations;

    public interface ITrackedBusinessObject
    {
#pragma warning disable CA2252 //fix for bug on vs2022 compiler https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2252#when-to-suppress-warnings
        static abstract string LogicalName { get; }
#pragma warning restore  CA2252
        [Ignore]
        Guid? CrmId { get; set; }


    }
}
