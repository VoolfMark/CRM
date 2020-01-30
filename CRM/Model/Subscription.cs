using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Model
{
	/// <summary>
	/// Клас абонемента
	/// </summary>
	public class Subscription
	{
		[Key]
		public int SubscriptionId { get; set; }

		[ForeignKey(nameof(Reader))]
		public int ReaderId { get; set; }

		[ForeignKey(nameof(CopyesBook))]
		public int CopyId { get; set; }


		
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


		//To many
		public virtual Reader Reader { get; set; }
		
		public virtual CopyesBook CopyesBook { get; set; }



		
	}
}
