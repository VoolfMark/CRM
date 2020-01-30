using CRM.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.Notification
{
	public class BookToCombine
	{
		public int BookId { get; set; }
		public int AuthorId { get; set; }
		public int PublishingId { get; set; }
		public int GenreId { get; set; }
		public string InventoryNum { get; set; }
		public string Name { get; set; }
		public int Year { get; set; }



	}
}
