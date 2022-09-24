FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS publish
WORKDIR /

COPY src/PriceObserver/PriceObserver.csproj ./src/PriceObserver/PriceObserver.csproj
COPY src/PriceObserver.Background/PriceObserver.Background.csproj ./src/PriceObserver.Background/PriceObserver.Background.csproj
COPY src/PriceObserver.Common/PriceObserver.Common.csproj ./src/PriceObserver.Common/PriceObserver.Common.csproj
COPY src/PriceObserver.Data.Persistent/PriceObserver.Data.Persistent.csproj ./src/PriceObserver.Data.Persistent/PriceObserver.Data.Persistent.csproj
COPY src/PriceObserver.Data.InMemory/PriceObserver.Data.InMemory.csproj ./src/PriceObserver.Data.InMemory/PriceObserver.Data.InMemory.csproj
COPY src/PriceObserver.Data.Service/PriceObserver.Data.Service.csproj ./src/PriceObserver.Data.Service/PriceObserver.Data.Service.csproj
COPY src/PriceObserver.Dialog/PriceObserver.Dialog.csproj ./src/PriceObserver.Dialog/PriceObserver.Dialog.csproj
COPY src/PriceObserver.Parser/PriceObserver.Parser.csproj ./src/PriceObserver.Parser/PriceObserver.Parser.csproj
COPY src/PriceObserver.Telegram/PriceObserver.Telegram.csproj ./src/PriceObserver.Telegram/PriceObserver.Telegram.csproj
COPY src/PriceObserver.Web.Api/PriceObserver.Web.Api.csproj ./src/PriceObserver.Web.Api/PriceObserver.Web.Api.csproj
COPY src/PriceObserver.Web.App/PriceObserver.Web.App.csproj ./src/PriceObserver.Web.App/PriceObserver.Web.App.csproj
COPY src/PriceObserver.Web.Shared/PriceObserver.Web.Shared.csproj ./src/PriceObserver.Web.Shared/PriceObserver.Web.Shared.csproj

WORKDIR /src/PriceObserver
RUN dotnet restore 

WORKDIR /
COPY . .

WORKDIR /src/PriceObserver
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceObserver.dll"]