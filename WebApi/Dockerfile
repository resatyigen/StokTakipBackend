FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY /bin/Release/net6.0/publish /app
ENTRYPOINT ["dotnet", "WebApi.dll"]