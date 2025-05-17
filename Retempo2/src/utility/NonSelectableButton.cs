namespace Retempo2
{
    // Credit to Ivan Stoev on stackoverflow for this
    public class NonSelectableButton : Button
    {
        public NonSelectableButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}
