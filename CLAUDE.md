# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Essential Commands

```bash
# Build and run the API
dotnet restore          # Restore NuGet packages
dotnet build           # Build the project
dotnet run             # Run the API (production mode)
dotnet watch run       # Run with hot reload (development)

# MongoDB verification
mongosh barunson --eval "db.wedding.countDocuments()"  # Check sample data
mongosh barunson --eval "db.wedding.findOne()"         # View sample document

# API testing
curl http://localhost:5000/health                       # Health check
curl http://localhost:5000/docs                        # Swagger UI redirect
```

## Architecture Overview

This is a .NET 9 Web API for managing wedding invitations with MongoDB storage. The application follows a layered architecture with clear separation of concerns:

### Data Flow Architecture
```
Controllers → WeddingInvitationService → MongoDbService → MongoDB
     ↓              ↓                        ↓
  Validation   Business Logic        Database Operations
```

### Key Architectural Patterns

**Service Layer Pattern**: Business logic is encapsulated in `WeddingInvitationService`, which orchestrates calls to the database service. This abstraction allows the business layer to remain independent of database implementation details.

**Repository Pattern**: `MongoDbService` implements `IMongoDbService` and handles all MongoDB operations. The service abstracts MongoDB-specific operations and provides a clean interface for data access.

**Dependency Injection**: All services are registered in `Program.cs` and injected via constructor injection. The MongoDB configuration is bound from `appsettings.json` using the Options pattern.

### MongoDB Integration Architecture

The application uses MongoDB with a specific schema structure:
- **Database**: `barunson`
- **Collection**: `wedding`
- **Document Structure**: Nested wedding invitation data with BSON attributes for proper serialization

Models use both `JsonPropertyName` for API serialization and `BsonElement` for MongoDB storage, enabling clean separation between API contracts and database storage.

### Validation Architecture

FluentValidation is integrated at the service registration level (`Program.cs`) with automatic validation. Validators in `/Validators/` define complex validation rules that are automatically applied to incoming requests.

### API Response Pattern

All controllers return standardized `ApiResponse<T>` objects that provide consistent success/error messaging across the API. The `ApiResponse` class handles both generic and typed responses.

## Critical MongoDB Configuration

The MongoDB connection configuration in `appsettings.json` defines:
- Connection string: `mongodb://localhost:27017`
- Database: `barunson`
- Collection: `wedding`

Sample data exists in the database with Korean wedding invitation content. The `MongoDbService` uses ObjectId validation and proper BSON serialization for all operations.

## Development Workflow

### Adding New Endpoints
1. Define the endpoint in the appropriate controller (`WeddingInvitationsController` or `TemplatesController`)
2. Add business logic to `WeddingInvitationService`
3. Implement database operations in `MongoDbService` if needed
4. Add validation rules in the appropriate validator class
5. Update the service interface if adding new methods

### MongoDB Document Schema
The wedding invitation schema is complex with nested objects for:
- `template` (design, colors, frame settings)
- `fonts` (title and body font configurations)
- `content` (basic info, ceremony details, additional info)
- `metadata` (timestamps, version, language)

When modifying the schema, update both the model classes and validation rules.

### Error Handling Strategy
- Controllers catch exceptions and return appropriate HTTP status codes
- `MongoDbService` logs errors and re-throws for controller handling
- `ErrorHandlingMiddleware` provides global exception handling
- All services use structured logging with meaningful error messages

### Rate Limiting Configuration
API includes rate limiting via `AspNetCoreRateLimit`:
- 100 requests per 15 minutes per IP
- 500 requests per hour per IP
- Configure in `appsettings.json` under `IpRateLimiting`

## Sample Data Context

The MongoDB database contains 5 sample wedding invitations with diverse templates (elegant, vintage, modern, classic, garden) and Korean wedding data. Use these ObjectIds for testing:
- Sample document: Use `mongosh barunson --eval "db.wedding.findOne()"` to get a valid ID
- API testing: `curl http://localhost:5000/api/wedding-invitations/{id}`

## Configuration Management

Development configuration uses localhost MongoDB connection (safe for public repos). Production secrets should use environment variables or Azure Key Vault. The application uses the .NET Options pattern for configuration binding in `MongoDbSettings`.