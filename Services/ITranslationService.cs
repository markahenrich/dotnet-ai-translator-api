namespace DotnetAiTranslatorApi.Services
{
    public interface ITranslationService
    {
        public bool IsKeyAndEndpointValid();

        public Task<string> GetSupportedLanguages();
    }
}
