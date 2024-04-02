using DotnetAiTranslatorApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAiTranslatorApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ITranslationService _translationService; 

        public TranslationController(ILogger logger, ITranslationService translationService)
        {
            _logger = logger;
            _translationService = translationService; 
        }

        [HttpGet]
        public string GetTestTranslation()
        {
            
            return "hello world"; 
        }
    }
}
