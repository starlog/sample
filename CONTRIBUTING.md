# Contributing to Wedding Invitation API

Thank you for considering contributing to the Wedding Invitation API project!

## Development Setup

### Prerequisites
- .NET 9.0 SDK
- MongoDB running on localhost:27017
- Git
- Your favorite code editor (VS Code, Visual Studio, etc.)

### Getting Started

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd dotnetcode
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Set up MongoDB**
   - Ensure MongoDB is running on `localhost:27017`
   - The API will create the `barunson` database and `wedding` collection automatically

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Test the API**
   - Visit `http://localhost:5000/docs` for Swagger documentation
   - Use the sample data already inserted in MongoDB

## Development Workflow

### Making Changes

1. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes**
   - Follow existing code style and patterns
   - Add appropriate validation and error handling
   - Update tests if applicable

3. **Test your changes**
   ```bash
   # Build the project
   dotnet build
   
   # Run the API
   dotnet run
   
   # Test endpoints using Swagger UI or curl
   ```

4. **Commit your changes**
   ```bash
   git add .
   git commit -m "Add: description of your changes"
   ```

### Commit Message Guidelines

Use clear, descriptive commit messages:
- `Add: new feature or functionality`
- `Fix: bug fix`
- `Update: modify existing feature`
- `Refactor: code restructuring`
- `Docs: documentation changes`

### Code Style

- Follow standard C# naming conventions
- Use meaningful variable and method names
- Add XML documentation for public APIs
- Keep methods focused and small
- Use async/await patterns for database operations

### Configuration Management

- ✅ **Safe to commit**: `appsettings.json`, `appsettings.Development.json` (with localhost settings)
- ❌ **Never commit**: Production connection strings, API keys, secrets
- Use environment variables or Azure Key Vault for production secrets

### Database Schema Changes

If you modify the data models:
1. Update the corresponding MongoDB models
2. Update validation schemas
3. Test with existing sample data
4. Document any breaking changes

### API Changes

When adding new endpoints:
1. Follow existing patterns in controllers
2. Add appropriate validation
3. Update Swagger documentation
4. Test all CRUD operations
5. Update README if needed

## Testing

### Manual Testing
1. Use Swagger UI at `/docs`
2. Test with sample data in MongoDB
3. Verify error responses
4. Check validation rules

### Sample API Tests
```bash
# Get all invitations
curl http://localhost:5000/api/wedding-invitations

# Create new invitation
curl -X POST http://localhost:5000/api/wedding-invitations \
  -H "Content-Type: application/json" \
  -d '{"template": {...}, "fonts": {...}, "content": {...}}'
```

## Pull Request Process

1. **Ensure your branch is up to date**
   ```bash
   git checkout main
   git pull origin main
   git checkout your-feature-branch
   git rebase main
   ```

2. **Push your branch**
   ```bash
   git push origin feature/your-feature-name
   ```

3. **Create Pull Request**
   - Provide clear description of changes
   - Include any breaking changes
   - Reference any related issues

4. **Code Review**
   - Address feedback promptly
   - Make requested changes in additional commits
   - Squash commits before merging if requested

## Security Considerations

- Never commit sensitive data (passwords, API keys, production URLs)
- Use HTTPS in production
- Validate all user inputs
- Follow OWASP security guidelines
- Keep dependencies up to date

## Questions or Issues?

- Open a GitHub issue for bugs or feature requests
- Use clear, descriptive titles
- Provide steps to reproduce for bugs
- Include relevant logs or error messages

Thank you for contributing! 🎉