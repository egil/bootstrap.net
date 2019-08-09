using System;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Base;

namespace Egil.RazorComponents.Bootstrap.Base.Context
{
    //public class BootstrapContext : IBootstrapContext
    //{
    //    private readonly Dictionary<string, IPartnerComponent> _partners = new Dictionary<string, IPartnerComponent>();
    //    private readonly Dictionary<string, List<IBootstrapComponent>> _partnerContacts = new Dictionary<string, List<IBootstrapComponent>>();

    //    public void ConnectToPartner<TBootstrapComponent>(string partnerId, TBootstrapComponent bootstrapComponent) where TBootstrapComponent : IBootstrapComponent
    //    {
    //        if (_partners.TryGetValue(partnerId, out var partner))
    //        {
    //            ((IPartnerComponent<TBootstrapComponent>)partner).Connect(bootstrapComponent);
    //        }
    //        else if (_partnerContacts.TryGetValue(partnerId, out var contacts))
    //        {
    //            contacts.Add(bootstrapComponent);
    //        }
    //        else
    //        {
    //            _partnerContacts.Add(partnerId, new List<IBootstrapComponent> { bootstrapComponent });
    //        }
    //    }

    //    public void DisconnectFromPartner<TBootstrapComponent>(string partnerId, TBootstrapComponent bootstrapComponent) where TBootstrapComponent : IBootstrapComponent
    //    {
    //        if (_partners.TryGetValue(partnerId, out var partner))
    //        {
    //            ((IPartnerComponent<TBootstrapComponent>)partner).Disconnect(bootstrapComponent);
    //        }
    //        else
    //        {
    //            _partnerContacts.Remove(partnerId);
    //        }
    //    }

    //    public void RegisterPartner<TBootstrapComponent>(IPartnerComponent<TBootstrapComponent> partner) where TBootstrapComponent : IBootstrapComponent
    //    {
    //        if (partner?.Id is null) throw new ArgumentNullException("Partner and partner.Id cannot be null");

    //        _partners.Add(partner.Id, partner);

    //        if (_partnerContacts.TryGetValue(partner.Id, out var contacts))
    //        {
    //            _partnerContacts.Remove(partner.Id);
    //            foreach (var contact in contacts)
    //            {
    //                partner.Connect((TBootstrapComponent)contact);
    //            }
    //        }
    //    }

    //    public void UnregisterPartner(IPartnerComponent partner)
    //    {
    //        if (partner?.Id is null) throw new ArgumentNullException("Partner and partner.Id cannot be null");

    //        _partners.Remove(partner.Id);
    //    }
    //}
}
