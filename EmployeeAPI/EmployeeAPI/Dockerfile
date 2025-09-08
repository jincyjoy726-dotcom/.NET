# Use the .NET 8 SDK as the build environment
    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    WORKDIR /src

    # Copy the project file and restore dependencies
    COPY ["EmployeeAPI/EmployeeAPI.csproj", "EmployeeAPI/"]
    RUN dotnet restore "EmployeeAPI/EmployeeAPI.csproj"

    # Copy the rest of the source code
    COPY ./EmployeeAPI/ ./EmployeeAPI/
    WORKDIR "/src/EmployeeAPI"
    RUN dotnet build "EmployeeAPI.csproj" -c Release -o /app/build

    # Publish the application
    FROM build AS publish
    RUN dotnet publish "EmployeeAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

    # Use the lightweight .NET 8 ASP.NET runtime for the final image
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "EmployeeAPI.dll"]