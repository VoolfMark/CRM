using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
	public class Password
	{
		[Key]
		public int PasswordID { get; set; }

		[Required]
		[MaxLength(30)]
		public string Login { get; set; }

		[Required]
		[MaxLength(30)]
		public string Pass { get; set; }

		[Required]
		[MaxLength(30)]
		public string Name { get; set; }

		[Required]
		[MaxLength(30)]
		public string Surname { get; set; }
	}
}
