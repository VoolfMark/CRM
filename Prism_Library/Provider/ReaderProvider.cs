

using CRM.Model;
using DataLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Prism_Library.Provider
{
	class ReaderProvider
	{
		internal static bool Add(Reader newItems)
		{
			return DatabaseConnection.Add<Reader>(newItems);
		}

		internal static void Delete(Reader selectedReader)
		{
			using (var db = new CrmContext())
			{
				try
				{
					DatabaseConnection.Remove<Reader>(x => x.ReaderId == selectedReader.ReaderId);
				}
				catch (Exception e)
				{
					MessageBox.Show("Cannot be deleted from database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}

		internal static void Replace(Reader toEditing)
		{			
			DatabaseConnection.Modify<Reader>(toEditing, r => r.ReaderId == toEditing.ReaderId);
		}

		internal static ObservableCollection<Reader> List()
		{
			return DatabaseConnection.List<Reader>();
		}

		internal static ObservableCollection<Reader> ListFind(string findReaders)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					var list = db.Readers;

					var query = list.Where(r => r.Surname.StartsWith(findReaders));

					var result = new ObservableCollection<Reader>(query);


					return result;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}

			return null;
		}
	}
}
