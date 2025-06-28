# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY Sports/Sports.csproj Sports/
RUN dotnet restore Sports/Sports.csproj

COPY . ./
RUN dotnet publish Sports/Sports.csproj -c Release -o /out

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# خلي التطبيق يسمع على البورت الداخلي 5000
ENV ASPNETCORE_URLS=http://+:5000

# نسخ الملفات المنشورة
COPY --from=build /out ./

# كشف البورت الداخلي (مش شرط نعمل EXPOSE في الإنتاج لكن مفيد للتوثيق)
EXPOSE 5000

CMD ["dotnet", "Sports.dll"]
