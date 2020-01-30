using CRM.Model;
using RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RepositoryPattern.Repos
{
	public class GenericRepository<Entity>: IGenericRepository<Entity> where Entity : class
	{
		private CrmContext _context = null;
		private DbSet<Entity> dbSet = null;

		public GenericRepository()
		{
			_context = new CrmContext();
			dbSet = _context.Set<Entity>();
		}
		public GenericRepository(CrmContext context)
		{
			_context = context;
			dbSet = context.Set<Entity>();
		}

		public void Delete(object id)
		{
			Entity existing = dbSet.Find(id);
			dbSet.Remove(existing);
		}

		public void Delete(Entity entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Entity> Get()
		{
			return dbSet.ToList();
		}

		public Entity GetByID(object id)
		{
			return dbSet.Find(id);
		}

		public void Insert(Entity entity)
		{
			dbSet.Add(entity);
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Update(Entity entity)
		{
			dbSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}
