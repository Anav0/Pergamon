
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Pergamon
{
    public static class ImageHelpers
    {

        public static void InsertImageWithHookedEventsAtCaretPosition(System.Windows.Controls.Image image, TextPointer caretPosition)
        {
            image.LostFocus += OnLostFocus;
            image.MouseDown += OnMouseDown;
            image.Stretch = System.Windows.Media.Stretch.None;

            var imageContainer = new InlineUIContainer(image, caretPosition);
            imageContainer.BaselineAlignment = BaselineAlignment.Baseline;
            caretPosition?.Paragraph?.Inlines.Add(imageContainer);
        }

        public static void InsertImageWithHookedEvents(System.Windows.Controls.Image image, FlowDocument document)
        {
            image.LostFocus += OnLostFocus;
            image.MouseDown += OnMouseDown;
            image.Stretch = System.Windows.Media.Stretch.None;

            var imageContainer = new BlockUIContainer(image);
            document.Blocks.Add(imageContainer);
        }


        private static void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            AdornerLayer.GetAdornerLayer(image).Add(new ResizingAdorner(image));
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            var layer = AdornerLayer.GetAdornerLayer(image);
            var toRemove = layer.GetAdorners(image);

            if (toRemove != null)
            {
                foreach (var item in toRemove)
                {
                    layer.Remove(item);
                }
            }
        }
    }
}
