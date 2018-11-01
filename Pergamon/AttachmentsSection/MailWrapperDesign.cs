
namespace Pergamon
{
    public class MailWrapperDesign : MailWrapperViewModel
    {
        public static MailWrapperDesign Instance => new MailWrapperDesign();

        public MailWrapperDesign()
        {
            Address = "address@mail.com";
            FirstLetter = "A";
        }
    }
}
