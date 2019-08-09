namespace Egil.RazorComponents.Bootstrap.Services.EventBus
{
    public interface IEventType
    {
        string Key { get; }
        bool CacheLatest { get; }
    }
}
