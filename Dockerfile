FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
EXPOSE 5000

WORKDIR /app

COPY PriceObserver/PriceObserver.csproj ./PriceObserver/PriceObserver.csproj
COPY PriceObserver.Background/PriceObserver.Background.csproj ./PriceObserver.Background/PriceObserver.Background.csproj
COPY PriceObserver.Common/PriceObserver.Common.csproj ./PriceObserver.Common/PriceObserver.Common.csproj
COPY PriceObserver.Data/PriceObserver.Data.csproj ./PriceObserver.Data/PriceObserver.Data.csproj
COPY PriceObserver.Data.InMemory/PriceObserver.Data.InMemory.csproj ./PriceObserver.Data.InMemory/PriceObserver.Data.InMemory.csproj
COPY PriceObserver.Data.Service/PriceObserver.Data.Service.csproj ./PriceObserver.Data.Service/PriceObserver.Data.Service.csproj
COPY PriceObserver.Dialog/PriceObserver.Dialog.csproj ./PriceObserver.Dialog/PriceObserver.Dialog.csproj
COPY PriceObserver.Dialog.Tests/PriceObserver.Dialog.Tests.csproj ./PriceObserver.Dialog.Tests/PriceObserver.Dialog.Tests.csproj
COPY PriceObserver.Parser/PriceObserver.Parser.csproj ./PriceObserver.Parser/PriceObserver.Parser.csproj
COPY PriceObserver.Parser.Tests/PriceObserver.Parser.Tests.csproj ./PriceObserver.Parser.Tests/PriceObserver.Parser.Tests.csproj
COPY PriceObserver.Telegram/PriceObserver.Telegram.csproj ./PriceObserver.Telegram/PriceObserver.Telegram.csproj
COPY PriceObserver.Web.Api/PriceObserver.Web.Api.csproj ./PriceObserver.Web.Api/PriceObserver.Web.Api.csproj
COPY PriceObserver.Web.App/PriceObserver.Web.App.csproj ./PriceObserver.Web.App/PriceObserver.Web.App.csproj
COPY PriceObserver.Web.Shared/PriceObserver.Web.Shared.csproj ./PriceObserver.Web.Shared/PriceObserver.Web.Shared.csproj

COPY PriceObserver.sln ./PriceObserver.sln

RUN dotnet restore
COPY . .

WORKDIR /app/PriceObserver/
RUN dotnet publish -c Release -o /publish

WORKDIR /publish
ENTRYPOINT ["dotnet", "PriceObserver.dll"]