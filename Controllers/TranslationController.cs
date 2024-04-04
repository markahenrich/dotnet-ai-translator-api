using DotnetAiTranslatorApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAiTranslatorApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly ILogger<TranslationController> _logger;
        private readonly ITranslationService _translationService; 

        public TranslationController(ILogger<TranslationController> logger, ITranslationService translationService)
        {
            _logger = logger;
            _translationService = translationService; 
        }

        [HttpGet("supported-langs")]
        public Task<string> GetSupportedLanguages()
        {
            
            return _translationService.GetSupportedLanguages(); 
        }

        [HttpGet]
        public Task<object?> GetTranslation(string text, string from, string to)
        {
            // TODO: Sanitize input 

            return _translationService.GetTranslation(text, from, to);
        }
    }
}
