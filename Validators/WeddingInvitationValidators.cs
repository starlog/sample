using FluentValidation;
using WeddingInvitationApi.Models;

namespace WeddingInvitationApi.Validators;

public class WeddingInvitationDataValidator : AbstractValidator<WeddingInvitationData>
{
    public WeddingInvitationDataValidator()
    {
        RuleFor(x => x.Template).SetValidator(new TemplateValidator());
        RuleFor(x => x.Fonts).SetValidator(new FontsValidator());
        RuleFor(x => x.Content).SetValidator(new ContentValidator());
        RuleFor(x => x.Metadata).SetValidator(new MetadataValidator());
    }
}

public class TemplateValidator : AbstractValidator<Template>
{
    public TemplateValidator()
    {
        RuleFor(x => x.OpeningEffect).SetValidator(new OpeningEffectValidator());
        RuleFor(x => x.Design).SetValidator(new DesignValidator());
    }
}

public class OpeningEffectValidator : AbstractValidator<OpeningEffect>
{
    public OpeningEffectValidator()
    {
        RuleFor(x => x.LetteringEffect).SetValidator(new LetteringEffectValidator());
    }
}

public class LetteringEffectValidator : AbstractValidator<LetteringEffect>
{
    public LetteringEffectValidator()
    {
        RuleFor(x => x.Color)
            .Matches(@"^#[0-9A-Fa-f]{6}$")
            .WithMessage("Color must be in hex format (e.g., #000000)");
            
        RuleFor(x => x.Position)
            .Must(x => x == "center")
            .WithMessage("Position must be 'center'");
    }
}

public class DesignValidator : AbstractValidator<Design>
{
    public DesignValidator()
    {
        RuleFor(x => x.TemplateId).NotEmpty().WithMessage("Template ID is required");
        RuleFor(x => x.Frame).SetValidator(new FrameValidator());
        RuleFor(x => x.Photo).SetValidator(new PhotoValidator());
        RuleFor(x => x.Colors).SetValidator(new ColorsValidator());
    }
}

public class FrameValidator : AbstractValidator<Frame>
{
    public FrameValidator()
    {
        RuleFor(x => x.Type)
            .Must(x => new[] { "square", "arch", "circle" }.Contains(x))
            .WithMessage("Frame type must be 'square', 'arch', or 'circle'");
    }
}

public class PhotoValidator : AbstractValidator<Photo>
{
    public PhotoValidator()
    {
        RuleFor(x => x.Url).NotNull();
    }
}

public class ColorsValidator : AbstractValidator<Colors>
{
    public ColorsValidator()
    {
        RuleFor(x => x.Background)
            .Matches(@"^#[0-9A-Fa-f]{6}$")
            .WithMessage("Background color must be in hex format");
            
        RuleFor(x => x.Accent)
            .Matches(@"^#[0-9A-Fa-f]{6}$")
            .WithMessage("Accent color must be in hex format");
    }
}

public class FontsValidator : AbstractValidator<Fonts>
{
    public FontsValidator()
    {
        RuleFor(x => x.Title).SetValidator(new TitleFontValidator());
        RuleFor(x => x.Body).SetValidator(new BodyFontValidator());
    }
}

public class TitleFontValidator : AbstractValidator<TitleFont>
{
    public TitleFontValidator()
    {
        RuleFor(x => x.Family).NotEmpty().WithMessage("Font family is required");
        
        RuleFor(x => x.Color)
            .Matches(@"^#[0-9A-Fa-f]{6}$")
            .WithMessage("Color must be in hex format");
            
        RuleFor(x => x.Size)
            .Must(x => new[] { "S", "M", "L" }.Contains(x))
            .WithMessage("Size must be 'S', 'M', or 'L'");
    }
}

public class BodyFontValidator : AbstractValidator<BodyFont>
{
    public BodyFontValidator()
    {
        RuleFor(x => x.Family).NotEmpty().WithMessage("Font family is required");
        
        RuleFor(x => x.Color)
            .Must(x => new[] { "black", "white" }.Contains(x))
            .WithMessage("Color must be 'black' or 'white'");
            
        RuleFor(x => x.Size)
            .Must(x => new[] { "S", "M", "L" }.Contains(x))
            .WithMessage("Size must be 'S', 'M', or 'L'");
    }
}

public class ContentValidator : AbstractValidator<Content>
{
    public ContentValidator()
    {
        RuleFor(x => x.BasicInfo).SetValidator(new BasicInfoValidator());
        RuleFor(x => x.CeremonyDetails).SetValidator(new CeremonyDetailsValidator());
        RuleFor(x => x.AdditionalInfo).SetValidator(new AdditionalInfoValidator());
    }
}

public class BasicInfoValidator : AbstractValidator<BasicInfo>
{
    public BasicInfoValidator()
    {
        RuleFor(x => x.Groom).SetValidator(new PersonValidator());
        RuleFor(x => x.Bride).SetValidator(new PersonValidator());
    }
}

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(20)
            .WithMessage("Name cannot exceed 20 characters");
    }
}

public class CeremonyDetailsValidator : AbstractValidator<CeremonyDetails>
{
    public CeremonyDetailsValidator()
    {
        RuleFor(x => x.Venue).SetValidator(new VenueValidator());
    }
}

public class VenueValidator : AbstractValidator<Venue>
{
    public VenueValidator()
    {
        // Add venue-specific validation rules if needed
    }
}

public class AdditionalInfoValidator : AbstractValidator<AdditionalInfo>
{
    public AdditionalInfoValidator()
    {
        RuleFor(x => x.Parents).SetValidator(new ParentsValidator());
        RuleFor(x => x.ContactInfo).SetValidator(new ContactInfoValidator());
    }
}

public class ParentsValidator : AbstractValidator<Parents>
{
    public ParentsValidator()
    {
        RuleFor(x => x.GroomParents).SetValidator(new ParentPairValidator());
        RuleFor(x => x.BrideParents).SetValidator(new ParentPairValidator());
    }
}

public class ParentPairValidator : AbstractValidator<ParentPair>
{
    public ParentPairValidator()
    {
        // Add parent pair validation rules if needed
    }
}

public class ContactInfoValidator : AbstractValidator<ContactInfo>
{
    public ContactInfoValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format");
    }
}

public class MetadataValidator : AbstractValidator<Metadata>
{
    public MetadataValidator()
    {
        RuleFor(x => x.Version).NotEmpty().WithMessage("Version is required");
        RuleFor(x => x.Language).NotEmpty().WithMessage("Language is required");
    }
}