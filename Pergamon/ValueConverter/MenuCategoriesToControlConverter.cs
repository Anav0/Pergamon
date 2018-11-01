
using System;
using System.Globalization;
using System.Windows.Controls;

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
                        output.DataContext = FormattingSubmenuViewModel.Instance;
                        output.ActionTarget = StaticReferences.editor;
                        return output;
                    }

                case MenuCategories.Insert:
                    {
                        var output = new InsertSubmenu();
                        output.DataContext = InsertSubmenuViewModel.Instance;
                        output.ActionTarget = StaticReferences.editor;
                        return output;
                    }
                case MenuCategories.Options:
                    {
                        var output = new OptionsSubmenu();
                        output.DataContext = OptionsSubmenuViewModel.Instance;
                        output.ActionTarget = StaticReferences.editor;
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
