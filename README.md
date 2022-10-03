## Pricer
PriceObserver is a project for telegram bot [**Pricer**](https://t.me/pricer_official_bot) which observes price of items that you added. 

#### Idea
A lot of people nowaday wants to buy products at discount. They visit links of their items 
every week/day to see if price has changed. Bot was created to track automatically prices, so bot users no longer need to take care when the price will go down.

#### Dialog
I implemented dialog architecture between bot and user that could be modified and extended easily. So you can try to implement the prepared architecture in your project with any bot provider like `Telegram/Viber/Facebook`.

The architecture of the dialog locates in `PriceObserver.Dialog` and all necessary data for it in `PriceObserver.Data.InMemory`, and it is not connected to any of bot provider like Telegram/Viber/Facebook. Because of this you can write your own implementation to every bot provider.

#### Parser
The logic for parser was implemented in `PriceObserver.Parser`, it supports a lot of ukranian shops. Architecture of this project is easy to extend, so it doesn't take a lot of time to add new shop.

Also were written `Integration tests` to test whole logic of parser, and besides, test if all shops could be parsed correctly. Another advantage of these tests is that you don't need to run application to test if your new shop-parser works.

#### Deploy 
All steps how to deploy Client solution were described in separate document. Go by [**link**](https://github.com/DanielNovikov/PriceObserver/blob/master/Client/DEPLOY.md) to see the details.

##### Technology stack
`C#`, `.NET 6`, `ASP.NET Core`, `Entity Framework Core`, `AngleSharp`, `Telegram.Bot`, `Blazor`, `SCSS`, `PostgreSQL`
