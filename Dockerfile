#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#EXPOSE 80
EXPOSE 443

# Serve on port 8080, we cannot serve on port 80 with a custom user that is not root.
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Create a group and user so we are not running our container and application as root and thus user 0 which is a security issue.
RUN addgroup --system --gid 1000 appgroup && adduser --system --uid 1000 --ingroup appgroup --shell /bin/sh appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["dsf-oidc-web-client.csproj", "."]
RUN dotnet restore "./dsf-oidc-web-client.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dsf-oidc-web-client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dsf-oidc-web-client.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Tell docker that all future commands should run as the appuser user, must use the user number
USER 1000

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dsf-oidc-web-client.dll"]