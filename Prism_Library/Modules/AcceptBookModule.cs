using Prism.Ioc;
using Prism.Modularity;
using Prism_Library.Views.SubscriptionView;

namespace Prism_Library.Modules
{
	public class AcceptBookModule : IModule
	{
		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<AcceptBookView>();
		}
	}
}
