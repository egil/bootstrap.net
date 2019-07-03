using Egil.RazorComponents.Bootstrap.Base.Context;

namespace Egil.RazorComponents.Bootstrap.Base
{
    public interface IBootstrapContext
    {
        void ConnectToPartner<TBootstrapComponent>(string partnerId, TBootstrapComponent bootstrapComponent) where TBootstrapComponent : IBootstrapComponent;
        void DisconnectFromPartner<TBootstrapComponent>(string partnerId, TBootstrapComponent bootstrapComponent) where TBootstrapComponent : IBootstrapComponent;
        void RegisterPartner<TBootstrapComponent>(IPartnerComponent<TBootstrapComponent> partner) where TBootstrapComponent : IBootstrapComponent;
        void UnregisterPartner(IPartnerComponent component);
    }
}
