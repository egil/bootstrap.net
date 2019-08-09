namespace Egil.RazorComponents.Bootstrap.Services.PageVisibilityAPI
{
    public readonly struct PageVisibilityChangedEventArgs
    {
        public bool IsPageVisible { get; }

        public PageVisibilityChangedEventArgs(bool isPageVisible)
        {
            IsPageVisible = isPageVisible;
        }
    }
}
