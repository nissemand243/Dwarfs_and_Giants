# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . /source
WORKDIR /source/Server

RUN dotnet restore

RUN dotnet publish --configuration Release --output /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5220/tcp
ENV ASPNETCORE_URLS http://*:5220

EXPOSE 7237/tcp
ENV ASPNETCORE_URLS https://*:7237

ENTRYPOINT ["dotnet", "SE_training.Server.dll"]
