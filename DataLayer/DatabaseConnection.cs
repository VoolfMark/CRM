using CRM;
using CRM.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace DataLayer
{
	/// <summary>
	/// Створює та містить рядок з'єднання з файлом mdf.
	/// Використовує Entity Framework для підключення до MS SQL LocalDB.
	/// Можна створити файл бази даних, якщо його не існує.
	/// Автоматично переміщує базу даних до останньої версії після підключення до неї.
	/// Містить шаблон маніпуляції з базою даних "linq to sql", який можна використовувати в класах постачальника даних.
	/// </summary>
	public class DatabaseConnection
	{
		private static string _server;
		private static string _directory;
		private static string _dbName;
		private static string _connectionString;
		private static string _directoryExcel;


		public static string ConnectionString { get => _connectionString; }
		public static string Directory { get => _directory; }
		public static string DbName { get =>_dbName; }
		public static string DirectoryExcel
		{
			get => (_directoryExcel == null) ? 
				_directoryExcel = AppDomain.CurrentDomain.BaseDirectory :
				_directoryExcel;
			set => _directoryExcel = value;
		}



		/// <summary>
		/// Статичний конструктор встановлює значення за замовчуванням для приватних змінних.
		/// </summary>
		static DatabaseConnection()
		{
			_server = "(LocalDB)\\MSSQLLocalDB";
			_directory = "";
			_dbName = "";
			_connectionString = "Data Source=" + _server + ";AttachDbFilename=\"" + Directory + DbName + ".mdf\";Integrated Security=True;";
			
		}





		#region Give Book
		public static string ReturnPublishingName(Book selectedItem)
		{
			using (var db = new CrmContext(_connectionString))
			{
				try
				{
					int i = selectedItem.PublishingId;

					var p = db.Publishings.FirstOrDefault(x => x.PublishingId == i);

					return p.Name;
				}
				catch (Exception e)
				{
					throw new NullReferenceException(e.Message);
				}
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="selectedItem"></param>
		/// <returns></returns>
		public static string ReturnNameAuthor(Book selectedItem)
		{
			using (var db = new CrmContext(_connectionString))
			{
				try
				{
					int i = selectedItem.AuthorId;

					var p = db.Authors.FirstOrDefault(x => x.AuthorId == i);

					return p.NS;
				}
				catch (Exception e)
				{
					throw new NullReferenceException(e.Message);
				}
			}			
		}
		public static string ReturnGenreName(Book selectedItem)
		{
			using (var db = new CrmContext(_connectionString))
			{
				try
				{
					int i = selectedItem.GenreId;

					var p = db.Genres.FirstOrDefault(x => x.GenreId == i);

					return p.Name;
				}
				catch (Exception e)
				{
					throw new NullReferenceException(e.Message);
				}
			}
		}
		#endregion


		/// <summary>
		/// Перевіряє, чи можна отримати базу даних за допомогою рядка з'єднання.
		/// </summary>
		/// <returns></returns>
		public static bool Test()
		{
			bool result = false;
			using (var db = new CrmContext(_connectionString))
			{
				result = db.Database.Exists();
			}
			return result;
		}

		/// <summary>
		/// Встановлює новий рядок з'єднання, якщо файл бази даних існує.
		/// Повертає помилку, якщо файл бази даних не існує.
		/// </summary>
		/// <param name="directory"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static bool InitializeConnection(string directory, string dbName)
		{
			bool result = File.Exists(directory + dbName + ".mdf");

			if(result)
			{
				_directory = directory;
				_dbName = dbName;
				_connectionString = "Data Source=" + _server + ";AttachDbFilename=\"" + Directory + DbName + ".mdf\";Integrated Security=True;";

				Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrmContext, CRM.Migrations.Configuration>(true));

				using (var db = new CrmContext(_connectionString))
				{
					result = db.Database.Exists();
				}
			}
			return result;
		}

		/// <summary>
		/// Створює файл бази даних, якщо він не існує.
		/// </summary>
		/// <param name="directory"></param>
		/// <param name="dbName"></param>
		/// <returns></returns>
		public static bool CreateDatabase(string directory, string dbName)
		{
			bool result = false;
			string connectionString = "Data Source=" + _server + ";AttachDbFilename=\"" + directory + dbName + ".mdf\";Integrated Security=True;";

			using (var db = new CrmContext(connectionString))
			{
				try
				{
					db.Database.CreateIfNotExists();
					result = true;
				}
				catch (Exception ex)
				{
					throw new Exception(string.Format("Create database error\nFolder: {0}\nDatabase: {1}", directory, dbName), ex);
				}
			}

			if (result)
				InitializeConnection(directory, dbName);

			return result;
		}

		/// <summary>
		/// Перераховує кожен запис із таблиці, де умова повертається як істинна
		/// </summary>
		/// <typeparam name="Entity">Тип таблиці в базі даних</typeparam>
		/// <param name="condition">Умова на записах таблиці.</param>
		/// <returns></returns>
		//public static List<Entity> ListTable<Entity>(Expression<Func<Entity, bool>> condition) where Entity : class
		//{
		//	List<Entity> list = new List<Entity>();
			
		//	using (var db = new CrmContext(_connectionString))
		//	{
		//		DbSet<Entity> EntityTable = db.Set<Entity>();
		//		var query = EntityTable.Where(condition);
		//		list.AddRange(query);
		//	}
		//	return list;
		//}

		public static ObservableCollection<Entity> List<Entity>() where Entity : class
		{
			ObservableCollection<Entity> list = new ObservableCollection<Entity>();

			using (var db = new CrmContext(_connectionString))
			{
				

				var q = db.Set<Entity>();


				list = new ObservableCollection<Entity>(q);

				return list;
			}
			
		}		

		/// <summary>
		/// Перевіряє, чи є принаймні один запис у таблиці бази даних, де умова повертається як істинна.
		/// </summary>
		/// <typeparam name="Entity">Тип таблиці в базі даних</typeparam>
		/// <param name="condition">Умова на записах таблиці. Напр.(p => p.Id == record.Id)</param>
		/// <returns></returns>
		public static bool IsExist<Entity>(Expression<Func<Entity, bool>> condition) where Entity : class
		{
			bool exists = false;
			using (var db = new CrmContext(_connectionString))
			{
				DbSet<Entity> EntityTable = db.Set<Entity>();
				var query = EntityTable.Where(condition);
				exists = (query.Count() > 0);
			}
			return exists;
		}
		/// <summary>
		/// Додає запис у таблицю бази даних.
		/// </summary>
		/// <typeparam name="Entity">Тип таблиці в базі даних</typeparam>
		/// <param name="record">Додає запис у таблицю бази даних.</param>
		/// <returns></returns>
		public static bool Add<Entity>(Entity record) where Entity : class
		{
			bool result = false;

			using (var db = new CrmContext(_connectionString))
			{
				using (var dbTransaction = db.Database.BeginTransaction())
				{
					try
					{
						
						
						DbSet<Entity> EntityTable = db.Set<Entity>();
						EntityTable.Add(record);

						db.SaveChanges();
						dbTransaction.Commit();
						result = true;
					}
					catch (Exception)
					{
						dbTransaction.Rollback();						
					}					
				}
			}
			return result;
		}

		/// <summary>
		/// Змінює запис у таблиці бази даних
		/// </summary>
		/// <typeparam name="Entity">Тип таблиці в базі даних</typeparam>
		/// <param name="record">Запис з оновленими значеннями.</param>
		/// <param name="condition">Умова в таблиці бази даних, яка вибирає запис. Напр. (p => p.Id == record.Id) </param>
		/// <returns></returns>
		public static bool Modify<Entity>(Entity record, Expression<Func<Entity, bool>> condition) where Entity : class
		{
			bool result = false;
			bool exists = false;
			using (var db = new CrmContext(_connectionString))
			{
				using (var dbTransaction = db.Database.BeginTransaction())
				{
					try
					{
						

						DbSet<Entity> EntityTable = db.Set<Entity>();
						var query = EntityTable.Where(condition);
						foreach (var rec in query)
						{
							exists = true;

							PropertyInfo[] properties = EntityCloner.GetProperties(typeof(Entity));
							foreach (PropertyInfo property in properties)
							{
								property.SetValue(rec, property.GetValue(record));
							}
						}
						if (exists)
						{
							db.SaveChanges();
							dbTransaction.Commit();
							result = true;
						}
					}
					catch (Exception ex)
					{
						dbTransaction.Rollback();						
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Видаляє кожен запис із таблиці бази даних, де умова повертається як істинна.
		/// </summary>
		/// <typeparam name="Entity">Тип таблиці в базі даних</typeparam>
		/// <param name="condition">Умова в таблиці бази даних, яка вибирає запис. Напр. (p => p.Id == Id) </param>
		/// <returns></returns>				
		//public void Delete<TEntity>(TEntity entity)	where TEntity : class
		//{
		//	// Настройки контекста
		//	CrmContext context = new CrmContext();
		//	context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));

		//	context.Entry<TEntity>(entity).State = EntityState.Deleted;
		//	context.SaveChanges();
		//}

		public static void Remove<Entity>(Expression<Func<Entity, bool>> selected) where Entity : class
		{
			using (var db = new CrmContext(_connectionString))
			{
				using (var dbTransaction = db.Database.BeginTransaction())
				{
					try
					{
						

						bool exists = false;
						DbSet<Entity> Table = db.Set<Entity>();

						var query = Table.Where(selected);



						foreach (var rec in query)
						{
							exists = true;
							Table.Remove(rec);
						}



						if (exists)
						{
							db.SaveChanges();
							dbTransaction.Commit();							
						}
					}
					catch (Exception)
					{
						dbTransaction.Rollback();
					}
				}
			}
		}
	}
}
