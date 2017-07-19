using Xamarin.Forms;

namespace examenPrutech
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			NavigationPage np = new NavigationPage(new ListaEstaciones());
			np.Style = Resources["NavigationPage"] as Style;
			MainPage = np;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
