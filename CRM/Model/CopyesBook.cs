using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
	public class CopyesBook
	{
		public CopyesBook()
		{
			this.Subscriptions = new HashSet<Subscription>();
		}

		
		[Key]
		public int CopyId { get; set; }

		[ForeignKey(nameof(Book))]
		public int BookId { get; set; }

		public string Inventory { get; set; }
		public string Name { get; set; }
		
		public virtual Book Book { get; set; }

		public virtual ICollection<Subscription> Subscriptions { get; set; }
	
	}
}
