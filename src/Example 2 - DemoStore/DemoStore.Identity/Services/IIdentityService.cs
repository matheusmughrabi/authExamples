using DemoStore.Identity.Services.Models;

namespace DemoStore.Identity.Services
{
    public interface IIdentityService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
        Task<LoginUserResponse> LoginUser(LoginUserRequest request);
    }
}
