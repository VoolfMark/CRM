using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
	//Клас книги
	public class Book: IComparable<Book>
	{
		public Book()
		{
			this.CopyesBooks = new HashSet<CopyesBook>();
		}

		[Key]
		public int BookId { get; set; }


		[ForeignKey(nameof(Author))]
		public int AuthorId { get; set; }


		[ForeignKey(nameof(Publishing))]
		public int PublishingId { get; set; }


		[ForeignKey(nameof(Genre))]
		public int GenreId { get; set; }

		[Required]
		public string InventoryNum { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		public int? Year { get; set; }
			   

		public virtual Author Author {get; set; }
		public virtual Genre Genre { get; set; }
		public virtual Publishing Publishing { get; set; }


		public virtual ICollection<CopyesBook> CopyesBooks { get; set; }


		public override string ToString()
		{
			return Name;
		}

		public int CompareTo(Book b)
		{
			var compare1 = Name ?? string.Empty;
			var compare2 = b.Name ?? string.Empty;
			return compare1.CompareTo(compare2);
		}
	}
}
