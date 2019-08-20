FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1803 AS base
WORKDIR app
COPY  FirstApi/Publish .
EXPOSE 5005
ENTRYPOINT ["dotnet", "FirstApi.dll"]