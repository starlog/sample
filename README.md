# Wedding Invitation API (.NET 9)

A .NET 9 REST API for managing wedding invitations with MongoDB storage.

## Features

- Full CRUD operations for wedding invitations
- Template, font, and content management
- MongoDB database with ObjectId support
- Input validation with FluentValidation
- Comprehensive error handling and logging
- Rate limiting with AspNetCoreRateLimit
- CORS support
- Swagger/OpenAPI documentation
- Dependency injection and clean architecture

## Prerequisites

- .NET 9.0 SDK
- MongoDB running on localhost:27017
- Visual Studio 2022 or VS Code (optional)

## Installation and Setup

1. Navigate to the project directory:
```bash
cd dotnetcode
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

For development with hot reload:
```bash
dotnet watch run
```

## API Endpoints

### Wedding Invitations
- `GET /api/wedding-invitations` - Get all wedding invitations
- `POST /api/wedding-invitations` - Create a new wedding invitation
- `GET /api/wedding-invitations/{id}` - Get wedding invitation by ID
- `PUT /api/wedding-invitations/{id}` - Update wedding invitation
- `DELETE /api/wedding-invitations/{id}` - Delete wedding invitation

### Templates
- `GET /api/wedding-invitations/{id}/template` - Get template configuration
- `PUT /api/wedding-invitations/{id}/template` - Update template configuration
- `GET /api/templates` - Get all available templates

### Fonts
- `GET /api/wedding-invitations/{id}/fonts` - Get font configuration
- `PUT /api/wedding-invitations/{id}/fonts` - Update font configuration

### Content
- `GET /api/wedding-invitations/{id}/content` - Get content details
- `PUT /api/wedding-invitations/{id}/content` - Update content details

### Content Sections
- `GET /api/wedding-invitations/{id}/content/basic-info` - Get basic information
- `PUT /api/wedding-invitations/{id}/content/basic-info` - Update basic information
- `GET /api/wedding-invitations/{id}/content/ceremony-details` - Get ceremony details
- `PUT /api/wedding-invitations/{id}/content/ceremony-details` - Update ceremony details
- `GET /api/wedding-invitations/{id}/content/additional-info` - Get additional information
- `PUT /api/wedding-invitations/{id}/content/additional-info` - Update additional information

## Configuration

### appsettings.json

The application can be configured through `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "barunson",
    "CollectionName": "wedding"
  },
  "IpRateLimiting": {
    "EnableEndpointRateLimiting": true,
    "GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "15m",
        "Limit": 100
      }
    ]
  }
}
```

### Environment Variables

- `ASPNETCORE_ENVIRONMENT` - Set to `Development` for development mode
- `ASPNETCORE_URLS` - Specify the URLs for the application (e.g., `https://localhost:5001;http://localhost:5000`)

## Data Storage

The API uses MongoDB to store wedding invitation data:
- **Database**: `barunson`
- **Collection**: `wedding`
- **Connection**: `mongodb://localhost:27017`

MongoDB documents are structured according to the wedding invitation schema with proper BSON attributes and ObjectId support.

## API Documentation

Swagger UI documentation is available at multiple endpoints:
- `/docs` - Main documentation endpoint (redirects to Swagger UI)
- `/swagger` - Direct Swagger UI access
- `/` - Root URL (redirects to Swagger UI)

URLs:
- `https://localhost:5001/docs` (HTTPS)
- `http://localhost:5000/docs` (HTTP)

## Health Check

Visit `/health` to check if the API is running:
```bash
curl http://localhost:5000/health
```

## Example Usage

### Create a Wedding Invitation

