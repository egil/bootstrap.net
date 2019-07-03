using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Base;
using Egil.RazorComponents.Bootstrap.Base.Context;
using Egil.RazorComponents.Bootstrap.Base.CssClassValues;
using Egil.RazorComponents.Bootstrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.JSInterop;

namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    public sealed class Collapse : BootstrapParentComponentBase, IPartnerComponent<IToggleForCollapse>
    {
        private const string CollapsedCssClass = "collapse";
        private readonly HashSet<IToggleForCollapse> _togglers = new HashSet<IToggleForCollapse>();
        private ElementRef _domElement;

        [Inject] private IJSRuntime? JSRuntime { get; set; }

        [CssClassToggleParameter("show")] private bool Showing { get; set; }

        [Parameter] public string? Id { get; set; }

        [Parameter] public bool Expanded { get; set; }

        public Collapse()
        {
            DefaultCssClass = CollapsedCssClass;
        }

        public void Toggle()
        {
            Expanded = !Expanded;
            StateHasChanged();
        }

        public void Show()
        {
            if (Expanded) return;
            Toggle();
        }

        public void Hide()
        {
            if (!Expanded) return;
            Toggle();
        }

        protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
        {
            builder.OpenElement(DefaultElementName);
            builder.AddIdAttribute(Id);
            builder.AddClassAttribute(CssClassValue);
            builder.AddMultipleAttributes(AdditionalAttributes);
            builder.AddContent(ChildContent);
            builder.AddElementReferenceCapture(elm => _domElement = elm);
            builder.CloseElement();
        }

        protected override void OnBootstrapInit()
        {
            Showing = Expanded;
            IPartnerComponent<IToggleForCollapse>.Register(this);
        }

        protected override async Task OnAfterRenderAsync()
        {
            await base.OnAfterRenderAsync();

            foreach (var toggle in _togglers)
            {
                toggle.SetExpandedState(Expanded);
            }

            if (Expanded && !Showing)
            {
                await _domElement.Show(JSRuntime);
                Showing = true;
            }

            else if (!Expanded && Showing)
            {
                await _domElement.Hide(JSRuntime);
                Showing = false;
            }
        }

        private void ToggglerOnToggled(object sender, EventArgs e)
        {
            Toggle();
        }

        void IPartnerComponent<IToggleForCollapse>.Connect(IToggleForCollapse toggle)
        {
            if (!_togglers.Add(toggle)) return;
            toggle.OnToggled += ToggglerOnToggled;
            toggle.SetExpandedState(Expanded);
        }

        void IPartnerComponent<IToggleForCollapse>.Disconnect(IToggleForCollapse toggle)
        {
            toggle.OnToggled -= ToggglerOnToggled;
            _togglers.Remove(toggle);
        }

        public void Dispose()
        {
            foreach (var toggle in _togglers)
                toggle.OnToggled -= ToggglerOnToggled;

            IPartnerComponent<IToggleForCollapse>.Unregister(this);
        }
    }
}
