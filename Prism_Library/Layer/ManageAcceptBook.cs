using Prism_Library.Model.ListView;
using Prism_Library.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.Layer
{
	class ManageAcceptBook
	{
		internal static ObservableCollection<BookToAcceptClass> ListBookToReader(CRM.Model.Reader selectedItem)
		{
			return AcceptBookProvider.List(selectedItem);
		}

		internal static void Accept(BookToAcceptClass selectedBook, DateTime? toAcceptDate)
		{
			AcceptBookProvider.Accept(selectedBook, toAcceptDate);
		}
	}
}
