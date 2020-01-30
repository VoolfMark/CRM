using Prism.Mvvm;
using Prism_Library.Model.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prism_Library.Model.ListView
{
	public abstract class CommandToEntity<E>: BindableBase
	{

		#region Command
		private ICommand _editCommand;
		public ICommand EditCommand
		{
			get
			{
				if (_editCommand == null)
					_editCommand = new RelayCommand(new Action<object>(Edit), new Predicate<object>(EditCanExecute));
				return _editCommand;
			}
			set => SetProperty(ref _editCommand, value); 
		}


		private ICommand _addCommand;
		public ICommand AddCommand
		{
			get
			{
				if (_addCommand == null)
					_addCommand = new RelayCommand(new Action<object>(Add), new Predicate<object>(AddCanExecute));
				return _addCommand;
			}
			set => SetProperty(ref _addCommand, value); 
		}


		private ICommand _deleteCommand;
		public ICommand DeleteCommand
		{
			get
			{
				if (_deleteCommand == null)
					_deleteCommand = new RelayCommand(new Action<object>(Delete), new Predicate<object>(DeleteCanExecute));
				return _deleteCommand;
			}
			set => SetProperty(ref _deleteCommand, value); 
		}


		private ICommand _updateListCommand;
		public ICommand UpdateListCommand
		{
			get
			{
				if (_updateListCommand == null)
					_updateListCommand = new RelayCommand(new Action<object>(UpdateList));

				return _updateListCommand;
			}
			set => SetProperty(ref _updateListCommand, value);
		}

		


		#endregion


		private ObservableCollection<E> _list;
		public ObservableCollection<E> List
		{
			get => _list;
			set => SetProperty(ref _list, value);
		}
			   		
		public abstract E SelectedItem { get; set; }
		//{
		//	get => _selectedItem;
		//	set
		//	{
		//		SetProperty(ref _selectedItem, value);
		//		_selectedEvent(value);
		//	}
		//}


		

		
		protected abstract void UpdateList(object parameter);
		protected abstract void Add(object parameter);
		protected abstract void Delete(object parameter);
		protected abstract void Edit(object parameter);


		protected virtual bool ItemSelected(object parameter)
		{
			return (SelectedItem != null);
		}
		protected virtual bool AddCanExecute(object parameter)
		{
			return true;
		}
		protected virtual bool EditCanExecute(object parameter)
		{
			return ItemSelected(parameter);
		}
		protected virtual bool DeleteCanExecute(object parameter)
		{
			return ItemSelected(parameter);
		}
		
	}
}
