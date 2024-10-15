# Use the .NET 8.0 SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
WORKDIR /src
COPY ["FunFactAPI.csproj", "./"]
RUN dotnet restore "FunFactAPI.csproj"
COPY . .
RUN dotnet build "FunFactAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FunFactAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the .NET 8.0 runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FunFactAPI.dll"]
