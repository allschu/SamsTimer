FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./Admin.Shared ./Admin.Shared
COPY ./WebUI ./WebUI

# Restore as distinct layers
RUN dotnet restore ./Admin.Shared/Admin.Shared.csproj
RUN dotnet restore ./WebUI/WebUI/WebUI.csproj

# Build and publish a release
RUN dotnet publish ./WebUI/WebUI/WebUI.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "WebUI.dll"]