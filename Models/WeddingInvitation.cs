using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeddingInvitationApi.Models;

public class WeddingInvitation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("wedding_invitation")]
    [JsonPropertyName("wedding_invitation")]
    public WeddingInvitationData WeddingInvitationData { get; set; } = new();
}

public class WeddingInvitationData
{
    [BsonElement("template")]
    [JsonPropertyName("template")]
    public Template Template { get; set; } = new();

    [BsonElement("fonts")]
    [JsonPropertyName("fonts")]
    public Fonts Fonts { get; set; } = new();

    [BsonElement("content")]
    [JsonPropertyName("content")]
    public Content Content { get; set; } = new();

    [BsonElement("metadata")]
    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; } = new();
}

public class Template
{
    [JsonPropertyName("opening_effect")]
    public OpeningEffect OpeningEffect { get; set; } = new();

    [JsonPropertyName("design")]
    public Design Design { get; set; } = new();
}

public class OpeningEffect
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("lettering_effect")]
    public LetteringEffect LetteringEffect { get; set; } = new();
}

public class LetteringEffect
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("color")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be in hex format (e.g., #000000)")]
    public string Color { get; set; } = "#000000";

    [JsonPropertyName("position")]
    public string Position { get; set; } = "center";
}

public class Design
{
    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = "default";

    [JsonPropertyName("frame")]
    public Frame Frame { get; set; } = new();

    [JsonPropertyName("photo")]
    public Photo Photo { get; set; } = new();

    [JsonPropertyName("colors")]
    public Colors Colors { get; set; } = new();
}

public class Frame
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "square";

    [JsonPropertyName("options")]
    public List<string> Options { get; set; } = new() { "square", "arch", "circle" };
}

public class Photo
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("required")]
    public bool Required { get; set; }
}

public class Colors
{
    [JsonPropertyName("background")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Background color must be in hex format")]
    public string Background { get; set; } = "#ffffff";

    [JsonPropertyName("accent")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Accent color must be in hex format")]
    public string Accent { get; set; } = "#000000";
}

public class Fonts
{
    [JsonPropertyName("title")]
    public TitleFont Title { get; set; } = new();

    [JsonPropertyName("body")]
    public BodyFont Body { get; set; } = new();
}

public class TitleFont
{
    [JsonPropertyName("family")]
    public string Family { get; set; } = "default";

    [JsonPropertyName("color")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Color must be in hex format")]
    public string Color { get; set; } = "#000000";

    [JsonPropertyName("size")]
    public string Size { get; set; } = "S";

    [JsonPropertyName("size_options")]
    public List<string> SizeOptions { get; set; } = new() { "S", "M", "L" };
}

public class BodyFont
{
    [JsonPropertyName("family")]
    public string Family { get; set; } = "default";

    [JsonPropertyName("color")]
    public string Color { get; set; } = "black";

    [JsonPropertyName("color_options")]
    public List<string> ColorOptions { get; set; } = new() { "black", "white" };

    [JsonPropertyName("size")]
    public string Size { get; set; } = "S";

    [JsonPropertyName("size_options")]
    public List<string> SizeOptions { get; set; } = new() { "S", "M", "L" };
}

public class Content
{
    [JsonPropertyName("basic_info")]
    public BasicInfo BasicInfo { get; set; } = new();

    [JsonPropertyName("ceremony_details")]
    public CeremonyDetails CeremonyDetails { get; set; } = new();

    [JsonPropertyName("additional_info")]
    public AdditionalInfo AdditionalInfo { get; set; } = new();
}

public class BasicInfo
{
    [JsonPropertyName("groom")]
    public Person Groom { get; set; } = new();

    [JsonPropertyName("bride")]
    public Person Bride { get; set; } = new();

    [JsonPropertyName("relationship")]
    public string Relationship { get; set; } = string.Empty;
}

public class Person
{
    [JsonPropertyName("name")]
    [MaxLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("max_length")]
    public int MaxLength { get; set; } = 20;

    [JsonPropertyName("required")]
    public bool Required { get; set; }
}

public class CeremonyDetails
{
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    [JsonPropertyName("venue")]
    public Venue Venue { get; set; } = new();
}

public class Venue
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("contact")]
    public string Contact { get; set; } = string.Empty;
}

public class AdditionalInfo
{
    [JsonPropertyName("parents")]
    public Parents Parents { get; set; } = new();

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("contact_info")]
    public ContactInfo ContactInfo { get; set; } = new();
}

public class Parents
{
    [JsonPropertyName("groom_parents")]
    public ParentPair GroomParents { get; set; } = new();

    [JsonPropertyName("bride_parents")]
    public ParentPair BrideParents { get; set; } = new();
}

public class ParentPair
{
    [JsonPropertyName("father")]
    public string Father { get; set; } = string.Empty;

    [JsonPropertyName("mother")]
    public string Mother { get; set; } = string.Empty;
}

public class ContactInfo
{
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
}

public class Metadata
{
    [JsonPropertyName("created_date")]
    public string CreatedDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

    [JsonPropertyName("last_modified")]
    public string LastModified { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

    [JsonPropertyName("version")]
    public string Version { get; set; } = "1.0";

    [JsonPropertyName("language")]
    public string Language { get; set; } = "ko";
}