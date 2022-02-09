using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PriceObserver.Api.Controllers;

[Route("api/error")]
[ApiController]
public class ExceptionHandlingController : ControllerBase
{
    private readonly ILogger _logger;
    
    public ExceptionHandlingController(ILogger<ExceptionHandlingController> logger)
    {
        _logger = logger;
    }

    [Route("handle")]
    public IActionResult Handle()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        var error = context!.Error;
        var originalPath = context.Path;
        
        const string errorMessageTemplate = @"Api error occured
Exception: {0}
Path: {1}
Inner message: {2}";
        
        _logger.LogError(context.Error, errorMessageTemplate, error.Message, originalPath, error.InnerException);
        
        return Problem($"Error occured. Exception: {error.Message}");
    }
}