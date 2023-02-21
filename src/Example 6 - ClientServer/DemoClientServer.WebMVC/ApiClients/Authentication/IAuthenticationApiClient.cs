using DemoClientServer.WebMVC.Requests;

namespace DemoClientServer.WebMVC.ApiClients.Authentication;

public interface IAuthenticationApiClient
{
    Task<GetAccessTokenResponse> GetAccessTokenAsync(GetAccessTokenRequest request);
}
