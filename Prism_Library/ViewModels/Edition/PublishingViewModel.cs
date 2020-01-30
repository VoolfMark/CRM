using CRM.Model;
using Prism.Mvvm;
using Prism_Library.Layer;
using Prism_Library.Model.Command;
using Prism_Library.Model.ListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Prism_Library.ViewModels
{
	public class PublishingViewModel: CommandToEntity<Publishing>
	{		
		string _name;
		private Publishing _selectedItem;

		public string Name
		{
			get => (_name == null) ? _name = "" : _name;
			set => SetProperty(ref _name, value);

		}
		public override Publishing SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}
		
		public PublishingViewModel()
		{
			List = ManagePublishing.List();
		}

		#region Commands
		protected override void Add(object parameter)
		{
			ManagePublishing.AddPublishing(Name);
			List.Add(new Publishing { Name = this.Name });

			Name = "";
			UpdateList(parameter);
		}

		protected override void Delete(object parameter)
		{
			ManagePublishing.DeletePublishing(SelectedItem);
			List.Remove(SelectedItem);
			UpdateList(parameter);
		}

		protected override void Edit(object parameter)
		{
			throw new NotImplementedException();
		}

		protected override void UpdateList(object parameter)
		{
			List = ManagePublishing.List();
		}

		#endregion

	}
}
