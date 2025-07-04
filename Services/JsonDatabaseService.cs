using WeddingInvitationApi.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace WeddingInvitationApi.Services;

public class JsonDatabaseService : IJsonDatabaseService
{
    private readonly string _dataPath;
    private readonly ConcurrentDictionary<string, WeddingInvitation> _invitations;
    private readonly SemaphoreSlim _fileLock;
    private readonly ILogger<JsonDatabaseService> _logger;

    public JsonDatabaseService(IConfiguration configuration, ILogger<JsonDatabaseService> logger)
    {
        _dataPath = configuration.GetValue<string>("DatabaseSettings:DataPath") ?? "./data/invitations.json";
        _invitations = new ConcurrentDictionary<string, WeddingInvitation>();
        _fileLock = new SemaphoreSlim(1, 1);
        _logger = logger;
        
        LoadDataAsync().Wait();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var directory = Path.GetDirectoryName(_dataPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (File.Exists(_dataPath))
            {
                var json = await File.ReadAllTextAsync(_dataPath);
                var invitations = JsonConvert.DeserializeObject<List<WeddingInvitation>>(json) ?? new List<WeddingInvitation>();
                
                foreach (var invitation in invitations)
                {
                    _invitations.TryAdd(invitation.Id, invitation);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading data from {DataPath}", _dataPath);
        }
    }

    private async Task SaveDataAsync()
    {
        await _fileLock.WaitAsync();
        try
        {
            var invitations = _invitations.Values.ToList();
            var json = JsonConvert.SerializeObject(invitations, Formatting.Indented);
            await File.WriteAllTextAsync(_dataPath, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving data to {DataPath}", _dataPath);
            throw;
        }
        finally
        {
            _fileLock.Release();
        }
    }

    public async Task<List<WeddingInvitation>> GetAllAsync()
    {
        return await Task.FromResult(_invitations.Values.ToList());
    }

    public async Task<WeddingInvitation?> GetByIdAsync(string id)
    {
        _invitations.TryGetValue(id, out var invitation);
        return await Task.FromResult(invitation);
    }

    public async Task<WeddingInvitation> CreateAsync(WeddingInvitationData data)
    {
        var invitation = new WeddingInvitation
        {
            Id = Guid.NewGuid().ToString(),
            WeddingInvitationData = data
        };

        // Ensure metadata is set
        invitation.WeddingInvitationData.Metadata.CreatedDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        _invitations.TryAdd(invitation.Id, invitation);
        await SaveDataAsync();
        
        return invitation;
    }

    public async Task<WeddingInvitation?> UpdateAsync(string id, WeddingInvitationData data)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData = data;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var removed = _invitations.TryRemove(id, out _);
        if (removed)
        {
            await SaveDataAsync();
        }
        return removed;
    }

    public async Task<WeddingInvitation?> UpdateTemplateAsync(string id, Template template)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData.Template = template;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }

    public async Task<WeddingInvitation?> UpdateFontsAsync(string id, Fonts fonts)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData.Fonts = fonts;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }

    public async Task<WeddingInvitation?> UpdateContentAsync(string id, Content content)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData.Content = content;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }

    public async Task<WeddingInvitation?> UpdateBasicInfoAsync(string id, BasicInfo basicInfo)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData.Content.BasicInfo = basicInfo;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }

    public async Task<WeddingInvitation?> UpdateCeremonyDetailsAsync(string id, CeremonyDetails ceremonyDetails)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData.Content.CeremonyDetails = ceremonyDetails;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }

    public async Task<WeddingInvitation?> UpdateAdditionalInfoAsync(string id, AdditionalInfo additionalInfo)
    {
        if (!_invitations.TryGetValue(id, out var invitation))
        {
            return null;
        }

        invitation.WeddingInvitationData.Content.AdditionalInfo = additionalInfo;
        invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        
        await SaveDataAsync();
        return invitation;
    }
}