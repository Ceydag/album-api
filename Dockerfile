# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
# Set the working directory.
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
# Copy the csproj and restore dependencies
COPY *.csproj ./

RUN dotnet restore "Album.Api.csproj"

# Copy everything else and build the app
COPY . ./

RUN dotnet build "Album.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlbumApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlbumApi.dll"]