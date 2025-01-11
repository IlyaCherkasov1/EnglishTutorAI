FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EnglishTutorAI.Api/EnglishTutorAI.Api.csproj", "EnglishTutorAI.Api/"]
COPY ["EnglishTutorAI.Application/EnglishTutorAI.Application.csproj", "EnglishTutorAI.Application/"]
COPY ["EnglishTutorAI.Domain/EnglishTutorAI.Domain.csproj", "EnglishTutorAI.Domain/"]
COPY ["EnglishTutorAI.Infrastructure/EnglishTutorAI.Infrastructure.csproj", "EnglishTutorAI.Infrastructure/"]
RUN dotnet restore "EnglishTutorAI.Api/EnglishTutorAI.Api.csproj"
COPY . .
WORKDIR "/src/EnglishTutorAI.Api"
RUN dotnet build "EnglishTutorAI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EnglishTutorAI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnglishTutorAI.Api.dll"]