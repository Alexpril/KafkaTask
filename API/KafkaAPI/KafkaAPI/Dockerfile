FROM mcr.microsoft.com/dotnet/aspnet:5.0
FROM mcr.microsoft.com/dotnet/sdk:latest

COPY bin/Release/net5.0/publish/ App/
WORKDIR /App
EXPOSE 6000
EXPOSE 6001
RUN dotnet dev-certs https --clean && dotnet dev-certs https --verbose
ENV ASPNETCORE_URLS=https://+:6001;http://+:6000
ENTRYPOINT ["dotnet", "KafkaAPI.dll"]
