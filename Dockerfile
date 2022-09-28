FROM mcr.microsoft.com/dotnet/aspnet:6.0.9-bullseye-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TemperatureDockerized/TemperatureDockerized.csproj", "TemperatureDockerized/"]
RUN dotnet restore "TemperatureDockerized/TemperatureDockerized.csproj"
COPY . .
WORKDIR "/src/TemperatureDockerized"
RUN dotnet build "TemperatureDockerized.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TemperatureDockerized.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemperatureDockerized.dll"]
