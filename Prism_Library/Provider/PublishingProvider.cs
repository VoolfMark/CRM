using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CRM.Model;
using DataLayer;

namespace Prism_Library.Provider
{
	public class PublishingProvider
	{
		internal static bool Add(string newItems)
		{
			Publishing publishing = new Publishing { Name = newItems };


			return DatabaseConnection.Add<Publishing>(publishing);
		}

		internal static void Delete(Publishing selectedPublishing)
		{
			using (var db = new CrmContext())
			{
				try
				{
					DatabaseConnection.Remove<Publishing>(x => x.PublishingId == selectedPublishing.PublishingId);

				}
				catch (Exception e)
				{
					MessageBox.Show("Cannot be deleted from database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		internal static ObservableCollection<Publishing> List()
		{
			var list = DatabaseConnection.List<Publishing>();
			//list.Sort();

			return list;
		}
	}
}
