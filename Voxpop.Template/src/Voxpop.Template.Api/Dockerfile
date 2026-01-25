FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Voxpop.Template/src/Voxpop.Template.Api/Voxpop.Template.Api.csproj", "profile/src/Voxpop.Template.Api/"]
RUN dotnet restore "Voxpop.Template/src/Voxpop.Template.Api/Voxpop.Template.Api.csproj"
COPY . .
WORKDIR "/src/Voxpop.Template/src/Voxpop.Template.Api"
RUN dotnet build "./Voxpop.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Voxpop.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Voxpop.Template.Api.dll"]
