#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ConfigurationManagementSystem.Api/ConfigurationManagementSystem.Api.csproj", "ConfigurationManagementSystem.Api/"]
COPY ["ConfigurationManagementSystem.ServicesConfiguring/ConfigurationManagementSystem.ServicesConfiguring.csproj", "ConfigurationManagementSystem.ServicesConfiguring/"]
COPY ["ConfigurationManagementSystem.Application/ConfigurationManagementSystem.Application.csproj", "ConfigurationManagementSystem.Application/"]
COPY ["ConfigurationManagementSystem.Framework/ConfigurationManagementSystem.Framework.csproj", "ConfigurationManagementSystem.Framework/"]
COPY ["ConfigurationManagementSystem.Domain/ConfigurationManagementSystem.Domain.csproj", "ConfigurationManagementSystem.Domain/"]
COPY ["ConfigurationManagementSystem.Persistence/ConfigurationManagementSystem.Persistence.csproj", "ConfigurationManagementSystem.Persistence/"]
RUN dotnet restore "ConfigurationManagementSystem.Api/ConfigurationManagementSystem.Api.csproj"
COPY . .
WORKDIR "/src/ConfigurationManagementSystem.Api"
RUN dotnet build "ConfigurationManagementSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfigurationManagementSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurationManagementSystem.Api.dll"]