
using System.Windows.Documents;

namespace Pergamon
{
    public interface IHyperLinkFactory
    {
        Hyperlink CreateHyperLinkOnTopOfSelectedText(TextSelection selectedText, string linkUri);
    }
}
