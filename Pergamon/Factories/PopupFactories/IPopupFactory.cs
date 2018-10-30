
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Pergamon
{
    public interface IPopupFactory
    {
        Popup CreatePopupOnPoint(Point point);
    }
}
