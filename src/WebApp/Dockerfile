FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs

WORKDIR /app
EXPOSE 80
 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApp.csproj", "./"]
RUN dotnet restore "./WebApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebApp.csproj" -c Release -o /app/build
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs
     
FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebApp.dll"]
