namespace DEMO.API.Functions.Presenters
{
    using DEMO.API.D365.Services.Common;
    using DEMO.API.Functions.Common.Wrapper;

    public interface IApiPresenter<in TUseCaseResponse> : IPresenter<TUseCaseResponse>
    {
        ResponseMessage ContentResult { get; }
    }
}
