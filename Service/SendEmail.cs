using Client;
using Newtonsoft.Json;
using Polly;
using Service.Contracts;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SendEmail :ISendEmail
    {
        private const string url = "https://us-central1-randomfails.cloudfunctions.net/submitEmail";

        public async Task<HttpResponseMessage> SendEmailAsync(ClientCredentials model)
        {
            var converted = JsonConvert.SerializeObject(model, Formatting.Indented);
            var policy = Policy
                             .Handle<Exception>()
                             .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                             .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                      + TimeSpan.FromMilliseconds(1000));

            using (var httpClient = new HttpClient())
            {
                var postAsync = await policy.ExecuteAsync(async () => await httpClient.PostAsync(url,
           new StringContent(converted, Encoding.UTF8, "application/json")));

                return postAsync;
            }
        }
    }
}
