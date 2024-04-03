using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Runtime.ConstrainedExecution;
using static System.Net.WebRequestMethods;

namespace DotnetAiTranslatorApi.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly string? _key;
        private static readonly string? _translatorEndpoint = Environment.GetEnvironmentVariable("TRANSLATOR_ENDPOINT");
        private static readonly string _supportedLangsEndpoint = "https://api.cognitive.microsofttranslator.com";

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
                    _key = client.GetSecret("TRANSLATOR-KEY").Value.Value;
                } 
                catch(AuthenticationFailedException e)
                {
                    Console.WriteLine($"Authentication failed: {e.Message}");
                }
            }
        }

        public bool IsKeyAndEndpointValid()
        {
            if (_key == null || _key == "" || _translatorEndpoint == null || _translatorEndpoint == "")
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
            HttpClient client = new HttpClient();
            UriBuilder builder = new UriBuilder(_supportedLangsEndpoint)
            {
                Path = "/languages",
                Query = "?api-version=3.0"
            };
            client.BaseAddress = builder.Uri;

            // TODO: implement model for this response
            var response = await client.GetStringAsync("");

            return response; 
        }
    }
}
