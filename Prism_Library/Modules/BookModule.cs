using Prism.Ioc;
using Prism.Modularity;
using Prism_Library.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.Modules
{
	public class BookModule : IModule
	{



		public void OnInitialized(IContainerProvider containerProvider)
		{

		}

		public void RegisterTypes(IContainerRegistry containerRegistry)
		{
			containerRegistry.RegisterForNavigation<BookView>();
			
		}
	}
}
