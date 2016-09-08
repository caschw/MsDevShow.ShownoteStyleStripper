using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using MsDevShow.ShownoteStyleStripper.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MsDevShow.ShownoteStyleStripper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Simplify_OnClick(object sender, RoutedEventArgs e)
        {
            var inputText = htmlText.Text.RemoveCruft();

            htmlText.Text = inputText;
            htmlText.Select(0, inputText.Length);

            var content = new DataPackage();
            content.SetText(inputText);
            Clipboard.SetContent(content);
        }

        private void HtmlText_OnDragEnter(object sender, DragEventArgs e)
        {
             e.AcceptedOperation=DataPackageOperation.Copy;
            //e.Data.RequestedOperation = DataPackageOperation.Copy;
        }

        private void HtmlText_OnDrop(object sender, DragEventArgs e)
        {
            var t = e;
        }
    }
}
