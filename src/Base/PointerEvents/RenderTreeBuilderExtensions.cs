using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using Egil.RazorComponents.Bootstrap.Extensions;

namespace Egil.RazorComponents.Bootstrap.Base.PointerEvents
{
    public static class RenderTreeBuilderExtensions
    {
        private const int DEFAULT_SEQUENCE = -1;

        public static void AddEventListeners(this RenderTreeBuilder builder, HorizontalSwipePointerEventDetector eventHandler, [CallerLineNumber] int sequence = DEFAULT_SEQUENCE)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));
            if (eventHandler is null) return;

            builder.AddAttribute(sequence, HtmlEvents.POINTERDOWN, EventCallback.Factory.Create<UIPointerEventArgs>(eventHandler, eventHandler.OnPointerDownHandler));
            builder.AddAttribute(sequence, HtmlEvents.POINTERMOVE, EventCallback.Factory.Create<UIPointerEventArgs>(eventHandler, eventHandler.OnPointerMoveHandler));
            builder.AddAttribute(sequence, HtmlEvents.POINTERUP, EventCallback.Factory.Create<UIPointerEventArgs>(eventHandler, eventHandler.OnPointerUpHandler));
        }
    }
}