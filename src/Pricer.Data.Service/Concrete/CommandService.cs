using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class CommandService : ICommandService
{
    private readonly ICommandRepository _repository;
    private readonly IResourceRepository _resourceRepository;
    private readonly IResourceService _resourceService;

    public CommandService(
        ICommandRepository repository, 
        IResourceRepository resourceRepository, 
        IResourceService resourceService)
    {
        _repository = repository;
        _resourceRepository = resourceRepository;
        _resourceService = resourceService;
    }

    public string GetTitle(CommandKey key)
    {
        var command = _repository.GetByKey(key);
        return _resourceService.Get(command.ResourceKey);
    }

    public Command GetByTitle(string title)
    {
        var resource = _resourceRepository.GetByValue(title);

        return resource is not null 
            ? _repository.GetByResourceKey(resource.Key) 
            : default;
    }
}