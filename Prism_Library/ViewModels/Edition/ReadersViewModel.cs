using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;


using Prism.Mvvm;
using Prism_Library.Layer;
using Prism_Library.Model.Command;

using CRM.Model;
using Prism_Library.Model.ListView;
using Prism.Services.Dialogs;

namespace Prism_Library.ViewModels
{
	public class ReadersViewModel : CommandToEntity<Reader>
	{
		private string _title = "Edit Readers";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		#region Find Readers

		private string _findReaders;
		public string FindReaders
		{
			get => _findReaders;
			set =>	SetProperty(ref _findReaders, value);			
		}


		ICommand _findCommand;
		public ICommand FindCommand
		{
			get => (_findCommand == null) ?
				_findCommand = new RelayCommand(new Action<object>(FindReaders_Method), new Predicate<object>(CanFind)) :
				_findCommand;

			set => SetProperty(ref _findCommand, value);
		}

		private bool CanFind(object obj)
		{
			if (!string.IsNullOrWhiteSpace(_findReaders))
				return true;

			return false;
		}

		private void FindReaders_Method(object obj)
		{
			List = ManageReaders.ListFind(_findReaders);
		}


		#endregion



		#region TextBox Property

		string _nameR;
		string _surnameR;		
		string _adressR;
		string _phoneR;
		DateTime? _birth;
		public string NameR
		{
			get => (_nameR == null) ? _nameR = "" : _nameR;
			set => SetProperty(ref _nameR, value);
		}
		public string SurnameR
		{
			get => (_surnameR == null) ? _surnameR = "" : _surnameR;
			set => SetProperty(ref _surnameR, value);
		}		
		public string AdressR
		{
			get => (_adressR == null) ? _adressR = "" : _adressR;
			set => SetProperty(ref _adressR, value.ToString());
		}
		public string PhoneR
		{
			get => (_phoneR == null) ? _phoneR = "" : _phoneR;
			set => SetProperty(ref _phoneR, value.ToString());
		}

		public DateTime? Birth
		{
			get => (_birth == null) ? DateTime.Now : _birth;
			set => SetProperty(ref _birth, value);
		}
		#endregion

		public ReadersViewModel()
		{
			List = ManageReaders.List();
		}
		
		
		ICommand _clearCommand;
		ICommand _saveCommand;
		Reader ToEdit;
		private Reader _selectedItem;
		

		public ICommand SaveCommand
		{
			get
			{
				if (_saveCommand == null)
					_saveCommand = new RelayCommand(new Action<object>(Save_Method), new Predicate<object>(CanExecute));
				return _saveCommand;
			}
			set => SetProperty(ref _saveCommand, value);
		}


		private void Save_Method(object obj)
		{
			ToEdit = new Reader
			{
				ReaderId = SelectedItem.ReaderId,
				Name = NameR,
				Surname = SurnameR,
				Phone = PhoneR,
				Adress = AdressR,
				DateOfBirth = Birth
			};

			Clear_Method(obj);
			ManageReaders.ReplaceReader(ToEdit);
			UpdateList(obj);
		}	
		public ICommand ClearCommand
		{
			get
			{
				if (_clearCommand == null)
					_clearCommand = new RelayCommand(new Action<object>(Clear_Method), new Predicate<object>(CanExecute));
				return _clearCommand;
			}
			set => SetProperty(ref _clearCommand, value);
		}

		public override Reader SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}
		
		protected bool CanExecute(object p)
		{
			if (string.IsNullOrWhiteSpace(_nameR) ||
				string.IsNullOrWhiteSpace(_surnameR) ||
				string.IsNullOrWhiteSpace(_phoneR) ||
				string.IsNullOrWhiteSpace(_adressR) ||
				_birth == null)
			{
				return false;
			}
			else
				return true;
		}		
		
		private void Clear_Method(object p)
		{
			NameR = "";
			SurnameR = "";
			AdressR = "";
			PhoneR = "";
			Birth = DateTime.Now;			
		}

		protected override void Add(object parameter)
		{


			Reader r = new Reader
			{
				Name = NameR,
				Surname = SurnameR,
				Adress = AdressR,
				Phone = PhoneR,
				DateOfBirth = Birth
			};

			ManageReaders.AddReader(r);
			Clear_Method(parameter);
			UpdateList(parameter);

			
		}

		protected override void Delete(object parameter)
		{
			ManageReaders.DeleteReader(SelectedItem);
			UpdateList(parameter);
		}

		protected override void Edit(object parameter)
		{
			NameR = SelectedItem.Name;
			SurnameR = SelectedItem.Surname;
			PhoneR = SelectedItem.Phone;
			AdressR = SelectedItem.Adress;
			Birth = SelectedItem.DateOfBirth;			
		}

		protected override void UpdateList(object parameter)
		{
			List = ManageReaders.List();
		}

		
	}
}
