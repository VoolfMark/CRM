using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Prism_Library.ViewModels;
using Prism.Mvvm;

namespace Prism_Library.Model
{
	public class Connector: BindableBase
	{		
		#region Connector Readers
		#region TestFindReader
		public static List<string> GetName_Readers()
		{
			using (var db = new LibraryEntities())
			{
				List<string> ts = new List<string>();

				var reader = db.Reader.OrderBy(r => r.Name).Select(r => r.Name);

				foreach (var item in reader)
				{
					ts.Add(item);
				}


				return ts;
			}
		}
		internal static int FindReader(string p)
		{
			using (var db = new LibraryEntities())
			{
				var r = db.Reader.FirstOrDefault(x => x.Name == p);

				if (r != null)
				{
					var index = r.IDReader;
					return index;
				}
			}

			return 0;
		}
		#endregion

		public static bool TryEditReader(Reader reader, ReadersViewModel RV)
		{
			using (var db = new LibraryEntities())
			{
				var r = db.Reader.Find(reader.IDReader);

				
				return false;				
			}
		}
		public static ObservableCollection<Reader> Readers
		{
			get
			{
				using(var db = new LibraryEntities())
				{
					var readers = new ObservableCollection<Reader>(db.Reader);
					readers = new ObservableCollection<Reader>(readers.OrderBy(i => i.Name));
					return readers;
				}
			}
		}	

		public static void AddReader(Reader reader)
		{
			using (var db = new LibraryEntities())
			{
				if(reader != null)
				{
					db.Reader.Add(reader);
					db.SaveChanges();
				}
			}
		}

		public static void DeleteReader(Reader reader)
		{
			using (var db = new LibraryEntities())
			{
				if(reader != null)
				{
					var readerToRemove = db.Reader.FirstOrDefault(x => x.IDReader == reader.IDReader);

					if(readerToRemove != null)
					{
						db.Reader.Remove(readerToRemove);
						db.SaveChanges();
					}
				}
			}

		}

		#endregion
			   
		#region Connector Book

		public static ObservableCollection<Status> Statuses
		{
			get
			{
				using (var db = new LibraryEntities())
				{
					var _status = new ObservableCollection<Status>(db.Status);

					return _status;
				}
			}
		}
		public static ObservableCollection<Author> Authors
		{
			get
			{
				using(var db = new LibraryEntities())
				{
					var _author = new ObservableCollection<Author>(db.Author);
					_author = new ObservableCollection<Author>(_author.OrderBy(i => i.Surname));
					return _author;
				}
			}
			
		}

		public static ObservableCollection<AnInstance> AnInstances
		{
			get
			{
				using(var db = new LibraryEntities())
				{
					var _instance = new ObservableCollection<AnInstance>(db.AnInstance);
					return _instance;
				}
			}
		}
		public static ObservableCollection<Publishing_House> Publishings
		{
			get
			{
				using (var db = new LibraryEntities())
				{
					var _publishing = new ObservableCollection<Publishing_House>(db.Publishing_House);
					_publishing = new ObservableCollection<Publishing_House>(_publishing.OrderBy(i => i.PublishingHouse));
					return _publishing;
				}
			}
		}
		public static ObservableCollection<Genre> Genres
		{
			get
			{
				using (var db = new LibraryEntities())
				{
					var _genre = new ObservableCollection<Genre>(db.Genre);
					_genre = new ObservableCollection<Genre>(_genre.OrderBy(i => i.Genre1));
					return _genre;
				}
			}
		}
		public static ObservableCollection<Invoice> Invoices
		{
			get
			{
				using (var db = new LibraryEntities())
				{
					var _invoice = new ObservableCollection<Invoice>(db.Invoice);
					_invoice = new ObservableCollection<Invoice>(_invoice.OrderBy(i => i.Data));
					return _invoice;
				}
			}
		}
		public static ObservableCollection<Book> Books
		{
			get
			{
				using (var db = new LibraryEntities())
				{
					var _book = new ObservableCollection<Book>(db.Book);
					_book = new ObservableCollection<Book>(_book.OrderBy(i => i.Name));
					return _book;
				}
			}
		}

		#endregion

	}
}
