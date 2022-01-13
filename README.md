#### PriceObserver is a project for telegram bot which observes price of the items that you add.

If you want to track automatically the price of the items that you want to buy, this bot will announce you first when the price changes. So you won't need to go to the links of your items to see if the price has changed **every week/day/hour**, the bot will observe the price instead of you.

I implemented in this project dialog between bot and user that could be modified and extended easily, so you can try to implement the prepared dialog architecture in your project with every bot.

The architecture of the dialog locates in PriceObserver.Dialog and data for it in PriceObserver.Data.InMemory, and it is not connected to any of bot provider like Telegram/Viber/Facebook. Because of this you can write your own implementation to every bot provider.

Link to the bot: [Pricer](https://t.me/pricer_official_bot)
