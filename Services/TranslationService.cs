using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Newtonsoft.Json;
using System.Text;

namespace DotnetAiTranslatorApi.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly KeyVaultSecret? _key;
        private static readonly string? _translatorEndpoint = Environment.GetEnvironmentVariable("TRANSLATOR_ENDPOINT");

        public TranslationService()
        {
            var keyVaultEndpoint = Environment.GetEnvironmentVariable("KEY_VAULT_ENDPOINT");
            if (keyVaultEndpoint != null && keyVaultEndpoint != "")
            {
                /*
                 * If running into issues with DefaultAzureCredential:
                 * 
                 * Inside Visual Studio: go to Tools > Options > Azure Service Authentication > Account Selection
                 * and select the correct account. 
                 * 
                 * If that doesn't work use Developer Powershell and run command "az login", then authenticate with Azure
                 * 
                 */
                var client = new SecretClient(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
                try
                {
                    _key = client.GetSecret("TRANSLATOR-KEY").Value;
                } 
                catch(AuthenticationFailedException e)
                {
                    Console.WriteLine($"Authentication failed: {e.Message}");
                }
            }
        }

        public bool IsKeyAndEndpointValid()
        {
            if (_key == null || _key.Value == "" || _translatorEndpoint == null || _translatorEndpoint == "")
            {
                return false;
            }

            return true;
        }

        /*
         * GET languages
         * 
         * Get the set of languages currently supported by operations of the Translator
         * https://learn.microsoft.com/en-us/azure/ai-services/translator/reference/v3-0-languages
         * 
         * endpoint = https://api.cognitive.microsofttranslator.com/languages
         * 
         * Required Request Params:
         * 1. api-version
         *
         * example: https://api.cognitive.microsofttranslator.com/languages?api-version=3.0
         * 
         */
        public async Task<string> GetSupportedLanguages()
        {
            using (var client = new HttpClient())
            {
                string route = "/languages?api-version=3.0";

                client.BaseAddress = new Uri(_translatorEndpoint + route);

                // TODO: implement model for this response
                var response = await client.GetStringAsync("");

                return response;
            }
        }

        public async Task<object?> GetTranslation(string text, string from, string to)
        {
            if (!IsKeyAndEndpointValid())
            {
                return null;
            }

            string route = $"/translate?api-version=3.0&from={from}&to={to}";

            Object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(_translatorEndpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", _key.Value);
                request.Headers.Add("Ocp-Apim-Subscription-Region", "westus2"); 

                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                // TODO: implement model for this response 
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
