using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CRM.Model;
using DataLayer;
using System.Windows;

namespace Prism_Library.Provider
{	

	public class CopyesProvider
	{
		

		internal static void Add(Book book)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					CopyesBook copyes = new CopyesBook
					{
						BookId = book.BookId,
						Name = book.Name,
						Inventory = book.InventoryNum
					};

					db.CopyesBooks.Add(copyes);

					db.SaveChanges();

				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}

		internal static void Deleted(Book selectedItem)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					var copyToRemove = db.CopyesBooks.FirstOrDefault(x => x.BookId == selectedItem.BookId);

					if (copyToRemove != null)
						db.CopyesBooks.Remove(copyToRemove);

					db.SaveChanges();
				}
				catch (Exception e)
				{
					throw e;
				}
				
			}
		}


		internal static ObservableCollection<CopyesBook> ListFind(string _findName)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					//var list = db.CopyesBooks;
					//join s in db.Subscriptions on copy.CopyId equals s.CopyId

					//запит на книги які видані
					var sub = from s in db.Subscriptions
							  where s.ToAcceptDate == null 
							  select s;

					//запит на збіги виданих книг по id в таблиці екземплярів
					var query = from copy in db.CopyesBooks
								from s in sub
								where copy.Name.StartsWith(_findName) && copy.CopyId != s.CopyId
								select copy;
						
					var copyes = new ObservableCollection<CopyesBook>(query);

					return copyes;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}

			return null;
		}
		internal static ObservableCollection<CopyesBook> List()
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				//запит "В наявності" 
				try
				{
					var list = DatabaseConnection.List<CopyesBook>();

					//запит з двох таблиць from..from
					//пошук в таблиці Subscriptions на збіги CopyId
					//тоді книга видана і в списку доступних не появляється
					//var linq = from c in db.CopyesBooks
					//		   join s in db.Subscriptions on c.CopyId equals s.CopyId
					//		   where c.CopyId == s.CopyId && s.ToAcceptDate != null
					//		   select c;

					//var query = from s in db.Subscriptions
					//			join c in db.CopyesBooks on s.CopyId equals c.CopyId into copyG
					//			from x in copyG
					//			where s.ToAcceptDate == null
					//			select new
					//			{
					//				Id = x.CopyId
					//			};


					ObservableCollection<CopyesBook> CopyessList = new ObservableCollection<CopyesBook>(list);

					//var linq = (from s in db.Subscriptions
					//			from x in db.CopyesBooks
					//			where s.ToAcceptDate == null
					//			select x).Distinct();


					var bb = from s in db.Subscriptions
							 where s.ToAcceptDate == null
							 select s;

					
					foreach (var item in list)
					{
						foreach (var i in bb)
						{
							if (item.CopyId == i.CopyId)
							{
								CopyessList.Remove(item);
							}
	
						}

						
					}

					//var linq = from c in db.CopyesBooks
					//			from s in db.Subscriptions
					//			where c.CopyId != s.CopyId
					//			where s.ToAcceptDate != null
					//			select c;

							 //var linq = from s in db.Subscriptions
							 //		   group s by s.ToAcceptDate == null into sGroup
							 //		   from c in db.CopyesBooks
							 //		   from sg in sGroup
							 //		   where c.CopyId != sg.CopyId 
							 //		   //join s in db.Subscriptions on copy.CopyId equals s.CopyId
							 //		   //join c in db.CopyesBooks on sub.CopyId equals c.CopyId
							 //		   select c;

							 //var list = new ObservableCollection<CopyesBook>();



							 //list = new ObservableCollection<CopyesBook>();


					

					if ((CopyessList == null) || (CopyessList.Count == 0))
						return DatabaseConnection.List<CopyesBook>();
					else
						return CopyessList;

				}
				catch (Exception e)
				{
					throw e;
				}
				
			}
		}
	}
}
