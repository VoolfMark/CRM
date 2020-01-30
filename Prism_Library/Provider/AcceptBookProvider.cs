using CRM.Model;
using DataLayer;
using Prism_Library.Model.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.Provider
{
	class AcceptBookProvider
	{
		internal static ObservableCollection<BookToAcceptClass> List(Reader selectedItem)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					if (selectedItem == null)
						return null;

					var list = new ObservableCollection<BookToAcceptClass>();

					var linq = from s in db.Subscriptions.ToList()
							   join c in db.CopyesBooks on s.CopyId equals c.CopyId
							   join b in db.Books on c.BookId equals b.BookId
							   join a in db.Authors on b.AuthorId equals a.AuthorId
							   where s.ToAcceptDate == null && s.ReaderId == selectedItem.ReaderId
							   select new
							   {
								   _SubID = s.SubscriptionId,
								   _Author = a.NS,
								   _Name = c.Name,
								   _Year = b.Year,
								   _Inventory = c.Inventory,
								   _DateOfIssue = s.DateOfIssue,
								   _OfferDate = s.OfferDate
							   };


					foreach (var item in linq)
					{
						list.Add(new BookToAcceptClass()
						{
							AuthorName = item._Author,
							Name = item._Name,
							InventoryNum = item._Inventory,
							Year = item._Year,
							DateOfIssue = item._DateOfIssue,
							OfferDate = item._OfferDate,
							SubId = item._SubID
						});
					}

					return list;

				}
				catch (Exception e)
				{
					System.Windows.MessageBox.Show(e.ToString());

				}

				return null;
			}
		}

		internal static void Accept(BookToAcceptClass selectedBook, DateTime? toAcceptDate)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					var sub = db.Subscriptions.Where(s => s.SubscriptionId == selectedBook.SubId).FirstOrDefault();

					sub.ToAcceptDate = toAcceptDate;

					db.SaveChanges();
				}
				catch (Exception e)
				{
					System.Windows.MessageBox.Show(e.ToString());
				}
			}
		}
	}
}
