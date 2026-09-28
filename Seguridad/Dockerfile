FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Abstracciones/Abstracciones.csproj", "Abstracciones/"]
COPY ["DA/DA.csproj","DA/"]
COPY ["Flujo/Flujo.csproj", "Flujo/"]
COPY ["API/API.csproj","API/"]

RUN --mount=type=secret,id=GH_PACKAGES_TOKEN \
    dotnet nuget add source https://nuget.pkg.github.com/JRaulRP196/index.json \
      --name github \
      --username JRaulRP196 \
      --password "$(cat /run/secrets/GH_PACKAGES_TOKEN)" \
      --store-password-in-clear-text \
    && dotnet restore "API/API.csproj"

COPY . .
WORKDIR /src/API
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT [ "dotnet", "API.dll" ]