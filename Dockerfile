FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . /app
RUN dotnet restore /app/BotDataStore.sln

WORKDIR /app
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out /app
ENTRYPOINT ["dotnet", "BotData.Api.dll"]