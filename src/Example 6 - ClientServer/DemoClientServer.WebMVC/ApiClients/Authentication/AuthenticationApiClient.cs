using DemoClientServer.WebMVC.Requests;
using System.Text;
using System.Text.Json;

namespace DemoClientServer.WebMVC.ApiClients.Authentication;

public class AuthenticationApiClient : IAuthenticationApiClient
{
    private readonly HttpClient _httpClient;

    public AuthenticationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetAccessTokenResponse> GetAccessTokenAsync(GetAccessTokenRequest request)
    {
        var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/authentication/GetAccessToken", requestContent);
        response.EnsureSuccessStatusCode();

        var token = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<GetAccessTokenResponse>(token, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
    }
}
