using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class CommandService : ICommandService
{
    private readonly ICommandRepository _repository;
    private readonly IResourceRepository _resourceRepository;

    public CommandService(
        ICommandRepository repository, 
        IResourceRepository resourceRepository)
    {
        _repository = repository;
        _resourceRepository = resourceRepository;
    }

    public string GetTitle(CommandKey key)
    {
        var command = _repository.GetByKey(key);
        var resource =  _resourceRepository.GetByKey(command.ResourceKey);

        return resource.Value;
    }

    public Command GetByTitle(string title)
    {
        var resource = _resourceRepository.GetByValue(title);

        return resource is not null 
            ? _repository.GetByResourceKey(resource.Key) 
            : null;
    }
}