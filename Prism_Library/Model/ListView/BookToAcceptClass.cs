using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.Model.ListView
{
	public class BookToAcceptClass
	{
		public BookToAcceptClass()
		{

		}
		public BookToAcceptClass(Subscription s)
			:this()
		{

		}



		public int SubId { get; set; }
		public string AuthorName { get; set; }
		public string Name { get; set; }
		public int? Year { get; set; }
		public string InventoryNum { get; set; }

		/// <summary>
		/// дата видачі
		/// </summary>
		public DateTime? DateOfIssue { get; set; }


		/// <summary>
		/// запропонована дата повернення
		/// </summary>		
		public DateTime? OfferDate { get; set; }

		/// <summary>
		/// реальна дата повернення
		/// </summary>
		public DateTime? ToAcceptDate { get; set; }
	}
}
