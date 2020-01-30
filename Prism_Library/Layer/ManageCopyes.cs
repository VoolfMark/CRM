using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CRM.Model;
using Prism_Library.Provider;

namespace Prism_Library.Layer
{
	public class ManageCopyes
	{
		internal static void AddCopyes(Book book)
		{
			try
			{
				if (book != null)
					CopyesProvider.Add(book);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error");
			}			
		}

		internal static void DeleteCopyes(Book selectedItem)
		{
			try
			{
				if (selectedItem != null)
					CopyesProvider.Deleted(selectedItem);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error");
			}
			
		}

		internal static ObservableCollection<CopyesBook> List()
		{
			return CopyesProvider.List();
		}

		internal static ObservableCollection<CopyesBook> ListFind(string _findName)
		{
			return CopyesProvider.ListFind(_findName);
		}
	}
}