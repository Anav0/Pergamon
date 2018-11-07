
using System.Windows.Controls;
using System.Windows.Documents;

namespace Pergamon
{
    public static class RichTextBoxExtentions
    {
        public static System.Windows.Point GetEditorPointToScreen(this RichTextBox editor)
        {
            var rect = editor.CaretPosition.GetCharacterRect(LogicalDirection.Backward);
            return editor.PointToScreen(rect.BottomRight);
        }
    }
}
