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
	public class ManageBook
	{	
		internal static void DeleteBook(Book selectedBook)
		{
			if (selectedBook != null)
				BookProvider.Deleted(selectedBook);
			else
				throw new System.ArgumentException("Wrong book.");
		}

		internal static void AddBook(Book newItems)
		{
			if (newItems != null)
				BookProvider.Add(newItems);
			else
				throw new System.ArgumentException("Wrong new item of the book.");
		}

		internal static ObservableCollection<Book> List()
		{
			return BookProvider.List();
		}
	}
}
