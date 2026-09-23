FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["Abstracciones/Abstracciones.csproj", "Abstracciones/"]
COPY ["DA/DA.csproj","DA/"]
COPY ["Flujo/Flujo.csproj","Flujo/"]
COPY ["API/API.csproj","API/"]

RUN dotnet restore "API/API.csproj"

COPY . .
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]
