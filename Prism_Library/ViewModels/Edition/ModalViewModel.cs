using CRM.Model;
using Prism_Library.Layer;
using Prism_Library.Model.Command;
using Prism_Library.Model.ListView;
using Prism_Library.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prism_Library.ViewModels.Edition
{
	public class ModalViewModel : CommandToEntity<Author> 
	{
		Window _window;
		Author _author;


		private string _title = "Add";
		private string _find;

		public Window Window
		{
			get => _window;
			set => SetProperty(ref _window, value);
		}
		public Author Author
		{
			get => _author;
			set => SetProperty(ref _author, value);
		}
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}
		public string Find
		{
			get => _find;
			set => SetProperty(ref _find, value);
		}


		Author _selectedItem;
		public override Author SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}
		

		
		#region Constructor
		public ModalViewModel()
		{
			List = ManageAuthor.ListAuthor();
		}
		public ModalViewModel(Window window): this()
		{
			Window = window;
		}
		public ModalViewModel(ref Author entity): this()
		{
		}
		#endregion



		#region Function
		protected override void Add(object parameter)
		{
			Window.Close();
		}

		protected override void Delete(object parameter)
		{
			throw new NotImplementedException();
		}

		protected override void Edit(object parameter)
		{
			throw new NotImplementedException();
		}

		protected override void UpdateList(object parameter)
		{
			//List = ManageModalAdd<E>.ListAdd();
		}
		#endregion


		#region Command
		private ICommand _findCommand;
		public ICommand FindCommand
		{
			get => (_findCommand == null) ?
				_findCommand = new RelayCommand(new Action<object>(Find_Method), new Predicate<object>(CanExecute)) :
				_findCommand;

			set { _findCommand = value; }
		}

		private bool CanExecute(object obj)
		{
			if (Find == null)
				return false;

			return true;
		}
		#endregion

		private void Find_Method(object obj)
		{
			List = AuthorProvider.FindList(Find);
		}



	}

}
