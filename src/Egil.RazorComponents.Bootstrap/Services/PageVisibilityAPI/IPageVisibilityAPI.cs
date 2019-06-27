using System;

namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    /// <summary>
    /// Enables Razor components and Blazor applications to gain access to 
    /// the Page Visibility API in the browser. Can be used to dynamically
    /// stop CPU intensive operations when the page is not visible to the
    /// user.
    /// 
    /// <see cref="https://developer.mozilla.org/en-US/docs/Web/API/Page_Visibility_API"/>
    /// </summary>
    public interface IPageVisibilityAPI
    {
        bool IsPageVisible { get; }

        event EventHandler<PageVisibilityChangedEventArgs> OnPageVisibilityChanged;
    }
}