```bash
curl -X POST https://localhost:5001/api/wedding-invitations \
  -H "Content-Type: application/json" \
  -d '{
    "template": {
      "opening_effect": {
        "enabled": false,
        "lettering_effect": {
          "enabled": true,
          "color": "#000000",
          "position": "center"
        }
      },
      "design": {
        "template_id": "default",
        "frame": {
          "type": "square",
          "options": ["square", "arch", "circle"]
        },
        "photo": {
          "url": "",
          "required": true
        },
        "colors": {
          "background": "#ffffff",
          "accent": "#000000"
        }
      }
    },
    "fonts": {
      "title": {
        "family": "default",
        "color": "#000000",
        "size": "S",
        "size_options": ["S", "M", "L"]
      },
      "body": {
        "family": "default",
        "color": "black",
        "color_options": ["black", "white"],
        "size": "S",
        "size_options": ["S", "M", "L"]
      }
    },
    "content": {
      "basic_info": {
        "groom": {
          "name": "John Doe",
          "max_length": 20,
          "required": true
        },
        "bride": {
          "name": "Jane Smith",
          "max_length": 20,
          "required": true
        },
        "relationship": "사랑하는 두 사람"
      },
      "ceremony_details": {
        "date": "2024-06-15",
        "time": "14:00",
        "venue": {
          "name": "Garden Wedding Hall",
          "address": "123 Wedding St, Seoul",
          "contact": "02-1234-5678"
        }
      },
      "additional_info": {
        "parents": {
          "groom_parents": {
            "father": "John Doe Sr.",
            "mother": "Mary Doe"
          },
          "bride_parents": {
            "father": "Robert Smith",
            "mother": "Lisa Smith"
          }
        },
        "message": "함께 해주셔서 감사합니다",
        "contact_info": {
          "phone": "010-1234-5678",
          "email": "wedding@example.com"
        }
      }
    },
    "metadata": {
      "created_date": "",
      "last_modified": "",
      "version": "1.0",
      "language": "ko"
    }
  }'
```

## Architecture

The application follows clean architecture principles:

- **Controllers**: Handle HTTP requests and responses
- **Services**: Business logic and orchestration
- **Models**: Data models and entities
- **DTOs**: Data transfer objects for API responses
- **Validators**: Input validation using FluentValidation
- **Middleware**: Cross-cutting concerns (error handling, logging)

## Rate Limiting

The API includes rate limiting to prevent abuse:
- 100 requests per 15 minutes per IP
- 500 requests per hour per IP

## Error Handling

All errors are handled consistently and return standardized error responses:

```json
{
  "success": false,
  "error": "Error message here",
  "data": null,
  "message": null
}
```

## Logging

The application uses built-in .NET logging with different log levels:
- `Information`: General application flow
- `Warning`: Potentially harmful situations
- `Error`: Error events that might still allow the application to continue
- `Debug`: Fine-grained informational events (Development only)

## Development

### Adding New Endpoints

1. Add method to the appropriate service interface
2. Implement the method in the service class
3. Add controller action
4. Add validation if needed
5. Update Swagger documentation

### Running Tests

```bash
dotnet test
```

### Code Formatting

```bash
dotnet format
```

## Deployment

For production deployment:

1. Set `ASPNETCORE_ENVIRONMENT=Production`
2. Configure appropriate logging levels
3. Set up MongoDB connection string for production
4. Configure reverse proxy (nginx/IIS) if needed
5. Enable HTTPS
6. Ensure MongoDB is properly secured and backed up

## Dependencies

- **Microsoft.AspNetCore.OpenApi** - OpenAPI support
- **Swashbuckle.AspNetCore** - Swagger UI
- **FluentValidation.AspNetCore** - Input validation
- **MongoDB.Driver** - MongoDB .NET driver
- **MongoDB.Bson** - BSON serialization
- **Newtonsoft.Json** - JSON serialization
- **AspNetCoreRateLimit** - Rate limiting
- **Microsoft.Extensions.Logging** - Logging

## Git and Version Control

### What's Included in the Repository
This repository includes:
- ✅ Source code (`.cs`, `.csproj` files)
- ✅ Configuration templates (`appsettings.json`, `appsettings.Development.json`)
- ✅ Documentation (`README.md`)
- ✅ Project configuration (`Program.cs`, validators, models, etc.)

### What's Excluded (.gitignore)
The following files/folders are automatically excluded:
- ❌ Build artifacts (`bin/`, `obj/`, `dist/`)
- ❌ IDE files (`.vs/`, `.vscode/`, `.idea/`)
- ❌ User-specific settings (`*.user`, `*.suo`)
- ❌ Production secrets (`appsettings.Production.json`, `secrets.json`)
- ❌ Logs and temporary files
- ❌ Database files (`*.db`, `*.sqlite`)
- ❌ Environment variables (`.env` files)

### Security Notes
- MongoDB connection strings in `appsettings.json` use localhost (safe for development)
- Production configurations should be stored separately and not committed
- API keys, passwords, and sensitive data should use environment variables or Azure Key Vault in production

### Contributing
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/new-feature`)
5. Create a Pull Request

## License

MIT License