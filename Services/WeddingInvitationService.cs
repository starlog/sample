using WeddingInvitationApi.Models;
using WeddingInvitationApi.DTOs;

namespace WeddingInvitationApi.Services;

public class WeddingInvitationService : IWeddingInvitationService
{
    private readonly IMongoDbService _databaseService;
    private readonly ILogger<WeddingInvitationService> _logger;

    public WeddingInvitationService(IMongoDbService databaseService, ILogger<WeddingInvitationService> logger)
    {
        _databaseService = databaseService;
        _logger = logger;
    }

    public async Task<List<WeddingInvitation>> GetAllInvitationsAsync()
    {
        try
        {
            return await _databaseService.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all invitations");
            throw;
        }
    }

    public async Task<WeddingInvitation?> GetInvitationByIdAsync(string id)
    {
        try
        {
            return await _databaseService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving invitation with id {Id}", id);
            throw;
        }
    }

    public async Task<WeddingInvitation> CreateInvitationAsync(WeddingInvitationData data)
    {
        try
        {
            return await _databaseService.CreateAsync(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating invitation");
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateInvitationAsync(string id, WeddingInvitationData data)
    {
        try
        {
            return await _databaseService.UpdateAsync(id, data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invitation with id {Id}", id);
            throw;
        }
    }

    public async Task<bool> DeleteInvitationAsync(string id)
    {
        try
        {
            return await _databaseService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting invitation with id {Id}", id);
            throw;
        }
    }

    public async Task<Template?> GetTemplateAsync(string id)
    {
        try
        {
            var invitation = await _databaseService.GetByIdAsync(id);
            return invitation?.WeddingInvitationData.Template;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving template for invitation {Id}", id);
            throw;
        }
    }

    public async Task<Template?> UpdateTemplateAsync(string id, Template template)
    {
        try
        {
            var invitation = await _databaseService.UpdateTemplateAsync(id, template);
            return invitation?.WeddingInvitationData.Template;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating template for invitation {Id}", id);
            throw;
        }
    }

    public async Task<Fonts?> GetFontsAsync(string id)
    {
        try
        {
            var invitation = await _databaseService.GetByIdAsync(id);
            return invitation?.WeddingInvitationData.Fonts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving fonts for invitation {Id}", id);
            throw;
        }
    }

    public async Task<Fonts?> UpdateFontsAsync(string id, Fonts fonts)
    {
        try
        {
            var invitation = await _databaseService.UpdateFontsAsync(id, fonts);
            return invitation?.WeddingInvitationData.Fonts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating fonts for invitation {Id}", id);
            throw;
        }
    }

    public async Task<Content?> GetContentAsync(string id)
    {
        try
        {
            var invitation = await _databaseService.GetByIdAsync(id);
            return invitation?.WeddingInvitationData.Content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving content for invitation {Id}", id);
            throw;
        }
    }

    public async Task<Content?> UpdateContentAsync(string id, Content content)
    {
        try
        {
            var invitation = await _databaseService.UpdateContentAsync(id, content);
            return invitation?.WeddingInvitationData.Content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating content for invitation {Id}", id);
            throw;
        }
    }

    public async Task<BasicInfo?> GetBasicInfoAsync(string id)
    {
        try
        {
            var invitation = await _databaseService.GetByIdAsync(id);
            return invitation?.WeddingInvitationData.Content.BasicInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving basic info for invitation {Id}", id);
            throw;
        }
    }

    public async Task<BasicInfo?> UpdateBasicInfoAsync(string id, BasicInfo basicInfo)
    {
        try
        {
            var invitation = await _databaseService.UpdateBasicInfoAsync(id, basicInfo);
            return invitation?.WeddingInvitationData.Content.BasicInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating basic info for invitation {Id}", id);
            throw;
        }
    }

    public async Task<CeremonyDetails?> GetCeremonyDetailsAsync(string id)
    {
        try
        {
            var invitation = await _databaseService.GetByIdAsync(id);
            return invitation?.WeddingInvitationData.Content.CeremonyDetails;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving ceremony details for invitation {Id}", id);
            throw;
        }
    }

    public async Task<CeremonyDetails?> UpdateCeremonyDetailsAsync(string id, CeremonyDetails ceremonyDetails)
    {
        try
        {
            var invitation = await _databaseService.UpdateCeremonyDetailsAsync(id, ceremonyDetails);
            return invitation?.WeddingInvitationData.Content.CeremonyDetails;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating ceremony details for invitation {Id}", id);
            throw;
        }
    }

    public async Task<AdditionalInfo?> GetAdditionalInfoAsync(string id)
    {
        try
        {
            var invitation = await _databaseService.GetByIdAsync(id);
            return invitation?.WeddingInvitationData.Content.AdditionalInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving additional info for invitation {Id}", id);
            throw;
        }
    }

    public async Task<AdditionalInfo?> UpdateAdditionalInfoAsync(string id, AdditionalInfo additionalInfo)
    {
        try
        {
            var invitation = await _databaseService.UpdateAdditionalInfoAsync(id, additionalInfo);
            return invitation?.WeddingInvitationData.Content.AdditionalInfo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating additional info for invitation {Id}", id);
            throw;
        }
    }

    public async Task<List<TemplatePreview>> GetAvailableTemplatesAsync()
    {
        try
        {
            var templates = new List<TemplatePreview>
            {
                new() { Id = "default", Name = "Default Template", PreviewUrl = "/templates/default/preview.jpg" },
                new() { Id = "elegant", Name = "Elegant Template", PreviewUrl = "/templates/elegant/preview.jpg" },
                new() { Id = "modern", Name = "Modern Template", PreviewUrl = "/templates/modern/preview.jpg" },
                new() { Id = "vintage", Name = "Vintage Template", PreviewUrl = "/templates/vintage/preview.jpg" }
            };

            return await Task.FromResult(templates);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving available templates");
            throw;
        }
    }
}