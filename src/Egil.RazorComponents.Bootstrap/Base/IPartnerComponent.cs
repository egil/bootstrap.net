using System;
using Microsoft.AspNetCore.Components;

namespace Egil.RazorComponents.Bootstrap.Base.Context
{
    public interface IPartnerComponent : IBootstrapComponent, IDisposable
    {
        public string? Id { get; set; }
    }

    public interface IPartnerComponent<TBootstrapComponent> : IPartnerComponent
        where TBootstrapComponent : IBootstrapComponent
    {
        void Connect(TBootstrapComponent component);

        void Disconnect(TBootstrapComponent component);

        protected static void Register(IPartnerComponent<TBootstrapComponent> partner)
        {
            if (partner.Id is null) return;
            if (partner.BootstrapContext is null) return;

            partner.BootstrapContext.RegisterPartner(partner);
        }

        protected static void Unregister(IPartnerComponent<TBootstrapComponent> partner)
        {
            if (partner.Id is null) return;
            if (partner.BootstrapContext is null) return;

            partner.BootstrapContext.UnregisterPartner(partner);
        }
    }
}
