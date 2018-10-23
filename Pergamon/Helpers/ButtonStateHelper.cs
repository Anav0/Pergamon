using System.Windows;
using System.Windows.Documents;

namespace Pergamon
{
    public static class ButtonStateHelper
    {

        public static bool CheckDependencyPropertyState(TextPointer position, DependencyProperty testedProperty, object expectedValue)
        {
            object currentValue = position.Parent.GetValue(testedProperty);

            if (currentValue == null || currentValue == DependencyProperty.UnsetValue)
                return false;

            return currentValue.Equals(expectedValue);
        }


        public static bool CheckTextMarkerPropertyState(TextSelection selectedText, TextMarkerStyle expectedStyle)
        {

            if (selectedText == null)
                return false;

            var startParagraph = selectedText.Start.Paragraph;
            var endParagraph = selectedText.End.Paragraph;

            if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
            {
                TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;

                if (markerStyle == expectedStyle)
                    return true;
                else return false;
            }
            else
            {
                return false;
            }
        }
    }
}
