dotnet ef migrations add "InitialMigrations" --context "ApplicationDBContext"
dotnet ef database update --context "ApplicationDBContext"