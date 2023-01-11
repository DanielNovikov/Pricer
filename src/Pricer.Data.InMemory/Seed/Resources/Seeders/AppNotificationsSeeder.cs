using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Data.InMemory.Seed.Resources.Seeders;

public class AppNotificationsSeeder
{
	public static void Seed(IList<Resource> resources)
	{
		resources.AddResource(
			ResourceKey.AppNotification_HowToAddItem,
			@"💸 Як додати товар за ціною котрого ви хотіли б слідкувати? 
❗ Наша команда створила відео-інструкцію як це зробити!",
			@"💸 Как добавить товар за ценой которого вы хотели бы следить? 
❗ Наша команда создала видео-инструкцию как это сделать!");
	}
}