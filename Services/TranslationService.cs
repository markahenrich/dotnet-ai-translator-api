using System.Runtime.ConstrainedExecution;
using static System.Net.WebRequestMethods;

namespace DotnetAiTranslatorApi.Services
{
    public class TranslationService : ITranslationService
    {
        private static readonly string? _key = Environment.GetEnvironmentVariable("TRANSLATOR_KEY");
        private static readonly string? _endpoint = Environment.GetEnvironmentVariable("TRANSLATOR_ENDPOINT");
        private static readonly string _supportedLangsEndpoint = "https://api.cognitive.microsofttranslator.com";

        public bool IsKeyAndEndpointValid()
        {
            if (_key == null || _key == "" || _endpoint == null || _endpoint == "")
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
