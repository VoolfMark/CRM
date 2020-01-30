using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Model;
using Prism_Library.Provider;

namespace Prism_Library.Layer
{
	public class ManagePublishing
	{	

		internal static void AddPublishing(string newItems)
		{
			if (newItems != null)
				PublishingProvider.Add(newItems);
			else
				throw new System.ArgumentException("Wrong new publishing.");
		}

		

		internal static void DeletePublishing(Publishing selectedPublishing)
		{
			if (selectedPublishing != null)
				PublishingProvider.Delete(selectedPublishing);
			else
				throw new System.ArgumentException("Wrong new publishing.");
		}

		internal static ObservableCollection<Publishing> List()
		{
			return PublishingProvider.List();
		}
	}
}
