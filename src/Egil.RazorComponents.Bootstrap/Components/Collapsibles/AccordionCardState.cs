namespace Egil.RazorComponents.Bootstrap.Components.Collapsibles
{
    internal class AccordionCardState
    {
        public int Index { get; }
        public bool Expanded { get; set; }
        public Collapse? Collapse { get; set; }

        public AccordionCardState(int index)
        {
            Index = index;
        }

        // BANG NOTES: The collapse must be added to the dictionary at this point!
        public void Toggle()
        {
            Collapse!.Toggle();
            Expanded = Collapse!.Expanded;
        }

        public void Hide()
        {
            Collapse!.Hide();
            Expanded = false;
        }
    }
}
