dotnet restore --source "https://api.nuget.org/v3/index.json" --source "https://www.myget.org/F/coolector/api/v3/index.json"  --source "https://www.myget.org/F/sergeydushkin/api/v3/index.json" --no-cache
dotnet pack ./ServiceDesk.Services.Tickets.Shared/ServiceDesk.Services.Tickets.Shared.csproj -o .