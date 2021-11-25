﻿FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS back-build
EXPOSE 80
EXPOSE 443

WORKDIR /app/backend

COPY PriceObserver/PriceObserver.csproj ./PriceObserver/PriceObserver.csproj
COPY PriceObserver.Authentication/PriceObserver.Authentication.csproj ./PriceObserver.Authentication/PriceObserver.Authentication.csproj
COPY PriceObserver.Background/PriceObserver.Background.csproj ./PriceObserver.Background/PriceObserver.Background.csproj
COPY PriceObserver.Common/PriceObserver.Common.csproj ./PriceObserver.Common/PriceObserver.Common.csproj
COPY PriceObserver.Data/PriceObserver.Data.csproj ./PriceObserver.Data/PriceObserver.Data.csproj
COPY PriceObserver.Data.Seed/PriceObserver.Data.Seed.csproj ./PriceObserver.Data.Seed/PriceObserver.Data.Seed.csproj
COPY PriceObserver.Data.Service/PriceObserver.Data.Service.csproj ./PriceObserver.Data.Service/PriceObserver.Data.Service.csproj
COPY PriceObserver.Dialog/PriceObserver.Dialog.csproj ./PriceObserver.Dialog/PriceObserver.Dialog.csproj
COPY PriceObserver.Parser/PriceObserver.Parser.csproj ./PriceObserver.Parser/PriceObserver.Parser.csproj
COPY PriceObserver.Telegram/PriceObserver.Telegram.csproj ./PriceObserver.Telegram/PriceObserver.Telegram.csproj

COPY PriceObserver.sln ./PriceObserver.sln

RUN dotnet restore
COPY . .
WORKDIR /app/backend/PriceObserver/
RUN dotnet publish -c Release -o /publish

FROM node:14-alpine as front-build
WORKDIR /app/front/source
COPY --from=back-build /app/backend/PriceObserver/ClientApp .
RUN npm install
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=back-build /publish ./

WORKDIR /app/front/wwwroot
COPY --from=front-build . /app/wwwroot

WORKDIR /app
ENTRYPOINT ["dotnet", "PriceObserver.dll"]