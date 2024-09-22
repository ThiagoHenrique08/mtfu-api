#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.401 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MoreThanFollowUp.API/MoreThanFollowUp.API.csproj", "MoreThanFollowUp.API/"]
COPY ["MoreThanFollowUp.Application/MoreThanFollowUp.Application.csproj", "MoreThanFollowUp.Application/"]
COPY ["MoreThanFollowUp.Domain/MoreThanFollowUp.Domain.csproj", "MoreThanFollowUp.Domain/"]
COPY ["MoreThanFollowUp.Infrastructure/MoreThanFollowUp.Infrastructure.csproj", "MoreThanFollowUp.Infrastructure/"]
RUN dotnet restore "./MoreThanFollowUp.API/MoreThanFollowUp.API.csproj"
COPY . .
RUN dotnet build "./MoreThanFollowUp.API/MoreThanFollowUp.API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.401 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./MoreThanFollowUp.Infrastructure/MoreThanFollowUp.Infrastructure.csproj"
COPY . .
WORKDIR "/src/MoreThanFollowUp.Infrastructure"
RUN dotnet build "./MoreThanFollowUp.Infrastructure.csproj" -c Release -o /app/migration


FROM build AS publish
RUN dotnet publish "./MoreThanFollowUp.API/MoreThanFollowUp.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MoreThanFollowUp.API.dll"]