FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY /Build /app
ENTRYPOINT ["dotnet", "WebApi.dll"]