FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копіюємо файл проекту та відновлюємо залежності
COPY ["GadgetRepairApi.csproj", "./"]
RUN dotnet restore "GadgetRepairApi.csproj"

# Копіюємо всі інші файли
COPY . .

# ДОДАНО /p:PublishContainer=false, щоб вимкнути конфлікт із вбудованою збіркою контейнерів .NET
RUN dotnet publish "GadgetRepairApi.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:PublishContainer=false

# Фінальний образ для запуску
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Налаштування порту (стандарт для контейнерів .NET 8+)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Копіюємо результат публікації з попереднього етапу
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GadgetRepairApi.dll"]