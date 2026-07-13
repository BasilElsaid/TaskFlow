FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY TaskFlow/TaskFlow.csproj TaskFlow/

RUN dotnet restore TaskFlow/TaskFlow.csproj

COPY TaskFlow/ TaskFlow/

WORKDIR /src/TaskFlow

RUN dotnet publish TaskFlow.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TaskFlow.dll"]