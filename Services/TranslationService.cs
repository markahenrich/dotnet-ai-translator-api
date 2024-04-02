using System.Runtime.ConstrainedExecution;

namespace DotnetAiTranslatorApi.Services
{
    public class TranslationService : ITranslationService
    {
        private static readonly string? _key = Environment.GetEnvironmentVariable("TRANSLATOR_KEY");
        private static readonly string? _endpoint = Environment.GetEnvironmentVariable("TRANSLATOR_ENDPOINT"); 

        public bool IsKeyAndEndpointValid()
        {
            if (_key == null || _key == "" || _endpoint == null || _endpoint == "")
            {
                return false;
            }

            return true;
        }
    }
}
