namespace AgriProductTracker.RestApi.Infrastructure.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GetUserName();
    }
}
