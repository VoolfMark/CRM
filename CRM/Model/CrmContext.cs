using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Model
{
	public class CrmContext: DbContext
	{
		private static MappingSource _mappingSource = new AttributeMappingSource();

		//public CrmContext() 
		//	: base("CRMConnection")
		//{

		//}

		public CrmContext() : base() { }
		public CrmContext(string connectionstring) : base(connectionstring) { }
		public CrmContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }


		//Абонемент
		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<Reader> Readers { get; set; }
		public DbSet<Genre> Genres { get; set; }
		//видавницство
		public DbSet<CopyesBook> CopyesBooks { get; set; }
		public DbSet<Publishing> Publishings { get; set; }			
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Password> Passwords { get; set; }
	}
}
