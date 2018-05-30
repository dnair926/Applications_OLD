namespace Applications.Core.Business.Services
{

    public interface ICurrentUserService
    {
        ApplicationPerson GetCurrentUserInfo();
    }
}