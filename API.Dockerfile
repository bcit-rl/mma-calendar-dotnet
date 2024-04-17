# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore the dependencies
COPY mma-calendar-dotnet.sln ./
COPY DBClass/DBClass.csproj DBClass/
COPY fight-api/fight-api.csproj fight-api/
COPY ExtractService/ExtractService.csproj ExtractService/
RUN dotnet restore

# Copy the remaining source code and build the application
COPY . .
WORKDIR /src/fight-api
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
EXPOSE 8080
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fight-api.dll"]
