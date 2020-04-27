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

# Get a random selection of Wikimedia images
WORKDIR /app/images
RUN for i in {1..5} \
    do \
        curl -S 'https://commons.wikimedia.org/w/api.php?action=query&generator=random&grnnamespace=6&prop=imageinfo&iiprop=url&iiurlwidth=640&format=json' | \
        jq '.query.pages[].imageinfo[].thumburl' | xargs curl -O \
    done

WORKDIR /app
ENTRYPOINT ["dotnet", "directory_traversal.dll"]