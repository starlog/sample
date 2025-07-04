using Microsoft.AspNetCore.Mvc;
using WeddingInvitationApi.Services;
using WeddingInvitationApi.DTOs;

namespace WeddingInvitationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly IWeddingInvitationService _service;
    private readonly ILogger<TemplatesController> _logger;

    public TemplatesController(IWeddingInvitationService service, ILogger<TemplatesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Get all available templates
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<TemplatePreview>>>> GetTemplates()
    {
        try
        {
            var templates = await _service.GetAvailableTemplatesAsync();
            return Ok(ApiResponse<List<TemplatePreview>>.SuccessResponse(templates));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving templates");
            return StatusCode(500, ApiResponse<List<TemplatePreview>>.ErrorResponse("Failed to retrieve templates"));
        }
    }
}