# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# نسخ ملف المشروع
COPY Sports/Sports.csproj Sports/

# استعادة الحزم
RUN dotnet restore Sports/Sports.csproj

# نسخ جميع الملفات
COPY . ./

# بناء ونشر التطبيق
RUN dotnet publish Sports/Sports.csproj -c Release -o /out

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# ضبط متغير البيئة لتشغيل التطبيق على المنفذ 80
ENV ASPNETCORE_URLS=http://+:5000

# نسخ الملفات المنشورة من مرحلة `build`
COPY --from=build /out ./

# فتح المنفذ 80
EXPOSE 5000

# تشغيل التطبيق
CMD ["dotnet", "Sports.dll"] 