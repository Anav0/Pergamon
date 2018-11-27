using Pergamon;
using System.Windows.Documents;

namespace Pergamon
{

    public class MarkupConverterImpl : IMarkupConverter
    {
        public string ConvertXamlToHtml(string xamlText)
        {
            return XamlToHtmlConverter.ConvertXamlToHtml(xamlText, false);
        }

        public string ConvertHtmlToXaml(string htmlText)
        {
            return HtmlToXamlConverter.ConvertHtmlToXaml(htmlText, true);
        }

        public string ConvertRtfToHtml(FlowDocument doc)
        {
            return RtfToHtmlConverter.ConvertRtfToHtml(doc);
        }

        public string ConvertHtmlToRtf(string htmlText)
        {
            return HtmlToRtfConverter.ConvertHtmlToRtf(htmlText);
        }
    }
}
