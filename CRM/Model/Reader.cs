using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
	public class Reader: IComparable<Reader>
	{
		public Reader()
		{
			this.Subscriptions = new HashSet<Subscription>();
		}

		[Key]
		public int ReaderId { get; set; }

		[Required]
		[MaxLength(30)]
		public string Name { get; set; }

		[Required]
		[MaxLength(30)]
		public string Surname { get; set; }

		public DateTime? DateOfBirth { get; set; }

		[Required]
		[MaxLength(30)]
		public string Adress { get; set; }

		[Required]
		[MaxLength(30)]
		public string Phone { get; set; }


		public virtual ICollection<Subscription> Subscriptions { get; set; }

		public int CompareTo(Reader other)
		{
			var a = Name ?? string.Empty;
			var b = other.Name ?? string.Empty;
			return a.CompareTo(b);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
