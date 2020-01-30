using CRM.Model;
using Prism.Mvvm;
using Prism_Library.Layer;
using Prism_Library.Model.Command;
using Prism_Library.Model.ListView;
using System.Collections.Specialized;
using System.Windows;

namespace Prism_Library.ViewModels
{
	public class GenreViewModel: CommandToEntity<Genre>
	{		
		string _name;
		private Genre _selectedItem;

		public string Name
		{
			get => (_name == null) ? _name = "" : _name;
			set => SetProperty(ref _name, value);

		}
		public override Genre SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}
		
		public GenreViewModel()
		{
			List = ManageGenre.UpdateList();
		}
		#region Commands
		protected override void Add(object parameter)
		{
			ManageGenre.AddGenre(Name);
			UpdateList(parameter);
			Name = "";
		}

		protected override void Delete(object parameter)
		{
			ManageGenre.DeleteGenre(SelectedItem);
			UpdateList(parameter);
		}

		protected override void Edit(object parameter)
		{
			throw new System.NotImplementedException();
		}

		protected override void UpdateList(object parameter)
		{
			List = ManageGenre.UpdateList();
		}		
		#endregion

	}
}
