# educonnect-backend

# Add .env file folwing the .env.example

# Migration Commands
dotnet ef migrations add InitialCreate --project EduConnect.Persistence --startup-project EduConnect.API
dotnet ef database update --project EduConnect.Persistence --startup-project EduConnect.API