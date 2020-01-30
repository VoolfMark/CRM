using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Prism_Library.Modules;
using Prism_Library.Views;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Prism_Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
	{
		
		protected override void OnStartup(StartupEventArgs e)
		{
			FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), 
																new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
			base.OnStartup(e);
		}

		void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox.Show("An unhandled exception occurred.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = false;
		}

		protected override Window CreateShell()
		{
			return Container.Resolve<GlobalWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{
			
		}

		protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
		{
			moduleCatalog.AddModule<ReaderModule>();
			moduleCatalog.AddModule<BookModule>();
			moduleCatalog.AddModule<GiveBookModule>();
			moduleCatalog.AddModule<AcceptBookModule>();
			
		}
		
	}
}
