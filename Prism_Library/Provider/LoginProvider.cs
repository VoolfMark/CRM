using CRM.Model;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prism_Library.Provider
{
	public class LoginProvider
	{
		internal static bool IsValidLogin(string login)
		{
			bool resault = false;

			using(var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				var q = from l in db.Passwords
						where l.Login == login
						select l;

				resault = (0 < q.Count());
			}

			return resault;
		}

		internal static bool IsValidPassword(string login, string password)
		{
			bool resault = false;


			using (var _context = new CrmContext(DatabaseConnection.ConnectionString))
			{
				var linq = from b in _context.Passwords
						   where b.Login == login && b.Pass == password
						   select b;

				foreach (var item in linq)
				{
					if (item.Pass == password)
						resault = true;
				}
			}

			return resault;
		}
	
		internal static bool NewUser(string log, string pass, string name, string surname)
		{
			
			Password _password = new Password()
			{
				Login = log,
				Pass = pass,
				Name = name,
				Surname = surname
			};

			{
				//using(var _context = new CrmContext())
				//{
				//	try
				//	{
				//		var temp = _context.Passwords.FirstOrDefault(x => x.Login == _password.Login);

				//		if (temp.Login != _password.Login)
				//		{
				//			_context.Passwords.Add(_password);
				//			_context.SaveChanges();

				//			result = true;
				//		}
				//		else
				//			MessageBox.Show("A user with this login already exists.");
				//	}
				//	catch (Exception e)
				//	{
				//		MessageBox.Show("Cannot add a record to the database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				//	}


				//}
			}

			return DatabaseConnection.Add<Password>(_password);
		}
	}
}
