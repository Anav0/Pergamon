
using Ninject;
using Nuntium.Core;
using System;
using System.Globalization;

namespace Pergamon
{
    public class MenuCategoriesToControlConverter : BaseValueConverter<MenuCategoriesToControlConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is MenuCategories category))
                return null;

            switch (category)
            {
                case MenuCategories.Format:
                    {
                        var output = new FormattingSubmenu();
                        output.ActionTarget = IoC.Kernel.Get<CustomRichTextBox>();
                        output.DataContext = new FormattingSubmenuViewModel();
                        return output;
                    }

                case MenuCategories.Insert:
                    {
                        var output = new InsertSubmenu();
                        output.ActionTarget = IoC.Kernel.Get<CustomRichTextBox>();
                        output.DataContext = new InsertSubmenuViewModel();
                        return output;
                    }
                case MenuCategories.Options:
                    {
                        var output = new OptionsSubmenu();
                        output.ActionTarget = IoC.Kernel.Get<CustomRichTextBox>();
                        output.DataContext = new OptionsSubmenuViewModel();
                        return output;
                    }
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
