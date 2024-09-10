
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gestion User/Gestion User.csproj", "Gestion User/"]
RUN dotnet restore "Gestion User/Gestion User.csproj"
COPY . .
WORKDIR "/src/Gestion User"
RUN dotnet build "./Gestion User.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gestion User.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gestion User.dll"]

# Utilisez l'image SDK pour construire l'application


