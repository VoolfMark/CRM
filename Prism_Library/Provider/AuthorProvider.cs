using CRM.Model;
using DataLayer;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prism_Library.Provider
{
	public class AuthorProvider
	{
		internal static bool Add(string name, string surname)
		{			
			Author author = new Author
			{
				Name = name,
				Surname = surname
			};
						
			return DatabaseConnection.Add<Author>(author);
		}

		internal static void Delete(Author selectedAuthor)
		{

			try
			{
				DatabaseConnection.Remove<Author>(a => a.AuthorId == selectedAuthor.AuthorId);

			}
			catch (Exception e)
			{
				MessageBox.Show("Cannot be deleted from database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
				
			
		}		

		internal static ObservableCollection<Author> List()
		{
			var list = DatabaseConnection.List<Author>();
			//list.Sort();
			

			return list;
		}

		internal static ObservableCollection<Author> FindList(string find)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					var authorList = db.Authors;

					var query = authorList.Where(a => a.Surname.StartsWith(find));

					ObservableCollection<Author> list = new ObservableCollection<Author>(query);


					return list;
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
