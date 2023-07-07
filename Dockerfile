FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS publish
WORKDIR /

COPY src/Pricer/Pricer.csproj ./src/Pricer/Pricer.csproj
COPY src/Pricer.Background/Pricer.Background.csproj ./src/Pricer.Background/Pricer.Background.csproj
COPY src/Pricer.Bot/Pricer.Bot.csproj ./src/Pricer.Bot/Pricer.Bot.csproj
COPY src/Pricer.Common/Pricer.Common.csproj ./src/Pricer.Common/Pricer.Common.csproj
COPY src/Pricer.Data.Persistent/Pricer.Data.Persistent.csproj ./src/Pricer.Data.Persistent/Pricer.Data.Persistent.csproj
COPY src/Pricer.Data.InMemory/Pricer.Data.InMemory.csproj ./src/Pricer.Data.InMemory/Pricer.Data.InMemory.csproj
COPY src/Pricer.Data.Service/Pricer.Data.Service.csproj ./src/Pricer.Data.Service/Pricer.Data.Service.csproj
COPY src/Pricer.Dialog/Pricer.Dialog.csproj ./src/Pricer.Dialog/Pricer.Dialog.csproj
COPY src/Pricer.Parser/Pricer.Parser.csproj ./src/Pricer.Parser/Pricer.Parser.csproj

WORKDIR /src/Pricer
RUN dotnet restore 

WORKDIR /
COPY . .

WORKDIR /src/Pricer
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pricer.dll"]