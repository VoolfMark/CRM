using CRM.Model;
using Prism.Mvvm;
using Prism_Library.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Prism_Library.Layer
{
	public class ManageAuthor
	{		
		internal static void AddAuthor(string name, string surname)
		{
			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
			{
				throw new System.ArgumentException("Not enough data.", "Name or Surname");
			}
			else 
			{
				if(AuthorProvider.Add(name, surname))
				{
					MessageBox.Show("Author added.");
					
				}
				
			}
			
		}

		internal static void DeleteAuthor(Author selectedAuthor)
		{
			if (selectedAuthor != null)
			{
				AuthorProvider.Delete(selectedAuthor);
			}
		}

		internal static ObservableCollection<Author> ListAuthor()
		{
			return AuthorProvider.List();
		}

		
	}
}
