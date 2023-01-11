using System;
using System.Linq;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class ShopCategoriesMessageBuilder : IShopCategoriesMessageBuilder
{
	private readonly IShopCategoryRepository _shopCategoryRepository;
	private readonly IResourceService _resourceService;

	public ShopCategoriesMessageBuilder(
		IShopCategoryRepository shopCategoryRepository,
		IResourceService resourceService)
	{
		_shopCategoryRepository = shopCategoryRepository;
		_resourceService = resourceService;
	}

	public string Build()
	{
		var shopCategories = _shopCategoryRepository.GetAll();
        
		var shopsInfo = shopCategories
			.OrderBy(x => x.Name)
			.Select(BuildShopCategoryMessage)
			.Aggregate((x, y) => $"{x}{Environment.NewLine}{Environment.NewLine}{y}");

		shopsInfo = Environment.NewLine + shopsInfo;
		
		return _resourceService.Get(ResourceKey.Dialog_AvailableShops, shopsInfo);
	}
	
	private string BuildShopCategoryMessage(ShopCategory category)
	{
		var shopsMessage = category
			.Shops
			.OrderBy(x => x.Name)
			.Select(x => $"- {x.Name} ({x.Host})")
			.Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

		var name = _resourceService.Get(category.Name);
		var title = $"{category.Sign} {name}";
		
		return string.Concat(title, Environment.NewLine, shopsMessage);
	}
}