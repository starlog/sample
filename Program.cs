using WeddingInvitationApi.Services;
using WeddingInvitationApi.Middleware;
using WeddingInvitationApi.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using AspNetCoreRateLimit;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "Wedding Invitation API", 
        Version = "v1",
        Description = "A comprehensive REST API for managing wedding invitations with templates, fonts, and content management",
        Contact = new() 
        {
            Name = "Wedding Invitation API",
            Url = new Uri("https://github.com/example/wedding-invitation-api")
        }
    });
    
    // Set the comments path for the Swagger JSON and UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Rate Limiting
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configure MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

// Add custom services
builder.Services.AddSingleton<IMongoDbService, MongoDbService>();
builder.Services.AddScoped<IWeddingInvitationService, WeddingInvitationService>();

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wedding Invitation API v1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

// Use Rate Limiting
app.UseIpRateLimiting();

// Use custom middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new
{
    Success = true,
    Message = "Wedding Invitation API is running",
    Timestamp = DateTime.UtcNow,
    Version = "1.0.0"
}));

// Documentation endpoint - redirect to Swagger UI
app.MapGet("/docs", () => Results.Redirect("/swagger"));
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();