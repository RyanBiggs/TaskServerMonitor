using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TSMonitor2
{
    public class TeamsService
    {
        private readonly string clientId = "fce175b5-842f-4c6a-9779-4cc579e365e1";
        private readonly string clientSecret = "178c97bf-751d-40f7-af1b-57f207284e57";
        private readonly string tenantId = "dbd7c2ea-34f0-46fa-9928-bf150dc2d70b";
        private readonly string teamsAppId = "59361c30-541a-4dc1-b210-19cc19755449";
        private readonly string channelId = "19%3aRbsyt93l7Cu4YSE87jk5iku-SotlmaIlLFrfmFqUo8A1%40thread.tacv2";

        public async Task SendMessageToTeamsChannel(string messageContent)
        {
            var httpClient = new HttpClient();

            // Get an access token using client credentials grant
            var tokenUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            tokenRequest.Content = new StringContent($"client_id={clientId}&scope=https://graph.microsoft.com/.default&client_secret={clientSecret}&grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var tokenResponse = await httpClient.SendAsync(tokenRequest);
            var tokenData = await tokenResponse.Content.ReadAsAsync<TokenResponse>();

            // Send the message to the Teams channel
            var apiUrl = $"https://graph.microsoft.com/v1.0/teams/{teamsAppId}/channels/{channelId}/messages";
            var messageRequest = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            messageRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenData.AccessToken);
            messageRequest.Content = new StringContent($"{{\"content\":\"{messageContent}\"}}", Encoding.UTF8, "application/json");
            var messageResponse = await httpClient.SendAsync(messageRequest);

            if (messageResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Message sent successfully!");
            }
            else
            {
                Console.WriteLine("Error sending message: " + await messageResponse.Content.ReadAsStringAsync());
            }
        }
    }

    // Helper class to deserialize token response
    public class TokenResponse
    {
        public string AccessToken { get; set; }
    }
}
