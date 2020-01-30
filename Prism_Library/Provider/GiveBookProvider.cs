using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CRM.Model;
using DataLayer;

namespace Prism_Library.Provider
{
	class GiveBookProvider
	{
		public static string NumOrder
		{
			get
			{
				using (var db = new CrmContext(DatabaseConnection.ConnectionString))
				{
					try
					{						
						if (db.CopyesBooks.Any())
						{
							//int? last;

							var last = db.CopyesBooks.Max(x => x.CopyId);


							if (last != 0)
								return last.ToString();
						}
						else
							return "1";
					}
					catch (Exception e)
					{
						MessageBox.Show("Cannot read this database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					return "1";
				}
			}
		}		
		internal static string GenreName(Book selectedItem)
		{
			return DatabaseConnection.ReturnGenreName(selectedItem);
		}
		internal static string AuthorName(Book selectedItem)
		{
			return DatabaseConnection.ReturnNameAuthor(selectedItem);
		}
		internal static string PublishingName(Book selectedItem)
		{
			return DatabaseConnection.ReturnPublishingName(selectedItem);
		}
		internal static Book CopyToBook(CopyesBook entity)
		{
			try
			{
				Book book = goToBook(entity);

				return book;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private static Book goToBook(CopyesBook entity)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				Book book = db.Books.FirstOrDefault(x => x.BookId == entity.BookId);

				return book;
			}
		}
		//internal static object CopyToBook(CopyesBook entity)
		//{
		//	using (var db = new CrmContext(DatabaseConnection.ConnectionString))
		//	{
		//		try
		//		{


		//			return 
		//		}
		//		catch (Exception)
		//		{

		//			throw;
		//		}

		//	}
		//}
	}
}
