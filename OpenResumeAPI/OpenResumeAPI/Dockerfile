FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["OpenResumeAPI/OpenResumeAPI.csproj", "OpenResumeAPI/"]
RUN dotnet restore "OpenResumeAPI/OpenResumeAPI.csproj"
COPY . .
WORKDIR "/src/OpenResumeAPI"
RUN dotnet build "OpenResumeAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "OpenResumeAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "OpenResumeAPI.dll"]