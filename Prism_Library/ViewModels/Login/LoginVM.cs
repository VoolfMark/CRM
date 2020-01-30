using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;


using Prism_Library.Views.Login;
//using Prism_Library.Model;
using CRM.Model;
using Prism_Library.Model.Command;
using Prism_Library.Model.ViewModelBase;
using Prism_Library.Layer;
using Prism_Library.Views.ConnectionView;

namespace Prism_Library.ViewModels.Login
{
	class LoginVM: BindableBase
	{
		private string _login;
		private string _password;
		//CrmContext _context;

		public Window LoginWindow { get; set; }

		public string Login
		{
			get
			{
				if (_login == null)
					_login = "";
				return _login;
			}
			set
			{
				SetProperty(ref _login, value);
			}
		}

		public string Password
		{
			get
			{
				if (_password == null) _password = "";
				return _password;
			}
			set { SetProperty(ref _password, value); }
		}



		private ICommand _click_LoginCommand;
		private ICommand _click_RegCommand;
		private ICommand _click_ConnectionCommand;

		public ICommand Click_ConnectionCommand
		{
			get
			{
				if (_click_ConnectionCommand == null)
					_click_ConnectionCommand = new RelayCommand(new Action<object>(Connection_Method));

				return _click_ConnectionCommand;
			}
			set => SetProperty(ref _click_ConnectionCommand, value);
		}

		private void Connection_Method(object obj)
		{
			ConnectionWindowView connectionWindow = new ConnectionWindowView();
			connectionWindow.ShowDialog();
		}

		public ICommand Click_LoginCommand
		{
			get
			{
				if (_click_LoginCommand == null)
					_click_LoginCommand = new RelayCommand(new Action<object>(Login_Method));

				return _click_LoginCommand;
			}
			set => SetProperty(ref _click_LoginCommand, value);
		}
		public ICommand Click_RegCommand
		{
			get
			{
				if (_click_RegCommand == null)
					_click_RegCommand = new RelayCommand(new Action<object>(Reg_Method));
				return _click_RegCommand;
			}

			set => SetProperty(ref _click_RegCommand, value);
		}

		private void Reg_Method(object obj)
		{
			try
			{
				RegisterView RV = new RegisterView();
				RV.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Program initialization error.\nCheck RedLog.txt for more information.\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				
				return;
			}
		}

		private void Login_Method(object p)
		{
			PasswordBox pwBox = (PasswordBox)p;
			Password = pwBox.Password;

			//if (_context == null)
			//	_context = new CrmContext();

			try
			{
				Loginner.Login(Login, Password);
				LoginWindow.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("Login error", e.Message);
			}			

			//if(linq != null)
			//	MessageBox.Show("Password & Login is correct");
			//else
			//	MessageBox.Show("Password & Login is not correct", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

			//foreach (var item in linq)
			//{
			//	MessageBox.Show("Password & Login is correct");
			//	LoginWindow.Close();
			//}
		}	   		 
		
	}
}
