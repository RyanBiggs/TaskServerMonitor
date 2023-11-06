namespace TSMonitor2
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    public class TeamsNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _webhookUrl;

        public TeamsNotificationService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _webhookUrl = configuration["TeamsWebhookUrl"]; // Retrieve the webhook URL from your configuration
        }

        public async Task SendNotificationAsync(string message)
        {
            try
            {
                var payload = new
                {
                    text = message
                };

                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_webhookUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle error
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
        }
    }

}
