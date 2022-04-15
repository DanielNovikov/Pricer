FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
EXPOSE 5000

WORKDIR /app

COPY src/PriceObserver/PriceObserver.csproj ./PriceObserver/PriceObserver.csproj
COPY src/PriceObserver.Background/PriceObserver.Background.csproj ./PriceObserver.Background/PriceObserver.Background.csproj
COPY src/PriceObserver.Common/PriceObserver.Common.csproj ./PriceObserver.Common/PriceObserver.Common.csproj
COPY src/PriceObserver.Data/PriceObserver.Data.csproj ./PriceObserver.Data/PriceObserver.Data.csproj
COPY src/PriceObserver.Data.InMemory/PriceObserver.Data.InMemory.csproj ./PriceObserver.Data.InMemory/PriceObserver.Data.InMemory.csproj
COPY src/PriceObserver.Data.Service/PriceObserver.Data.Service.csproj ./PriceObserver.Data.Service/PriceObserver.Data.Service.csproj
COPY src/PriceObserver.Dialog/PriceObserver.Dialog.csproj ./PriceObserver.Dialog/PriceObserver.Dialog.csproj
COPY src/PriceObserver.Parser/PriceObserver.Parser.csproj ./PriceObserver.Parser/PriceObserver.Parser.csproj
COPY src/PriceObserver.Telegram/PriceObserver.Telegram.csproj ./PriceObserver.Telegram/PriceObserver.Telegram.csproj
COPY src/PriceObserver.Web.Api/PriceObserver.Web.Api.csproj ./PriceObserver.Web.Api/PriceObserver.Web.Api.csproj
COPY src/PriceObserver.Web.App/PriceObserver.Web.App.csproj ./PriceObserver.Web.App/PriceObserver.Web.App.csproj
COPY src/PriceObserver.Web.Shared/PriceObserver.Web.Shared.csproj ./PriceObserver.Web.Shared/PriceObserver.Web.Shared.csproj

WORKDIR /app/PriceObserver
RUN dotnet restore

WORKDIR /app
COPY . .

WORKDIR /app/PriceObserver
RUN dotnet publish -c Release -o /publish

WORKDIR /publish
ENTRYPOINT ["dotnet", "PriceObserver.dll"]