# Gebruik de officiële ASP.NET Core Runtime als basisimage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
# Zet de werkdirectory in naar de app map
WORKDIR /app
# Kopieer de gepubliceerde output van de applicatie naar de container
COPY ./publish/ .
# Definieer de poort waarop de applicatie moet draaien
ENV ASPNETCORE_HTTP_PORTS=80
# Expose poort 80 naar buiten de container
EXPOSE 80

# Start de ASP.NET Core applicatie wanneer de container wordt gestart
ENTRYPOINT ["dotnet", "Album.Api.dll"]