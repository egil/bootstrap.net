using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egil.RazorComponents.Bootstrap.Components;
using Egil.RazorComponents.Testing;
using Microsoft.AspNetCore.Components.RenderTree;
using Shouldly;
using Xunit;

namespace Egil.RazorComponents.Bootstrap.NewBase
{
    public class ComponentBaseTest : BootstrapComponentFixture
    {
        #region Dummy Components
        class DummyComponent : ComponentBase
        {
            protected override void OnCompomnentInit() => RegCall();
            protected override Task OnCompomnentInitAsync() => RegCallAsync();
            protected override void OnCompomnentParametersSet() => RegCall();
            protected override Task OnCompomnentParametersSetAsync() => RegCallAsync();
            protected override void OnCompomnentAfterFirstRender() => RegCall();
            protected override Task OnCompomnentAfterFirstRenderAsync() => RegCallAsync();
            protected override void OnCompomnentAfterRender() => RegCall();
            protected override Task OnCompomnentAfterRenderAsync() => RegCallAsync();
            protected override void OnCompomnentDispose() => RegCall();
            protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
            {
                base.DefaultRenderFragment(builder);
                RegCall();
            }
            private void RegCall([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
            {
                CallOrder.Add($"{this.GetType().Name}.{memberName}");
            }
            private Task RegCallAsync([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
            {
                CallOrder.Add($"{this.GetType().Name}.{memberName}");
                return Task.CompletedTask;
            }
        }
        class DummyParentComponent : ParentComponentBase
        {
            protected override void ApplyChildHooks(ComponentBase component)
            {
                RegCall();
                if (component is DummyComponent dc)
                {
                    dc.OnInitHook = _ => RegHook(nameof(OnInitHook));
                    dc.OnInitHookAsync = _ => RegHookAsync(nameof(OnInitHookAsync));
                    dc.OnParametersSetHook = _ => RegHook(nameof(OnParametersSetHook));
                    dc.OnParametersSetHookAsync = _ => RegHookAsync(nameof(OnParametersSetHookAsync));
                    dc.OnAfterFirstRenderHook = _ => RegHook(nameof(OnAfterFirstRenderHook));
                    dc.OnAfterFirstRenderHookAsync = _ => RegHookAsync(nameof(OnAfterFirstRenderHookAsync));
                    dc.OnAfterRenderHook = _ => RegHook(nameof(OnAfterRenderHook));
                    dc.OnAfterRenderHookAsync = _ => RegHookAsync(nameof(OnAfterRenderHookAsync));
                    dc.OnDisposedHook = _ => RegHook(nameof(OnDisposedHook));
                }
            }
            protected override void OnCompomnentInit() => RegCall();
            protected override Task OnCompomnentInitAsync() => RegCallAsync();
            protected override void OnCompomnentParametersSet() => RegCall();
            protected override Task OnCompomnentParametersSetAsync() => RegCallAsync();
            protected override void OnCompomnentAfterFirstRender() => RegCall();
            protected override Task OnCompomnentAfterFirstRenderAsync() => RegCallAsync();
            protected override void OnCompomnentAfterRender() => RegCall();
            protected override Task OnCompomnentAfterRenderAsync() => RegCallAsync();
            protected override void OnCompomnentDispose() => RegCall();
            protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
            {
                base.DefaultRenderFragment(builder);
                RegCall();
            }
            private void RegCall([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
            {
                CallOrder.Add($"{this.GetType().Name}.{memberName}");
            }
            private Task RegCallAsync([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
            {
                CallOrder.Add($"{this.GetType().Name}.{memberName}");

                return Task.CompletedTask;
            }
            private void RegHook(string hookName)
            {
                CallOrder.Add($"{this.GetType().Name}.{hookName}");
            }
            private Task RegHookAsync(string hookName)
            {
                CallOrder.Add($"{this.GetType().Name}.{hookName}");

                return Task.CompletedTask;
            }
        }
        class DummySuperParentComponent : ParentComponentBase
        {
            protected override void ApplyChildHooks(ComponentBase component)
            {
                switch (component)
                {
                    case DummyParentComponent dpc: ApplyDummyParentComponentHooks(dpc); break;
                    case DummyComponent dc: ApplyDummyComponentHooks(dc); break;
                    default: break;
                }
            }
            private void ApplyDummyParentComponentHooks(DummyParentComponent dc)
            {
                RegCall();
                dc.OnInitHook = _ => RegHook(nameof(OnInitHook));
                dc.OnInitHookAsync = _ => RegHookAsync(nameof(OnInitHookAsync));
                dc.OnParametersSetHook = _ => RegHook(nameof(OnParametersSetHook));
                dc.OnParametersSetHookAsync = _ => RegHookAsync(nameof(OnParametersSetHookAsync));
                dc.OnAfterFirstRenderHook = _ => RegHook(nameof(OnAfterFirstRenderHook));
                dc.OnAfterFirstRenderHookAsync = _ => RegHookAsync(nameof(OnAfterFirstRenderHookAsync));
                dc.OnAfterRenderHook = _ => RegHook(nameof(OnAfterRenderHook));
                dc.OnAfterRenderHookAsync = _ => RegHookAsync(nameof(OnAfterRenderHookAsync));
                dc.OnDisposedHook = _ => RegHook(nameof(OnDisposedHook));

                dc.CustomChildHooksInjector = ApplyChildHooks;
            }
            private void ApplyDummyComponentHooks(DummyComponent dc)
            {
                RegCall();
                dc.OnInitHook = _ => RegHook(nameof(OnInitHook));
                dc.OnInitHookAsync = _ => RegHookAsync(nameof(OnInitHookAsync));
                dc.OnParametersSetHook = _ => RegHook(nameof(OnParametersSetHook));
                dc.OnParametersSetHookAsync = _ => RegHookAsync(nameof(OnParametersSetHookAsync));
                dc.OnAfterFirstRenderHook = _ => RegHook(nameof(OnAfterFirstRenderHook));
                dc.OnAfterFirstRenderHookAsync = _ => RegHookAsync(nameof(OnAfterFirstRenderHookAsync));
                dc.OnAfterRenderHook = _ => RegHook(nameof(OnAfterRenderHook));
                dc.OnAfterRenderHookAsync = _ => RegHookAsync(nameof(OnAfterRenderHookAsync));
                dc.OnDisposedHook = _ => RegHook(nameof(OnDisposedHook));
            }

            protected override void OnCompomnentInit() => RegCall();
            protected override Task OnCompomnentInitAsync() => RegCallAsync();
            protected override void OnCompomnentParametersSet() => RegCall();
            protected override Task OnCompomnentParametersSetAsync() => RegCallAsync();
            protected override void OnCompomnentAfterFirstRender() => RegCall();
            protected override Task OnCompomnentAfterFirstRenderAsync() => RegCallAsync();
            protected override void OnCompomnentAfterRender() => RegCall();
            protected override Task OnCompomnentAfterRenderAsync() => RegCallAsync();
            protected override void OnCompomnentDispose() => RegCall();
            protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
            {
                base.DefaultRenderFragment(builder);
                RegCall();
            }
            private void RegCall([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
            {
                CallOrder.Add($"{this.GetType().Name}.{memberName}");
            }
            private Task RegCallAsync([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
            {
                CallOrder.Add($"{this.GetType().Name}.{memberName}");
                return Task.CompletedTask;
            }
            private void RegHook(string hookName)
            {
                CallOrder.Add($"{this.GetType().Name}.{hookName}");
            }
            private Task RegHookAsync(string hookName)
            {
                CallOrder.Add($"{this.GetType().Name}.{hookName}");
                return Task.CompletedTask;
            }
        }

        class DummyTrackingParentCompenent : ParentComponentBase, IChildTrackingParentComponent
        {
            private List<ComponentBase> Children { get; } = new List<ComponentBase>();

            protected internal override void DefaultRenderFragment(RenderTreeBuilder builder)
            {
                Class = $"c-{Children.Count}";
                base.DefaultRenderFragment(builder);
            }

            void IChildTrackingParentComponent.AddChild(ComponentBase component)
            {
                Children.Add(component);
                Invoke(StateHasChanged);
            }

            void IChildTrackingParentComponent.RemoveChild(ComponentBase component)
            {
                Children.Remove(component);
            }
        }
        #endregion

        private static List<string> CallOrder { get; set; }

        public ComponentBaseTest()
        {
            CallOrder = new List<string>();
        }

        [Fact(DisplayName = "Lify-cycle methods and hooks are called in expected order")]
        public void MyTestMethod()
        {
            var component = Component<DummySuperParentComponent>()
                .WithChildContent(Fragment<DummyParentComponent>().WithChildContent(
                    Fragment<DummyComponent>()));

            var result = component.Render();

            CallOrder.ShouldBe(new string[] {
                "DummySuperParentComponent.OnCompomnentInit",
                "DummySuperParentComponent.OnCompomnentInitAsync",
                "DummySuperParentComponent.OnCompomnentParametersSet",
                "DummySuperParentComponent.OnCompomnentParametersSetAsync",
                "DummySuperParentComponent.DefaultRenderFragment",
                "DummySuperParentComponent.ApplyDummyParentComponentHooks",
                "DummyParentComponent.OnCompomnentInit",
                "DummySuperParentComponent.OnInitHook",
                "DummyParentComponent.OnCompomnentInitAsync",
                "DummySuperParentComponent.OnInitHookAsync",
                "DummyParentComponent.OnCompomnentParametersSet",
                "DummySuperParentComponent.OnParametersSetHook",
                "DummyParentComponent.OnCompomnentParametersSetAsync",
                "DummySuperParentComponent.OnParametersSetHookAsync",
                "DummyParentComponent.DefaultRenderFragment",
                "DummyParentComponent.ApplyChildHooks",
                "DummySuperParentComponent.ApplyDummyComponentHooks",
                "DummyComponent.OnCompomnentInit",
                "DummyParentComponent.OnInitHook",
                "DummySuperParentComponent.OnInitHook",
                "DummyComponent.OnCompomnentInitAsync",
                "DummyParentComponent.OnInitHookAsync",
                "DummySuperParentComponent.OnInitHookAsync",
                "DummyComponent.OnCompomnentParametersSet",
                "DummyParentComponent.OnParametersSetHook",
                "DummySuperParentComponent.OnParametersSetHook",
                "DummyComponent.OnCompomnentParametersSetAsync",
                "DummyParentComponent.OnParametersSetHookAsync",
                "DummySuperParentComponent.OnParametersSetHookAsync",
                "DummyComponent.DefaultRenderFragment",
                "DummySuperParentComponent.OnCompomnentAfterFirstRender",
                "DummySuperParentComponent.OnCompomnentAfterRender",
                "DummySuperParentComponent.OnCompomnentAfterFirstRenderAsync",
                "DummySuperParentComponent.OnCompomnentAfterRenderAsync",
                "DummyParentComponent.OnCompomnentAfterFirstRender",
                "DummySuperParentComponent.OnAfterFirstRenderHook",
                "DummyParentComponent.OnCompomnentAfterRender",
                "DummySuperParentComponent.OnAfterFirstRenderHook",
                "DummyParentComponent.OnCompomnentAfterFirstRenderAsync",
                "DummySuperParentComponent.OnAfterFirstRenderHookAsync",
                "DummyParentComponent.OnCompomnentAfterRenderAsync",
                "DummySuperParentComponent.OnAfterRenderHookAsync",
                "DummyComponent.OnCompomnentAfterFirstRender",
                "DummyParentComponent.OnAfterFirstRenderHook",
                "DummySuperParentComponent.OnAfterFirstRenderHook",
                "DummyComponent.OnCompomnentAfterRender",
                "DummyParentComponent.OnAfterFirstRenderHook",
                "DummySuperParentComponent.OnAfterFirstRenderHook",
                "DummyComponent.OnCompomnentAfterFirstRenderAsync",
                "DummyParentComponent.OnAfterFirstRenderHookAsync",
                "DummySuperParentComponent.OnAfterFirstRenderHookAsync",
                "DummyComponent.OnCompomnentAfterRenderAsync",
                "DummyParentComponent.OnAfterRenderHookAsync",
                "DummySuperParentComponent.OnAfterRenderHookAsync",
                "DummySuperParentComponent.OnCompomnentDispose",
                "DummyParentComponent.OnCompomnentDispose",
                "DummySuperParentComponent.OnDisposedHook",
                "DummyComponent.OnCompomnentDispose",
                "DummyParentComponent.OnDisposedHook",
                "DummySuperParentComponent.OnDisposedHook" });
        }

        [Fact(DisplayName = "IChildTrackingParent component has child components added to its Children collection")]
        public void MyTestMethodfdfd()
        {
            var component = Component<DummyTrackingParentCompenent>()
                .WithChildContent(Fragment<DummyComponent>(), Fragment<DummyComponent>());

            var result = component.Render();

            result.ShouldBe(@"<div class=""c-2""><div/><div/></div>");
        }
    }
}
