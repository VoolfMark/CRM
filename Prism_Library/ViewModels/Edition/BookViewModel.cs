using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


using Prism.Mvvm;
using Prism_Library.Model.Command;

using CRM.Model;
using Prism_Library.Layer;
using Prism_Library.Model.ListView;
using System.Collections;
using System.Windows.Controls;
using Prism_Library.ViewModels.Edition;
using Prism_Library.Views.Edition;
using Prism_Library.Provider;

namespace Prism_Library.ViewModels
{ 
	public class BookViewModel: CommandToEntity<Book>
	{
		private string _title = "Edit Book";
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}



		public BookViewModel()
		{
			List = ManageBook.List();
			
		}
		#region Commands button

		ICommand _clearCommand;
		ICommand _selectedAuthor;
		ICommand _excelViewCommand;

		public ICommand ExcelViewCommand
		{
			get => (_excelViewCommand == null) ?
				_excelViewCommand = new RelayCommand(new Action<object>(ExcelVeiw_Method), new Predicate<object>(Excel_CanExecute)) :
				_excelViewCommand;

			set => SetProperty(ref _excelViewCommand, value);
		}

		private void ExcelVeiw_Method(object obj)
		{
			BookProvider.ExcelView(List);
		}

		private bool Excel_CanExecute(object obj)
		{
			if (List.Any())
				return true;

			return false;
		}

		public ICommand SelectedAuthor
		{
			get => (_selectedAuthor == null) ?
				_selectedAuthor = new RelayCommand(new Action<object>(SelectedAuthor_Method)) :
				_selectedAuthor;

			set => SetProperty(ref _selectedAuthor, value);
		}

		private void SelectedAuthor_Method(object obj)
		{
			ModalViewModel modalView = new ModalViewModel();
			ModalView modal = new ModalView { DataContext = modalView };

			modalView.Window = modal;
			modal.ShowDialog();

			if (modalView.SelectedItem != null)
			{
				ChoiceAuthor = modalView.SelectedItem;
				AuthorText = ChoiceAuthor.ToString();
			}
		}

		public ICommand ClearCommand
		{
			get=> (_clearCommand == null) ?
				_clearCommand = new RelayCommand(new Action<object>(Clear_Method), new Predicate<object>(CanExecute)) :
				_clearCommand;

			set => SetProperty(ref _clearCommand, value);
		}

		
		private void Clear_Method(object obj)
		{
			Name = "";
			Year = DateTime.Now.Year;
			InventoryNumber = "";
			SelectedAuthor = null;
			SelectedGenre = null;
			SelectedPublishing = null;
			
		}
		private bool CanExecute(object obj)
		{
			if ((string.IsNullOrEmpty(_name)) && (_year == 0) &&
				(string.IsNullOrEmpty(_inventoryNumber)) && (AuthorSearch == null) &&
				(GenreSearch == null) && (PublishingsSearch == null))
			{
				return false;
			}


			return true;
		}

		protected override void Add(object parameter)
		{
			Book book = new Book
			{
				AuthorId = ChoiceAuthor.AuthorId,
				Name = _name,
				Year = _year,
				GenreId = SelectedGenre.GenreId,
				InventoryNum = _inventoryNumber,
				PublishingId = SelectedPublishing.PublishingId
			};

			
			ManageBook.AddBook(book);
			ManageCopyes.AddCopyes(book);

			ChoiceAuthor = null;
			AuthorText = "";
			UpdateList(parameter);
			Clear_Method(parameter);
		}

		protected override void Delete(object parameter)
		{
			ManageBook.DeleteBook(SelectedItem);
			ManageCopyes.DeleteCopyes(SelectedItem);
			UpdateList(parameter);
		}

		protected override void Edit(object parameter)
		{
			throw new NotImplementedException();
			
		}

		protected override void UpdateList(object parameter)
		{
			List = ManageBook.List();
		}



		#endregion




		#region Fields for forms

		string _name;		
		int? _year;
		string _inventoryNumber;
		string _authorText = "";




		public string AuthorText
		{
			get => (_authorText == null) ? _authorText = "" : _authorText;
			set => SetProperty(ref _authorText, value);
		}


		public string InventoryNumber
		{
			get => (_inventoryNumber == null) ? _inventoryNumber = "" : _inventoryNumber;
			set => SetProperty(ref _inventoryNumber, value);
		}
		public string Name
		{
			get => (_name == null) ? _name = "" : _name;
			set => SetProperty(ref _name, value);
		}
		public int? Year
		{
			get => _year;
			set => SetProperty(ref _year, value);
		}



		ObservableCollection<Author> _authors;
		ObservableCollection<Publishing> _publishingsSearch;
		public ObservableCollection<Author> AuthorSearch
		{
			get
			{
				_authors = ManageAuthor.ListAuthor();
				RaisePropertyChanged(nameof(List));
				return _authors;
			}
		}
		public ObservableCollection<Publishing> PublishingsSearch
		{
			get
			{
				RaisePropertyChanged(nameof(List));
				return ManagePublishing.List();
			}
			set => SetProperty(ref _publishingsSearch, value, nameof(List));
		}
		public ObservableCollection<Genre> GenreSearch
		{
			get
			{
				RaisePropertyChanged(nameof(List));
				return ManageGenre.UpdateList();
			}
		}
		

		

		Publishing _selectedPublishing;
		Genre _selectedGenre;		
		Book _selectedBook;
		private Author _choiceAuthor;
		private Book _selectedItem;
		

		public Author ChoiceAuthor
		{
			get => _choiceAuthor; 
			set => SetProperty(ref _choiceAuthor, value);
		}

		public Book SelectedBook
		{
			get => _selectedBook;
			set
			{
				SetProperty(ref _selectedBook, value);
			}
		}
		public Publishing SelectedPublishing
		{
			get => _selectedPublishing;
			set
			{
				SetProperty(ref _selectedPublishing, value);
			}
		}
		public Genre SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				SetProperty(ref _selectedGenre, value);
			}
		}		
		

		public override Book SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}

	


		
		#endregion


	}
}
