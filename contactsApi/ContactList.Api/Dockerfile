#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactList.Api/ContactList.Api.csproj", "ContactList.Api/"]
COPY ["ContactList.Infrastructure/ContactList.Infrastructure.csproj", "ContactList.Infrastructure/"]
COPY ["ContactList.core/ContactList.Core.csproj", "ContactList.core/"]
RUN dotnet restore "ContactList.Api/ContactList.Api.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ContactList.Api/ContactList.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactList.Api/ContactList.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactList.Api.dll"]