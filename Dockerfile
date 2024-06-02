FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["AlbumApi/AlbumApi.csproj", "AlbumApi/"]

RUN dotnet restore "AlbumApi/AlbumApi.csproj"

COPY . .
WORKDIR "/src/AlbumApi"

RUN dotnet build "AlbumApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AlbumApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AlbumApi.dll"]