#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Warehouse.Reporting.Api/Warehouse.Reporting.Api.csproj", "Warehouse.Reporting.Api/"]
COPY ["Warehouse.Contracts/Warehouse.Contracts.csproj", "Warehouse.Contracts/"]
RUN dotnet restore "./Warehouse.Reporting.Api/./Warehouse.Reporting.Api.csproj"
COPY . .
WORKDIR "/src/Warehouse.Reporting.Api"
RUN dotnet build "./Warehouse.Reporting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Warehouse.Reporting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.Reporting.Api.dll"]