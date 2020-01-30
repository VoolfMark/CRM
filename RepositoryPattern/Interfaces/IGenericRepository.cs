using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern.Interfaces
{
	interface IGenericRepository<Entity> where Entity : class
	{
		IEnumerable<Entity> Get();

		Entity GetByID(object id);

		void Insert(Entity entity);
		void Delete(object id);
		void Delete(Entity entity);
		void Update(Entity entity);
		void Save();

	}
}
