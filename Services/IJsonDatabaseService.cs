using WeddingInvitationApi.Models;

namespace WeddingInvitationApi.Services;

public interface IJsonDatabaseService
{
    Task<List<WeddingInvitation>> GetAllAsync();
    Task<WeddingInvitation?> GetByIdAsync(string id);
    Task<WeddingInvitation> CreateAsync(WeddingInvitationData data);
    Task<WeddingInvitation?> UpdateAsync(string id, WeddingInvitationData data);
    Task<bool> DeleteAsync(string id);
    Task<WeddingInvitation?> UpdateTemplateAsync(string id, Template template);
    Task<WeddingInvitation?> UpdateFontsAsync(string id, Fonts fonts);
    Task<WeddingInvitation?> UpdateContentAsync(string id, Content content);
    Task<WeddingInvitation?> UpdateBasicInfoAsync(string id, BasicInfo basicInfo);
    Task<WeddingInvitation?> UpdateCeremonyDetailsAsync(string id, CeremonyDetails ceremonyDetails);
    Task<WeddingInvitation?> UpdateAdditionalInfoAsync(string id, AdditionalInfo additionalInfo);
}