using System.Text.Json;

namespace UserManagementService.Application.Features.Queries.LoginGoogle;

public class AccessTokenDecoder
{
    private const string TokenInfoEndpoint = "https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={0}";

    public static async Task<string> DecodeAccessTokenAsync(string accessToken)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(string.Format(TokenInfoEndpoint, accessToken));

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var tokenInfo = await JsonSerializer.DeserializeAsync<TokenInfo>(contentStream);

                    string email = tokenInfo.email;
                    return email;
                }
                else
                {
                    throw new Exception($"Failed to decode access token: {response.ReasonPhrase}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; 
        }
    }

    public class TokenInfo
    {
        public string email { get; set; }
    }
}