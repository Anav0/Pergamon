using Ninject;
using Nuntium.Core;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pergamon
{
    /// <summary>
    /// Interaction logic for AddressInputBox.xaml
    /// </summary>
    public partial class AddressInputBox : UserControl
    {
        public AddressInputBox()
        {
            Addresses = new ObservableCollection<MailWrapperViewModel>();
            InitializeComponent();
        }

        #region Purpose DP

        public AddressCategory Purpose
        {
            get { return (AddressCategory)GetValue(PurposeProperty); }
            set { SetValue(PurposeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Purpose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PurposeProperty =
            DependencyProperty.Register("Purpose", typeof(AddressCategory), typeof(AddressInputBox), new PropertyMetadata());

        #endregion

        #region Address DP

        public string  Address
        {
            get { return (string )GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Address.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string ), typeof(AddressInputBox), new PropertyMetadata(""));

        #endregion

        #region Addresses DP

        public ObservableCollection<MailWrapperViewModel> Addresses
        {
            get { return (ObservableCollection<MailWrapperViewModel>)this.GetValue(AddressesProperty); }
            set { this.SetValue(AddressesProperty, value); }
        }

        public static readonly DependencyProperty AddressesProperty =
            DependencyProperty.Register("Addresses",
            typeof(ObservableCollection<MailWrapperViewModel>), typeof(AddressInputBox),
            new FrameworkPropertyMetadata());

        #endregion

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is TextBox box))
                return;

            if (string.IsNullOrEmpty(box.Text))
                return;

            var lastChar = box.Text[box.Text.Length - 1];

            if(lastChar == ';')
            {

                if (!(new EmailAddressAttribute().IsValid(box.Text)))
                    return;

                var adr = new Address
                {
                    EmailAddress = box.Text.Remove(box.Text.Length - 1, 1),
                    EmailCategory = Purpose,
                };

                IoC.Kernel.Get<AddressSectionViewModel>().Addresses.Add(adr);

                var wrapper = new MailWrapperViewModel
                {
                    Address = box.Text.Remove(box.Text.Length-1, 1),
                    CornerRadius = 10,
                    FirstLetter = box.Text[0].ToString().ToUpper(),
                };

                wrapper.OnDeleteButtonClick += ((s, args) =>
                {
                    Addresses.Remove(wrapper);
                    IoC.Kernel.Get<AddressSectionViewModel>().Addresses.Remove(adr);
                });

                Addresses.Add(wrapper);
                box.Text = "";
            }

        }
    }
}
