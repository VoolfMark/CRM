using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
	public class Genre: IComparable<Genre>
	{
		public Genre()
		{
			this.Books = new HashSet<Book>();
		}

		[Key]
		public int GenreId { get; set; }

		[Required]
		[MaxLength(30)]
		public string Name { get; set; }


		public virtual ICollection<Book> Books { get; set; }

		public int CompareTo(Genre other)
		{
			var compare1 = Name ?? string.Empty;
			var compare2 = other.Name ?? string.Empty;
			return compare1.CompareTo(compare2);
		}
	}
}
