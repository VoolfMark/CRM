using System;
using System.IO;

namespace Prism_Library.Layer
{
	/// <summary>
	/// Коннектор для БД
	/// </summary>	
	public class Connection
	{
		public static string Directory{	get => DataLayer.DatabaseConnection.Directory; }
		public static string DbName { get { return DataLayer.DatabaseConnection.DbName; } }



		/// <summary>
		/// Базобий каталог 
		/// </summary>		
		private static string _baseDir
		{
			get
			{
				string baseDir = AppDomain.CurrentDomain.BaseDirectory;

				if (!baseDir.EndsWith("\\"))
					baseDir += "\\";

				return baseDir;
			}
		}

		private static string _dbPropertyFile { get => _baseDir + "Property.txt"; }

		private static void ReadDbProperty(ref string directory, ref string dbname)
		{
			//Відкрити потік і прочитайти його
			using (StreamReader sr = new StreamReader(_dbPropertyFile))
			{
				string line = "";
				if ((line = sr.ReadLine()) != null)
				{
					directory = line;
				}
				if ((line = sr.ReadLine()) != null)
				{
					dbname = line;
				}
			}
		}

		private static void WriteDbProperty(string directory, string dbname)
		{
			//Видаліть файл, якщо він існує.
			if (File.Exists(_dbPropertyFile))
			{
				File.Delete(_dbPropertyFile);
			}

			//Створіти файл.
			using (StreamWriter sw = new StreamWriter(_dbPropertyFile))
			{
				sw.WriteLine(directory);
				sw.WriteLine(dbname);
			}
		}


		public static bool TestConnection()
		{
			if (string.IsNullOrWhiteSpace(Directory) || string.IsNullOrWhiteSpace(DbName))
			{
				string dirName = _baseDir;
				string dbName = "LibraryDB";

				if (File.Exists(_dbPropertyFile))
					ReadDbProperty(ref dirName, ref dbName);

				ChangeDatabase(dirName, dbName);
			}
			return DataLayer.DatabaseConnection.Test();
		}

		public static bool ChangeDatabase(string directory, string database)
		{
			if (DataLayer.DatabaseConnection.InitializeConnection(directory, database))
			{
				WriteDbProperty(directory, database);
				return true;
			}
			return false;
		}

		public static bool CreateDatabase(string directory, string database)
		{
			if (DataLayer.DatabaseConnection.CreateDatabase(directory, database))
			{
				WriteDbProperty(directory, database);
				return true;
			}
			return false;
		}
	}
}
