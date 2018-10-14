
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

            switch(category)
            {
                case MenuCategories.Format:
                    return new FormattingSubmenu();
                case MenuCategories.Insert:
                    return null;
                case MenuCategories.Options:
                    return null;
            }


            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
