#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Vitabalans.sln ./
COPY DataAccess/DataAccess.csproj ./DataAccess/
COPY VitabalansApp/VitabalansApp.csproj ./VitabalansApp/

RUN dotnet restore
COPY . .

WORKDIR /src/DataAccess
RUN dotnet build -c Release -o /app
WORKDIR /src/VitabalansApp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "VitabalansApp.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet VitabalansApp.dll