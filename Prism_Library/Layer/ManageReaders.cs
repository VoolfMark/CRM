using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CRM.Model;
using Prism_Library.Provider;

namespace Prism_Library.Layer
{
	class ManageReaders
	{	
		internal static void AddReader(Reader newItems)
		{
			if (newItems != null)
				if(!ReaderProvider.Add(newItems))
					MessageBox.Show("Wrong new reader.\n", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		internal static void DeleteReader(Reader selectedReader)
		{
			if (selectedReader != null)
				ReaderProvider.Delete(selectedReader);
			else
				MessageBox.Show("Wrong reader.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		internal static void ReplaceReader(Reader toEditing)
		{

			if (toEditing != null)
				ReaderProvider.Replace(toEditing);
			else
				MessageBox.Show("Wrong reader.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		internal static ObservableCollection<Reader> List()
		{
			return ReaderProvider.List();
		}

		internal static ObservableCollection<Reader> ListFind(string findReaders)
		{
			return ReaderProvider.ListFind(findReaders);
		}
	}
}
