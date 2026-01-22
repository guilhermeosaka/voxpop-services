FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["identity/src/Voxpop.Identity.Api/Voxpop.Identity.Api.csproj", "identity/src/Voxpop.Identity.Api/"]
RUN dotnet restore "identity/src/Voxpop.Identity.Api/Voxpop.Identity.Api.csproj"
COPY . .
WORKDIR "/src/identity/src/Voxpop.Identity.Api"
RUN dotnet build "./Voxpop.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Voxpop.Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Voxpop.Identity.Api.dll"]
