#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TestCore/TestCore.csproj", "TestCore/"]
RUN dotnet restore "TestCore/TestCore.csproj"
COPY . .
WORKDIR "/src/TestCore"
RUN dotnet build "TestCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestCore.dll"]