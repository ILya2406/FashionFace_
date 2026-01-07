FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

ENV TZ=UTC
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["FashionFace.Executable.Worker.UserEvents/FashionFace.Executable.Worker.UserEvents.csproj", "FashionFace.Executable.Worker.UserEvents/"]
RUN dotnet restore "FashionFace.Executable.Worker.UserEvents/FashionFace.Executable.Worker.UserEvents.csproj"
COPY . .
WORKDIR "/src/FashionFace.Executable.Worker.UserEvents"
RUN dotnet build "FashionFace.Executable.Worker.UserEvents.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FashionFace.Executable.Worker.UserEvents.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionFace.Executable.Worker.UserEvents.dll"]
