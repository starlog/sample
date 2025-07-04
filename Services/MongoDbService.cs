using MongoDB.Driver;
using MongoDB.Bson;
using WeddingInvitationApi.Models;
using WeddingInvitationApi.Configuration;
using Microsoft.Extensions.Options;

namespace WeddingInvitationApi.Services;

public class MongoDbService : IMongoDbService
{
    private readonly IMongoCollection<WeddingInvitation> _collection;
    private readonly ILogger<MongoDbService> _logger;

    public MongoDbService(IOptions<MongoDbSettings> settings, ILogger<MongoDbService> logger)
    {
        _logger = logger;
        
        try
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<WeddingInvitation>(settings.Value.CollectionName);
            
            _logger.LogInformation("Connected to MongoDB: {DatabaseName}.{CollectionName}", 
                settings.Value.DatabaseName, settings.Value.CollectionName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to MongoDB");
            throw;
        }
    }

    public async Task<List<WeddingInvitation>> GetAllAsync()
    {
        try
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all invitations from MongoDB");
            throw;
        }
    }

    public async Task<WeddingInvitation?> GetByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving invitation {Id} from MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation> CreateAsync(WeddingInvitationData data)
    {
        try
        {
            var invitation = new WeddingInvitation
            {
                Id = ObjectId.GenerateNewId().ToString(),
                WeddingInvitationData = data
            };

            // Ensure metadata is set
            invitation.WeddingInvitationData.Metadata.CreatedDate = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            invitation.WeddingInvitationData.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            await _collection.InsertOneAsync(invitation);
            _logger.LogInformation("Created new invitation with ID: {Id}", invitation.Id);
            
            return invitation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating invitation in MongoDB");
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateAsync(string id, WeddingInvitationData data)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            data.Metadata.LastModified = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData, data);

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            if (result != null)
            {
                _logger.LogInformation("Updated invitation with ID: {Id}", id);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating invitation {Id} in MongoDB", id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return false;
            }

            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            
            if (result.DeletedCount > 0)
            {
                _logger.LogInformation("Deleted invitation with ID: {Id}", id);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting invitation {Id} from MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateTemplateAsync(string id, Template template)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData.Template, template)
                .Set(x => x.WeddingInvitationData.Metadata.LastModified, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating template for invitation {Id} in MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateFontsAsync(string id, Fonts fonts)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData.Fonts, fonts)
                .Set(x => x.WeddingInvitationData.Metadata.LastModified, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating fonts for invitation {Id} in MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateContentAsync(string id, Content content)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData.Content, content)
                .Set(x => x.WeddingInvitationData.Metadata.LastModified, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating content for invitation {Id} in MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateBasicInfoAsync(string id, BasicInfo basicInfo)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData.Content.BasicInfo, basicInfo)
                .Set(x => x.WeddingInvitationData.Metadata.LastModified, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating basic info for invitation {Id} in MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateCeremonyDetailsAsync(string id, CeremonyDetails ceremonyDetails)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData.Content.CeremonyDetails, ceremonyDetails)
                .Set(x => x.WeddingInvitationData.Metadata.LastModified, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating ceremony details for invitation {Id} in MongoDB", id);
            throw;
        }
    }

    public async Task<WeddingInvitation?> UpdateAdditionalInfoAsync(string id, AdditionalInfo additionalInfo)
    {
        try
        {
            if (!ObjectId.TryParse(id, out _))
            {
                _logger.LogWarning("Invalid ObjectId format: {Id}", id);
                return null;
            }

            var update = Builders<WeddingInvitation>.Update
                .Set(x => x.WeddingInvitationData.Content.AdditionalInfo, additionalInfo)
                .Set(x => x.WeddingInvitationData.Metadata.LastModified, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var options = new FindOneAndUpdateOptions<WeddingInvitation>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _collection.FindOneAndUpdateAsync<WeddingInvitation>(
                x => x.Id == id,
                update,
                options);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating additional info for invitation {Id} in MongoDB", id);
            throw;
        }
    }
}