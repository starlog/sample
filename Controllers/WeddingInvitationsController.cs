using Microsoft.AspNetCore.Mvc;
using WeddingInvitationApi.Models;
using WeddingInvitationApi.Services;
using WeddingInvitationApi.DTOs;

namespace WeddingInvitationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeddingInvitationsController : ControllerBase
{
    private readonly IWeddingInvitationService _service;
    private readonly ILogger<WeddingInvitationsController> _logger;

    public WeddingInvitationsController(IWeddingInvitationService service, ILogger<WeddingInvitationsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Get all wedding invitations
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<WeddingInvitation>>>> GetAllInvitations()
    {
        try
        {
            var invitations = await _service.GetAllInvitationsAsync();
            return Ok(ApiResponse<List<WeddingInvitation>>.SuccessResponse(invitations));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all invitations");
            return StatusCode(500, ApiResponse<List<WeddingInvitation>>.ErrorResponse("Failed to retrieve invitations"));
        }
    }

    /// <summary>
    /// Get wedding invitation by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<WeddingInvitation>>> GetInvitation(string id)
    {
        try
        {
            var invitation = await _service.GetInvitationByIdAsync(id);
            if (invitation == null)
            {
                return NotFound(ApiResponse<WeddingInvitation>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<WeddingInvitation>.SuccessResponse(invitation));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving invitation {Id}", id);
            return StatusCode(500, ApiResponse<WeddingInvitation>.ErrorResponse("Failed to retrieve invitation"));
        }
    }

    /// <summary>
    /// Create a new wedding invitation
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ApiResponse<WeddingInvitation>>> CreateInvitation([FromBody] WeddingInvitationData data)
    {
        try
        {
            var invitation = await _service.CreateInvitationAsync(data);
            return CreatedAtAction(nameof(GetInvitation), new { id = invitation.Id }, 
                ApiResponse<WeddingInvitation>.SuccessResponse(invitation, "Wedding invitation created successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating invitation");
            return StatusCode(500, ApiResponse<WeddingInvitation>.ErrorResponse("Failed to create invitation"));
        }
    }

    /// <summary>
    /// Update wedding invitation
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<WeddingInvitation>>> UpdateInvitation(string id, [FromBody] WeddingInvitationData data)
    {
        try
        {
            var invitation = await _service.UpdateInvitationAsync(id, data);
            if (invitation == null)
            {
                return NotFound(ApiResponse<WeddingInvitation>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<WeddingInvitation>.SuccessResponse(invitation, "Wedding invitation updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invitation {Id}", id);
            return StatusCode(500, ApiResponse<WeddingInvitation>.ErrorResponse("Failed to update invitation"));
        }
    }

    /// <summary>
    /// Delete wedding invitation
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteInvitation(string id)
    {
        try
        {
            var deleted = await _service.DeleteInvitationAsync(id);
            if (!deleted)
            {
                return NotFound(ApiResponse.ErrorResult("Wedding invitation not found"));
            }

            return Ok(ApiResponse.SuccessResult("Wedding invitation deleted successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting invitation {Id}", id);
            return StatusCode(500, ApiResponse.ErrorResult("Failed to delete invitation"));
        }
    }

    /// <summary>
    /// Get template configuration
    /// </summary>
    [HttpGet("{id}/template")]
    public async Task<ActionResult<ApiResponse<Template>>> GetTemplate(string id)
    {
        try
        {
            var template = await _service.GetTemplateAsync(id);
            if (template == null)
            {
                return NotFound(ApiResponse<Template>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<Template>.SuccessResponse(template));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving template for invitation {Id}", id);
            return StatusCode(500, ApiResponse<Template>.ErrorResponse("Failed to retrieve template"));
        }
    }

    /// <summary>
    /// Update template configuration
    /// </summary>
    [HttpPut("{id}/template")]
    public async Task<ActionResult<ApiResponse<Template>>> UpdateTemplate(string id, [FromBody] Template template)
    {
        try
        {
            var updatedTemplate = await _service.UpdateTemplateAsync(id, template);
            if (updatedTemplate == null)
            {
                return NotFound(ApiResponse<Template>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<Template>.SuccessResponse(updatedTemplate, "Template updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating template for invitation {Id}", id);
            return StatusCode(500, ApiResponse<Template>.ErrorResponse("Failed to update template"));
        }
    }

    /// <summary>
    /// Get font configuration
    /// </summary>
    [HttpGet("{id}/fonts")]
    public async Task<ActionResult<ApiResponse<Fonts>>> GetFonts(string id)
    {
        try
        {
            var fonts = await _service.GetFontsAsync(id);
            if (fonts == null)
            {
                return NotFound(ApiResponse<Fonts>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<Fonts>.SuccessResponse(fonts));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving fonts for invitation {Id}", id);
            return StatusCode(500, ApiResponse<Fonts>.ErrorResponse("Failed to retrieve fonts"));
        }
    }

    /// <summary>
    /// Update font configuration
    /// </summary>
    [HttpPut("{id}/fonts")]
    public async Task<ActionResult<ApiResponse<Fonts>>> UpdateFonts(string id, [FromBody] Fonts fonts)
    {
        try
        {
            var updatedFonts = await _service.UpdateFontsAsync(id, fonts);
            if (updatedFonts == null)
            {
                return NotFound(ApiResponse<Fonts>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<Fonts>.SuccessResponse(updatedFonts, "Fonts updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating fonts for invitation {Id}", id);
            return StatusCode(500, ApiResponse<Fonts>.ErrorResponse("Failed to update fonts"));
        }
    }

    /// <summary>
    /// Get content details
    /// </summary>
    [HttpGet("{id}/content")]
    public async Task<ActionResult<ApiResponse<Content>>> GetContent(string id)
    {
        try
        {
            var content = await _service.GetContentAsync(id);
            if (content == null)
            {
                return NotFound(ApiResponse<Content>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<Content>.SuccessResponse(content));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving content for invitation {Id}", id);
            return StatusCode(500, ApiResponse<Content>.ErrorResponse("Failed to retrieve content"));
        }
    }

    /// <summary>
    /// Update content details
    /// </summary>
    [HttpPut("{id}/content")]
    public async Task<ActionResult<ApiResponse<Content>>> UpdateContent(string id, [FromBody] Content content)
    {
        try
        {
            var updatedContent = await _service.UpdateContentAsync(id, content);
            if (updatedContent == null)
            {
                return NotFound(ApiResponse<Content>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<Content>.SuccessResponse(updatedContent, "Content updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating content for invitation {Id}", id);
            return StatusCode(500, ApiResponse<Content>.ErrorResponse("Failed to update content"));
        }
    }

    /// <summary>
    /// Get basic information
    /// </summary>
    [HttpGet("{id}/content/basic-info")]
    public async Task<ActionResult<ApiResponse<BasicInfo>>> GetBasicInfo(string id)
    {
        try
        {
            var basicInfo = await _service.GetBasicInfoAsync(id);
            if (basicInfo == null)
            {
                return NotFound(ApiResponse<BasicInfo>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<BasicInfo>.SuccessResponse(basicInfo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving basic info for invitation {Id}", id);
            return StatusCode(500, ApiResponse<BasicInfo>.ErrorResponse("Failed to retrieve basic info"));
        }
    }

    /// <summary>
    /// Update basic information
    /// </summary>
    [HttpPut("{id}/content/basic-info")]
    public async Task<ActionResult<ApiResponse<BasicInfo>>> UpdateBasicInfo(string id, [FromBody] BasicInfo basicInfo)
    {
        try
        {
            var updatedBasicInfo = await _service.UpdateBasicInfoAsync(id, basicInfo);
            if (updatedBasicInfo == null)
            {
                return NotFound(ApiResponse<BasicInfo>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<BasicInfo>.SuccessResponse(updatedBasicInfo, "Basic info updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating basic info for invitation {Id}", id);
            return StatusCode(500, ApiResponse<BasicInfo>.ErrorResponse("Failed to update basic info"));
        }
    }

    /// <summary>
    /// Get ceremony details
    /// </summary>
    [HttpGet("{id}/content/ceremony-details")]
    public async Task<ActionResult<ApiResponse<CeremonyDetails>>> GetCeremonyDetails(string id)
    {
        try
        {
            var ceremonyDetails = await _service.GetCeremonyDetailsAsync(id);
            if (ceremonyDetails == null)
            {
                return NotFound(ApiResponse<CeremonyDetails>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<CeremonyDetails>.SuccessResponse(ceremonyDetails));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ceremony details for invitation {Id}", id);
            return StatusCode(500, ApiResponse<CeremonyDetails>.ErrorResponse("Failed to retrieve ceremony details"));
        }
    }

    /// <summary>
    /// Update ceremony details
    /// </summary>
    [HttpPut("{id}/content/ceremony-details")]
    public async Task<ActionResult<ApiResponse<CeremonyDetails>>> UpdateCeremonyDetails(string id, [FromBody] CeremonyDetails ceremonyDetails)
    {
        try
        {
            var updatedCeremonyDetails = await _service.UpdateCeremonyDetailsAsync(id, ceremonyDetails);
            if (updatedCeremonyDetails == null)
            {
                return NotFound(ApiResponse<CeremonyDetails>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<CeremonyDetails>.SuccessResponse(updatedCeremonyDetails, "Ceremony details updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating ceremony details for invitation {Id}", id);
            return StatusCode(500, ApiResponse<CeremonyDetails>.ErrorResponse("Failed to update ceremony details"));
        }
    }

    /// <summary>
    /// Get additional information
    /// </summary>
    [HttpGet("{id}/content/additional-info")]
    public async Task<ActionResult<ApiResponse<AdditionalInfo>>> GetAdditionalInfo(string id)
    {
        try
        {
            var additionalInfo = await _service.GetAdditionalInfoAsync(id);
            if (additionalInfo == null)
            {
                return NotFound(ApiResponse<AdditionalInfo>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<AdditionalInfo>.SuccessResponse(additionalInfo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving additional info for invitation {Id}", id);
            return StatusCode(500, ApiResponse<AdditionalInfo>.ErrorResponse("Failed to retrieve additional info"));
        }
    }

    /// <summary>
    /// Update additional information
    /// </summary>
    [HttpPut("{id}/content/additional-info")]
    public async Task<ActionResult<ApiResponse<AdditionalInfo>>> UpdateAdditionalInfo(string id, [FromBody] AdditionalInfo additionalInfo)
    {
        try
        {
            var updatedAdditionalInfo = await _service.UpdateAdditionalInfoAsync(id, additionalInfo);
            if (updatedAdditionalInfo == null)
            {
                return NotFound(ApiResponse<AdditionalInfo>.ErrorResponse("Wedding invitation not found"));
            }

            return Ok(ApiResponse<AdditionalInfo>.SuccessResponse(updatedAdditionalInfo, "Additional info updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating additional info for invitation {Id}", id);
            return StatusCode(500, ApiResponse<AdditionalInfo>.ErrorResponse("Failed to update additional info"));
        }
    }
}