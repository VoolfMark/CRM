using CRM.Model;
using Prism.Mvvm;
using Prism_Library.Layer;
using Prism_Library.Model.Command;
using Prism_Library.Model.ListView;
using Prism_Library.ViewModels.SubscriptionView.Utility;
using Prism_Library.Views.Subscription.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prism_Library.ViewModels
{
	public class GiveBookViewModel: CommandToEntity<CopyesBook>
	{
		private string _title = "Give Book";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}			

		public GiveBookViewModel()
		{
			List = ManageCopyes.List();
			CountCopyes += List.Count.ToString();
		}

		private void EventToSelected(CopyesBook entity)
		{
			//Book book = ManageGiveBook.ReturnBookCopyes(entity);

			Task<Book> task = new Task<Book>(() => ManageGiveBook.ReturnBookCopyes(entity));
			task.Start();

			Book book = task.Result;

			Author = ManageGiveBook.ReturnAuthorProperty(book);


			if (book == null)
				Inventory = "";
			else
				Inventory = book.InventoryNum;


			Genre = ManageGiveBook.ReturnGenreName(book);
			Publishing = ManageGiveBook.ReturnPublishingName(book);
		}

		#region Fields

		string _author;
		string _publishing;
		string _genre;
		string _inventory;
		string _findName;
		string _countCopyes = "Number of copies: ";
		private CopyesBook _selectedItem;

		public string CountCopyes
		{
			get => _countCopyes;
			set => SetProperty(ref _countCopyes, value);
		}
		public string FindName
		{
			get => _findName ?? (_findName = "");
			set => SetProperty(ref _findName, value);			
		}		
		public string Publishing
		{
			get
			{
				if (_publishing == null)
					_publishing = "";				

				return _publishing;
			}
			set => SetProperty(ref _publishing, value);
		}
		public string Genre
		{
			get
			{
				if (_genre == null)
					_genre = "";				

				return _genre;
			}

			set => SetProperty(ref _genre, value);
		}
		public string Inventory
		{
			get
			{
				if (_inventory == null)
					_inventory = "";
				

				return _inventory;
			}
			set => SetProperty(ref _inventory, value);
		}
		public string Author
		{
			get
			{
				if (_author == null)
					_author = "";

				return _author;
			}
			set => SetProperty(ref _author, value);		
		}

		#endregion


		#region Methods
		public override CopyesBook SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				
				EventToSelected(value);
			}
		}



		protected override void UpdateList(object parameter)
		{
			List = ManageCopyes.List();
			CountCopyes = "Number of copies: " + List.Count.ToString();
		}

		protected override void Add(object parameter)
		{
			//викликати вікно видачі книги
			//передати вибраний екземпляр новому вікну
			GiveModalViewModel GMVM = new GiveModalViewModel(SelectedItem);
			GiveModalView GMV = new GiveModalView { DataContext = GMVM };

			GMV.ShowDialog();


			UpdateList(parameter);
		}

		protected override void Delete(object parameter)
		{
			throw new NotImplementedException();
		}

		protected override void Edit(object parameter)
		{
			throw new NotImplementedException();
		}
		#endregion



		#region Commands

		ICommand _click_Find;
		ICommand _refresh_Command;

		public ICommand Refresh_Command
		{
			get => _refresh_Command ?? (_refresh_Command = new RelayCommand(new Action<object>(Refresh_method)));
			set => SetProperty(ref _refresh_Command, value);
		}

		public ICommand Click_Find
		{
			get => _click_Find ?? (_click_Find = new RelayCommand(new Action<object>(Find_Method), new Predicate<object>(CanFind)));

			set => SetProperty(ref _click_Find, value);
		}

		private void Refresh_method(object obj)
		{
			SelectedItem = null;

			List = ManageCopyes.List();
			CountCopyes = "Number of copies: " + List.Count.ToString();
			//RaisePropertyChanged(nameof(List));
		}
		private bool CanFind(object obj)
		{
			if (string.IsNullOrWhiteSpace(_findName))
				return false;

			return true;
		}
		private void Find_Method(object obj)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(_findName))
					List = ManageCopyes.ListFind(_findName);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		#endregion
	}
}
