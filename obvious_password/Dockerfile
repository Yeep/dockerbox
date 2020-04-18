FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# Copy and publish app and libraries
COPY . .
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "obvious_password.dll"]