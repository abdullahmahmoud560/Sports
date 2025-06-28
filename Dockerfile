# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Sports/Sports.csproj Sports/
RUN dotnet restore Sports/Sports.csproj

COPY . ./
RUN dotnet publish Sports/Sports.csproj -c Release -o /out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_ENVIRONMENT=Production

COPY --from=build /out ./

EXPOSE 5000

CMD ["dotnet", "Sports.dll"]
