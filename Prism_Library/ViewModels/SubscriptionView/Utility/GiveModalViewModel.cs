using Prism_Library.Model.ListView;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism_Library.Layer;
using System.Collections.ObjectModel;

namespace Prism_Library.ViewModels.SubscriptionView.Utility
{
	public class GiveModalViewModel : CommandToEntity<Subscription>
	{
		#region Fields
		private CopyesBook _selectedCopy;
		private Subscription _selectedItem;
		private string _order;
		private DateTime? _dateOfIssue;
		private DateTime? _offerDate;
		private string _inventory;
		private Reader _selectedreader;



		public ObservableCollection<Reader> ReaderSearch
		{
			get
			{
				RaisePropertyChanged(nameof(List));
				return ManageReaders.List();
			}
		}

		public Reader SelectedReader
		{
			get => _selectedreader;
			set => SetProperty(ref _selectedreader, value);
		}
		public CopyesBook SelectedCopy
		{
			get => _selectedCopy;
			set => SetProperty(ref _selectedCopy, value);
		}
		public string Order
		{
			get => _order;
			set => SetProperty(ref _order, value);
		}
		public DateTime? DateOfIssue
		{
			get => (_dateOfIssue == null) ?
				DateTime.Now : _dateOfIssue;

			set => SetProperty(ref _dateOfIssue, value);
		}
		public DateTime? OfferDate
		{
			get => (_offerDate == null) ? 
				DateTime.Now.AddDays(7) : _offerDate;

			set => SetProperty(ref _offerDate, value);
		}
		public string Inventory
		{
			get => (_inventory == null) ? _inventory = "" : _inventory;
			set => SetProperty(ref _inventory, value);
		}
		#endregion


		//Передаємо вибраний екземпляр в конструктор
		public GiveModalViewModel(CopyesBook selectedItem)
		{
			this.SelectedCopy = selectedItem;

			Order = ManageGiveBook.ReturnNumOrder;
			Inventory = SelectedCopy.Inventory;
		}

		public override Subscription SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}

		protected override void Add(object parameter)
		{
			Subscription subscription = new Subscription
			{
				ReaderId = _selectedreader.ReaderId,
				CopyId = _selectedCopy.CopyId,
				DateOfIssue = DateOfIssue,
				OfferDate = OfferDate
			};

			ManageSubscription.AddSubscription(subscription);

			ClearMethod();
		}

		protected override void Delete(object parameter)
		{
			ClearMethod();
		}

		private void ClearMethod()
		{
			DateOfIssue = DateTime.Now;
			OfferDate = DateTime.Now.AddDays(7);
			SelectedReader = null;
		}

		protected override void Edit(object parameter)
		{
			throw new NotImplementedException();
		}

		protected override void UpdateList(object parameter)
		{
			throw new NotImplementedException();
		}
	}
}
