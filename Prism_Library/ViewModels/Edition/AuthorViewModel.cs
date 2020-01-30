using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;


//using Prism_Library.Model;
using Prism_Library.Model.Command;
using CRM.Model;

using Prism.Mvvm;
using Prism_Library.Layer;
using Prism_Library.Model.ListView;
using NLog;

namespace Prism_Library.ViewModels
{
	public class AuthorViewModel : CommandToEntity<Author>
	{
		string _surname;
		string _name;
		int _count;

		public int Count
		{
			get
			{
				return List.Count;
			}

			set => SetProperty(ref _count, value);
		}
		public string Name
		{
			get => _name ?? (_name = "");
			set
			{
				SetProperty(ref _name, value);
			}
		}
		public string Surname
		{
			get => _surname ?? (_surname = "");
			set
			{
				SetProperty(ref _surname, value);
			}
		}

		internal static void Update(object p)
		{
			
		}

		public AuthorViewModel()
		{			
			List = ManageAuthor.ListAuthor();
		}

		

		#region         
		ICommand _click_ClearCommand;
		private Author _selectedItem;

		public ICommand Click_ClearCommand
		{
			get
			{
				if (_click_ClearCommand == null)
					_click_ClearCommand = new RelayCommand(new Action<object>(Clear_Method));
				return _click_ClearCommand;
			}
		}

		public override Author SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}



		#endregion


		#region методи команд
		private void Clear_Method(object p)
		{
			Name = null;
			Surname = null;
		}


		#endregion





		#region Реалізація абстрактного класу
		protected override void Add(object parameter)
		{
			ManageAuthor.AddAuthor(_name, _surname);

			List.Add(new Author { Name = _name, Surname = _surname });

			UpdateList(parameter);

			Name = null;
			Surname = null;
			Count++;
			RaisePropertyChanged(nameof(List));
		}

		protected override void Delete(object parameter)
		{
			ManageAuthor.DeleteAuthor(SelectedItem);

			List.Remove(SelectedItem);

			UpdateList(parameter);
			Count--;
			RaisePropertyChanged(nameof(List));
		}

		protected override void Edit(object parameter)
		{
			throw new NotImplementedException();
		}

		protected override void UpdateList(object parameter)
		{
			List = ManageAuthor.ListAuthor();
			//RaisePropertyChanged(nameof(List));
		}

		#endregion




		

	}
}
