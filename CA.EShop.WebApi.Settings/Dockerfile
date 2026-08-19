#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CA.EShop.WebApi.Settings/CA.EShop.WebApi.Settings.csproj", "CA.EShop.WebApi.Settings/"]
COPY ["CA.EShop.WebApi.Presentation/CA.EShop.WebApi.Presentation.csproj", "CA.EShop.WebApi.Presentation/"]
COPY ["CA.EShop.Appliction/CA.EShop.Application.csproj", "CA.EShop.Appliction/"]
COPY ["CA.EShop.Domain/CA.EShop.Domain.csproj", "CA.EShop.Domain/"]
RUN dotnet restore "CA.EShop.WebApi.Settings/CA.EShop.WebApi.Settings.csproj"
COPY . .
WORKDIR "/src/CA.EShop.WebApi.Settings"
RUN dotnet build "CA.EShop.WebApi.Settings.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CA.EShop.WebApi.Settings.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CA.EShop.WebApi.Settings.dll"]
