using Prism_Library.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.Layer
{
	public class Loginner
	{
		private static string _logined;
		public static string Logined
		{
			get
			{
				if (_logined == null) _logined = "";
				return _logined;
			}
		}

		public static bool IsValidLogin(string login)
		{
			return LoginProvider.IsValidLogin(login);
		}

		public static bool IsValidPassword(string login, string password)
		{
			return LoginProvider.IsValidPassword(login, password);
		}

		public static void Login(string login, string password)
		{
			if (string.IsNullOrWhiteSpace(login) || !IsValidLogin(login))
			{
				throw new System.ArgumentException("Wrong login.", "Login");
			}
			else if (string.IsNullOrWhiteSpace(password) || !IsValidPassword(login, password))
			{
				throw new System.ArgumentException("Wrong password.", "Password");
			}


			_logined = login;
		}

		internal static bool AddUser(string log, string pass, string name, string surname)
		{
			if (string.IsNullOrWhiteSpace(log))
			{
				throw new System.ArgumentException("Wrong login.", "login");
			}
			else if (string.IsNullOrWhiteSpace(pass))
			{
				throw new System.ArgumentException("Wrong password.", "password");
			}
			else if (string.IsNullOrWhiteSpace(name))
			{
				throw new System.ArgumentException("Wrong name.", "name");
			}
			else if (string.IsNullOrWhiteSpace(surname))
			{
				throw new System.ArgumentException("Wrong surname.", "surname");
			}

			return LoginProvider.NewUser(log, pass, name, surname);
		}
	}
}
