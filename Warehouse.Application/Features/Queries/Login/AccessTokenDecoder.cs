using Newtonsoft.Json.Linq;

namespace Warehouse.Application.Features.Queries.Login;
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
                    var content = await response.Content.ReadAsStringAsync();
                    var tokenInfo = JObject.Parse(content);

                    string email = tokenInfo.Value<string>("email");
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
            throw new Exception($"Error decoding access token: {ex.Message}");
        }

    }
}
