FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ENV TZ=UTC
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FashionFace.Executable.WebApi/FashionFace.Executable.WebApi.csproj", "FashionFace.Executable.WebApi/"]
RUN dotnet restore "FashionFace.Executable.WebApi/FashionFace.Executable.WebApi.csproj"
COPY . .
WORKDIR "/src/FashionFace.Executable.WebApi"
RUN dotnet build "FashionFace.Executable.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FashionFace.Executable.WebApi.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFace.Executable.WebApi.dll"]