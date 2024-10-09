# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 2: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["BackEnd/Gestion User/Gestion User.csproj", "Gestion User/"]
RUN dotnet restore "Gestion User/Gestion User.csproj"

# Copy the entire source code and build the application
COPY . .
WORKDIR "/src/Gestion User"
RUN dotnet build "./Gestion User.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gestion User.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Stage 4: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Gestion User.dll"]
