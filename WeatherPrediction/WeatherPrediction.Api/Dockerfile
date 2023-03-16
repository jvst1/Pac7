#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeatherPrediction.Api/WeatherPrediction.Api.csproj", "WeatherPrediction.Api/"]
COPY ["WeatherPrediction.Domain/WeatherPrediction.Domain.csproj", "WeatherPrediction.Domain/"]
COPY ["WeatherPrediction.Infrastructure/WeatherPrediction.Infrastructure.csproj", "WeatherPrediction.Infrastructure/"]
COPY ["WeatherPrediction.Crosscutting/WeatherPrediction.Crosscutting.csproj", "WeatherPrediction.Crosscutting/"]
COPY ["WeatherPrediction.Application/WeatherPrediction.Application.csproj", "WeatherPrediction.Application/"]
COPY ["WeatherPrediction.Data/WeatherPrediction.Data.csproj", "WeatherPrediction.Data/"]
RUN dotnet restore "WeatherPrediction.Api/WeatherPrediction.Api.csproj"
COPY . .
WORKDIR "/src/WeatherPrediction.Api"
RUN dotnet build "WeatherPrediction.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherPrediction.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherPrediction.Api.dll"]