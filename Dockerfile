FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS publish
WORKDIR /

COPY Client/src/Pricer.Client/Pricer.Client.csproj ./Client/src/Pricer.Client/Pricer.Client.csproj
COPY Client/src/Pricer.Background/Pricer.Background.csproj .Client//src/Pricer.Background/Pricer.Background.csproj
COPY Client/src/Pricer.Dialog.Callback/Pricer.Dialog.Callback.csproj ./Client/src/Pricer.Dialog.Callback/Pricer.Dialog.Callback.csproj
COPY Client/src/Pricer.Dialog.Common/Pricer.Dialog.Common.csproj ./Client/src/Pricer.Dialog.Common/Pricer.Dialog.Common.csproj
COPY Client/src/Pricer.Dialog.Input/Pricer.Dialog.Input.csproj ./Client/src/Pricer.Dialog.Input/Pricer.Dialog.Input.csproj
COPY Client/src/Pricer.Dialog.Telegram/Pricer.Dialog.Telegram.csproj ./Client/src/Pricer.Dialog.Telegram/Pricer.Dialog.Telegram.csproj
COPY Client/src/Pricer.Web.Api/Pricer.Web.Api.csproj ./Client/src/Pricer.Web.Api/Pricer.Web.Api.csproj
COPY Client/src/Pricer.Web.App/Pricer.Web.App.csproj ./Client/src/Pricer.Web.App/Pricer.Web.App.csproj
COPY Client/src/Pricer.Web.Shared/Pricer.Web.Shared.csproj ./Client/src/Pricer.Web.Shared/Pricer.Web.Shared.csproj

COPY Shared/src/Pricer.Common/Pricer.Common.csproj ./Shared/src/Pricer.Common/Pricer.Common.csproj
COPY Shared/src/Pricer.Data.Persistent/Pricer.Data.Persistent.csproj ./Shared/src/Pricer.Data.Persistent/Pricer.Data.Persistent.csproj
COPY Shared/src/Pricer.Data.InMemory/Pricer.Data.InMemory.csproj ./Shared/src/Pricer.Data.InMemory/Pricer.Data.InMemory.csproj
COPY Shared/src/Pricer.Data.Service/Pricer.Data.Service.csproj ./Shared/src/Pricer.Data.Service/Pricer.Data.Service.csproj
COPY Shared/src/Pricer.Parser/Pricer.Parser.csproj ./Shared/src/Pricer.Parser/Pricer.Parser.csproj
COPY Shared/src/Pricer.Telegram/Pricer.Telegram.csproj ./Shared/src/Pricer.Telegram/Pricer.Telegram.csproj

WORKDIR /Client/src/Pricer.Client
RUN dotnet restore 

WORKDIR /
COPY . .

WORKDIR /Client/src/Pricer.Client
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pricer.Client.dll"]