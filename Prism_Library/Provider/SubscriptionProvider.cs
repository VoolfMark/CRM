using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CRM.Model;
using DataLayer;

namespace Prism_Library.Provider
{
	class SubscriptionProvider
	{
		internal static void Add(Subscription subscription)
		{
			try
			{
				DatabaseConnection.Add<Subscription>(subscription);
			}
			catch (Exception e)
			{
				MessageBox.Show("Cannot add a record to the database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
