using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;

namespace Egil.RazorComponents.Bootstrap.Helpers
{
    internal static class RenderTreeBuilderExtensions
    {
        private const int DEFAULT_SEQUENCE = -1;

        public static void OpenElement(this RenderTreeBuilder builder, string elementName, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.OpenElement(sequence, elementName);
        }

        public static void OpenComponent<TComponent>(this RenderTreeBuilder builder, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE) where TComponent : IComponent
        {
            builder.OpenComponent<TComponent>(sequence);
        }

        public static void AddAttribute(this RenderTreeBuilder builder, string name, string value, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.AddAttribute(sequence, name, value);
        }

        public static void AddAttribute(this RenderTreeBuilder builder, string name, bool value, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.AddAttribute(sequence, name, value);
        }

        public static void AddAttribute(this RenderTreeBuilder builder, string name, MulticastDelegate value, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.AddAttribute(sequence, name, value);
        }

        public static void AddAttribute(this RenderTreeBuilder builder, string name, object value, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.AddAttribute(sequence, name, value);
        }

        public static void AddContent(this RenderTreeBuilder builder, RenderFragment fragment, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.AddContent(sequence, fragment);
        }

        public static void AddMarkupContent(this RenderTreeBuilder builder, string markupContent, [System.Runtime.CompilerServices.CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            builder.AddMarkupContent(sequence, markupContent);
        }

        public static void AddClassAttribute(this RenderTreeBuilder builder, string value)
        {
            builder.AddAttribute("class", value);
        }

        public static void AddRoleAttribute(this RenderTreeBuilder builder, string value)
        {
            builder.AddAttribute("role", value);
        }

        public static void AddAriaLabelAttribute(this RenderTreeBuilder builder, string value)
        {
            builder.AddAttribute("aria-label", value);
        }

        public static void AddAriaHiddenAttribute(this RenderTreeBuilder builder, bool value = true)
        {
            builder.AddAttribute("aria-hidden", value);
        }

    }
}