namespace DotnetAiTranslatorApi.Services
{
    public interface ITranslationService
    {
        public bool IsKeyAndEndpointValid();

        public Task<string> GetSupportedLanguages();

        public Task<object?> GetTranslation(string text, string from, string to);
    }
}
