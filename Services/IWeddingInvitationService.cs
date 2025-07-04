using WeddingInvitationApi.Models;
using WeddingInvitationApi.DTOs;

namespace WeddingInvitationApi.Services;

public interface IWeddingInvitationService
{
    Task<List<WeddingInvitation>> GetAllInvitationsAsync();
    Task<WeddingInvitation?> GetInvitationByIdAsync(string id);
    Task<WeddingInvitation> CreateInvitationAsync(WeddingInvitationData data);
    Task<WeddingInvitation?> UpdateInvitationAsync(string id, WeddingInvitationData data);
    Task<bool> DeleteInvitationAsync(string id);
    Task<Template?> GetTemplateAsync(string id);
    Task<Template?> UpdateTemplateAsync(string id, Template template);
    Task<Fonts?> GetFontsAsync(string id);
    Task<Fonts?> UpdateFontsAsync(string id, Fonts fonts);
    Task<Content?> GetContentAsync(string id);
    Task<Content?> UpdateContentAsync(string id, Content content);
    Task<BasicInfo?> GetBasicInfoAsync(string id);
    Task<BasicInfo?> UpdateBasicInfoAsync(string id, BasicInfo basicInfo);
    Task<CeremonyDetails?> GetCeremonyDetailsAsync(string id);
    Task<CeremonyDetails?> UpdateCeremonyDetailsAsync(string id, CeremonyDetails ceremonyDetails);
    Task<AdditionalInfo?> GetAdditionalInfoAsync(string id);
    Task<AdditionalInfo?> UpdateAdditionalInfoAsync(string id, AdditionalInfo additionalInfo);
    Task<List<TemplatePreview>> GetAvailableTemplatesAsync();
}