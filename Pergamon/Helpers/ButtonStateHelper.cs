
using System.Windows;
using System.Windows.Documents;

namespace Pergamon
{
    public static class ButtonStateHelper
    {

        /// <summary>
        /// Checkes if text at given <paramref name="position"/> has <paramref name="testedProperty"/> of a <paramref name="expectedValue"/>
        /// </summary>
        /// <param name="position">Position of tested text</param>
        /// <param name="testedProperty">Tested property</param>
        /// <param name="expectedValue">expected value of tested tested property/></param>
        /// <returns></returns>
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
