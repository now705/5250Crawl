
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Crawl.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManagePage : TabbedPage
    {
		public ManagePage ()
		{
			InitializeComponent ();
		}

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            ((NavigationPage)CurrentPage).PopToRootAsync();
        }
    }
}