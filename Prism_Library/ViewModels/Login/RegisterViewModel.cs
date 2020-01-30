using Prism.Mvvm;
using Prism_Library.Model.Command;
using System;
using System.Windows.Input;

using CRM.Model;
using System.Windows;
using Prism_Library.Layer;

namespace Prism_Library.ViewModels.Login
{
	public class RegisterViewModel: BindableBase
	{
		private string _title = "Registration";
		public string Title
		{
			get => _title;
			set { SetProperty(ref _title, value); }
		}

		public Window RegWindow { get; set; }

		public RegisterViewModel()
		{
			
		}

		#region Fields

		string _name;
		string _surname;
		string _pass;
		string _log;

		public string Name
		{
			get => (_name == null) ? _name = "" : _name; 
			set => SetProperty(ref _name, value);
		}
		public string Surname
		{
			get => (_surname == null) ? _surname = "" : _surname;
			set => SetProperty(ref _surname, value);
		}
		public string Pass
		{
			get => (_pass == null) ? _pass = "" : _pass;
			set => SetProperty(ref _pass, value);
		}
		public string Login
		{
			get => (_log == null) ? _log = "" : _log;
			set => SetProperty(ref _log, value);
		}

		#endregion


		#region Commands

		ICommand _click_RegCommand;

		public ICommand Click_RegCommand
		{
			get
			{
				if (_click_RegCommand == null)
					_click_RegCommand = new RelayCommand(new System.Action<object>(Reg_Method), new Predicate<object>(CanReg));
				return _click_RegCommand;
			}

			set => SetProperty(ref _click_RegCommand, value);
		}

		private bool CanReg(object obj)
		{
			if ((_name == "") ||
				(_surname == "") ||
				(_pass == "") ||
				(_log == ""))
				return false;

			return true;
		}

		private void Reg_Method(object obj)
		{	
			if(Loginner.AddUser(_log, _pass, _name, _surname))
			{
				MessageBox.Show("New user added!");
				Close();


				Name = "";
				Surname = "";
				Pass = "";
				Login = "";
			}				   			 			
			
		}

		private void Close()
		{
			RegWindow?.Close();
		}

		#endregion


	}



}
