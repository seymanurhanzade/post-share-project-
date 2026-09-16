FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY react.server.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT [ "dotnet", "react.server.dll" ]