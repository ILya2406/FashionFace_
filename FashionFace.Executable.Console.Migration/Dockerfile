FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base

ENV TZ=UTC
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FashionFace.Executable.Console.Migration/FashionFace.Executable.Console.Migration.csproj", "FashionFace.Executable.Console.Migration/"]
RUN dotnet restore "FashionFace.Executable.Console.Migration/FashionFace.Executable.Console.Migration.csproj"
COPY . .
WORKDIR "/src/FashionFace.Executable.Console.Migration"
RUN dotnet build "./FashionFace.Executable.Console.Migration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./FashionFace.Executable.Console.Migration.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFace.Executable.Console.Migration.dll"]