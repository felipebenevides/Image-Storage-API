﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ImageStorageAPI/ImageStorageAPI.csproj", "ImageStorageAPI/"]
RUN dotnet restore "ImageStorageAPI/ImageStorageAPI.csproj"
COPY . .
WORKDIR "/src/ImageStorageAPI"
RUN dotnet build "ImageStorageAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageStorageAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageStorageAPI.dll"]
