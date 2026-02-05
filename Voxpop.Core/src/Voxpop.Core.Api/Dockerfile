FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Voxpop.Core/src/Voxpop.Core.Api/Voxpop.Core.Api.csproj", "Voxpop.Core/src/Voxpop.Core.Api/"]
RUN dotnet restore "Voxpop.Core/src/Voxpop.Core.Api/Voxpop.Core.Api.csproj"
COPY . .
WORKDIR "/src/Voxpop.Core/src/Voxpop.Core.Api"
RUN dotnet build "./Voxpop.Core.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Voxpop.Core.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
USER root
RUN apt-get update && apt-get install -y libkrb5-3 && rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Voxpop.Core.Api.dll"]
