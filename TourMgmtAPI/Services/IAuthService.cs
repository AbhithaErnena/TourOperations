using TourMgmtAPI.Accounts;

namespace TourMgmtAPI.Services
{
    public interface IAuthService
    {
        (int, string) Register(RegisterModel model);
        (int, string) Login(LoginModel model);
    }
}
