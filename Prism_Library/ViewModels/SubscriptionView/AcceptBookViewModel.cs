using CRM.Model;
using Prism_Library.Layer;
using Prism_Library.Model.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Library.ViewModels.SubscriptionView
{
	public class AcceptBookViewModel : CommandToEntity<Reader>
	{
		private string _title = "Accept Book";
		public string Title
		{
			get { return _title; }
			set => SetProperty(ref _title, value);
		}


		public AcceptBookViewModel()
		{
			List = ManageReaders.List();
		}

		#region Fields
		private DateTime? _dateOfBirth;
		private string _adress;
		private string _phone;
		string _name;
		int? _year;
		string _inventoryNumber;
		string _author;
		private DateTime? _dateOfIssue;
		private DateTime? _offerDate;
		private DateTime? _toAcceptDate;
		public DateTime? DateOfBirth
		{
			get => _dateOfBirth;
			set => SetProperty(ref _dateOfBirth, value);
		}
		public string Adress
		{
			get => (_adress == null) ? _adress = "" : _adress;
			set => SetProperty(ref _adress, value);
		}
		public string Phone
		{
			get => (_phone == null) ? _phone = "" : _phone;
			set => SetProperty(ref _phone, value);
		}
		public string InventoryNum
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
		public string AuthorName
		{
			get => _author;
			set => SetProperty(ref _author, value);
		}

		/// <summary>
		/// дата видачі
		/// </summary>
		public DateTime? DateOfIssue
		{
			get => _dateOfIssue;
			set => SetProperty(ref _dateOfIssue, value);
		}


		/// <summary>
		/// запропонована дата повернення
		/// </summary>		
		public DateTime? OfferDate
		{
			get => _offerDate;
			set => SetProperty(ref _offerDate, value);
		}

		/// <summary>
		/// реальна дата повернення
		/// </summary>
		public DateTime? ToAcceptDate
		{
			get => _toAcceptDate;
			set => SetProperty(ref _toAcceptDate, value);
		}
		#endregion




		#region методи і функціїї

		private Reader _selectedItem;
		private BookToAcceptClass _selectedBook;
		private ObservableCollection<BookToAcceptClass> _listBook;


		public ObservableCollection<BookToAcceptClass> ListBook
		{
			get => (_listBook == null) ? _listBook = ManageAcceptBook.ListBookToReader(SelectedItem) : _listBook;
			set => SetProperty(ref _listBook, value);
		}
		public BookToAcceptClass SelectedBook
		{
			get => _selectedBook;
			set => SetProperty(ref _selectedBook, value);
		}


		public override Reader SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				InfoToReaders(value);
				ListBook = ManageAcceptBook.ListBookToReader(SelectedItem);
			}
		}
		private void InfoToReaders(Reader value)
		{
			if (value != null)
			{
				DateOfBirth = value.DateOfBirth;
				Adress = value.Adress;
				Phone = value.Phone;
			}
		}
		#endregion


		#region Реалізація абстрактного класу
		protected override void Add(object parameter)
		{
			if(SelectedBook != null && ToAcceptDate == null)
			{
				if(SelectedBook != null)
					ToAcceptDate = SelectedBook.ToAcceptDate;

				ManageAcceptBook.Accept(SelectedBook, ToAcceptDate);
				UpdateList(parameter);
			}

			
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
			List = ManageReaders.List();
		}

		#endregion
	}
}
