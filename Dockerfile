#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 6002

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TrackService.csproj", ""]
RUN dotnet restore "./TrackService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TrackService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrackService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackService.dll"]