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
	class ManageGenre
	{		
		internal static void AddGenre(string newItems)
		{
			if (!string.IsNullOrWhiteSpace(newItems))
				GenreProvider.Add(newItems);
			else
				throw new System.ArgumentException("Wrong new genre.");
		}

		internal static void DeleteGenre(Genre selectedGenre)
		{
			if (selectedGenre != null)
				GenreProvider.Delete(selectedGenre);
			else
				throw new System.ArgumentException("Wrong copy of the book.");
		}

		internal static ObservableCollection<Genre> UpdateList()
		{
			return GenreProvider.List();
		}
	}
}
