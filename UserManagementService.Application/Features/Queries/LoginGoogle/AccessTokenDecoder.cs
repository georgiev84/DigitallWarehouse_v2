using System.Text.Json;

namespace Warehouse.Application.Features.Queries.LoginGoogle;

public class AccessTokenDecoder
{
    private const string TokenInfoEndpoint = "https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={0}";

    //public static async Task<string> DecodeAccessTokenAsync(string accessToken)
    //{
    //    try
    //    {
    //        using (var httpClient = new HttpClient())
    //        {
    //            var response = await httpClient.GetAsync(string.Format(TokenInfoEndpoint, accessToken));

    //            if (response.IsSuccessStatusCode)
    //            {
    //                var content = await response.Content.ReadAsStringAsync();
    //                var tokenInfo = JObject.Parse(content);

    //                string email = tokenInfo.Value<string>("email");
    //                return email;
    //            }
    //            else
    //            {
    //                throw new Exception($"Failed to decode access token: {response.ReasonPhrase}");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception($"Error decoding access token: {ex.Message}");
    //    }

    //}

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
            throw; // Rethrow the exception for handling elsewhere if necessary
        }
    }

    public class TokenInfo
    {
        public string email { get; set; }
    }
}