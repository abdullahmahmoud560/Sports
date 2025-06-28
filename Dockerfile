
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY Sports/Sports.csproj Sports/

RUN dotnet restore Sports/Sports.csproj

COPY . ./

RUN dotnet publish Sports/Sports.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000

COPY --from=build /out ./

EXPOSE 5000

# تشغيل التطبيق
CMD ["dotnet", "Sports.dll"]