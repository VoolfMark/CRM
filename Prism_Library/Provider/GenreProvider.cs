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
	public class GenreProvider
	{
		internal static void Add(string newItems)
		{
			Genre genre = new Genre { Name = newItems };

			try
			{
				if(DatabaseConnection.Add<Genre>(genre))
					MessageBox.Show("Genre added.");
			}
			catch (Exception e)
			{
				MessageBox.Show("Cannot add a record to the database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

			}
			
		}

		internal static void Delete(Genre selectedGenre)
		{

			try
			{
				DatabaseConnection.Remove<Genre>(x => x.GenreId == selectedGenre.GenreId);
								
			}
			catch (Exception e)
			{
				MessageBox.Show("Cannot be deleted from database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			
		}

		internal static ObservableCollection<Genre> List()
		{
			var list = DatabaseConnection.List<Genre>();
			//list.Sort();

			return list;
		}
	}
}
