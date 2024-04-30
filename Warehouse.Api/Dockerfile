#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Warehouse.Api/Warehouse.Api.csproj", "Warehouse.Api/"]
COPY ["Warehouse.Application/Warehouse.Application.csproj", "Warehouse.Application/"]
COPY ["Warehouse.Domain/Warehouse.Domain.csproj", "Warehouse.Domain/"]
COPY ["Warehouse.Persistence.Abstractions/Warehouse.Persistence.Abstractions.csproj", "Warehouse.Persistence.Abstractions/"]
COPY ["Warehouse.Security/Warehouse.Security.csproj", "Warehouse.Security/"]
COPY ["Warehouse.Infrastructure/Warehouse.Infrastructure.csproj", "Warehouse.Infrastructure/"]
COPY ["Warehouse.Persistence.EF/Warehouse.Persistence.EF.csproj", "Warehouse.Persistence.EF/"]
RUN dotnet restore "./Warehouse.Api/./Warehouse.Api.csproj"
COPY . .
WORKDIR "/src/Warehouse.Api"
RUN dotnet build "./Warehouse.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Warehouse.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.Api.dll"]