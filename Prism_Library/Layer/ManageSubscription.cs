using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Model;
using Prism_Library.Provider;

namespace Prism_Library.Layer
{
	class ManageSubscription
	{
		internal static void AddSubscription(Subscription subscription)
		{
			if (subscription != null)
				SubscriptionProvider.Add(subscription);
			else
				throw new System.ArgumentException("Wrong new item of the book.");
		}
	}
}
