namespace DEMO.API.D365.Services.Common
{
    public interface IPresenter<in TUseCaseResponse>
    { 
        Task<bool> Handle(TUseCaseResponse response);
    }
}
