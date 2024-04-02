using DotnetAiTranslatorApi.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public Task<string> GetSupportedLanguages()
        {
            
            return _translationService.GetSupportedLanguages(); 
        }
    }
}
