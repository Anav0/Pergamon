
using System.Windows.Media;

namespace Pergamon
{
    public class ColorPickerDesign : ColorPickerViewModel
    {
        public static ColorPickerDesign Instance => new ColorPickerDesign();

        public ColorPickerDesign()
        {
            Instance.StandardColors.Clear();
            Instance.StandardColors.Add(new SolidColorBrush(Colors.Gray));

        }
    }
}
