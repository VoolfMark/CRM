using Prism.Ioc;
using Prism.Modularity;
using Prism_Library.Views;



namespace Prism_Library.Modules
{
	public class ReaderModule : IModule
	{ 
		public void OnInitialized(IContainerProvider containerProvider)
		{
			
		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{			
			containerRegistry.RegisterForNavigation<ReadersView>();
		}
	}
}
