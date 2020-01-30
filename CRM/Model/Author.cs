using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{

	public class Author : IComparable<Author>
	{
		string _name;
		string _surname;

		public Author()
		{
			this.Books = new HashSet<Book>();
		}

		[Key]
		public int AuthorId { get; set; }

		[Required]
		[MaxLength(30)]
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
			}
		}

		[Required]
		[MaxLength(30)]
		public string Surname
		{
			get => _surname;
			set
			{
				_surname = value;
			}
		}

		public virtual ICollection<Book> Books { get; set; }


		public string NS
		{
			get
			{
				return Name + " " + Surname;
 			}
		}

		public int CompareTo(Author a)
		{
			var compare1 = Surname ?? string.Empty;
			var compare2 = a.Surname ?? string.Empty;
			return compare1.CompareTo(compare2);
		}
		public override string ToString()
		{
			return Name + " " + Surname;
		}
	}
}
