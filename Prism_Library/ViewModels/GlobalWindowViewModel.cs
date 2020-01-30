using System;
using System.Windows;
using System.Windows.Input;
using NLog;


using Prism.Mvvm;
using Prism.Regions;
using Prism_Library.Layer;
using Prism_Library.Model.Command;
using Prism_Library.Views;
using Prism_Library.Views.ConnectionView;
using Prism_Library.Views.Login;

namespace Prism_Library.ViewModels
{
	public class GlobalWindowViewModel: BindableBase
	{
	

		#region Public Properties
		private readonly IRegionManager _regionManager;
		
		#endregion

		private string _title = "Library";
		public string Title
		{
			get => _title;
			set { SetProperty(ref _title, value); }
		}
	
		public GlobalWindowViewModel(IRegionManager regionManager)
		{
			_regionManager = regionManager;


			try
			{
				if (!Layer.Connection.TestConnection())
				{
					ConnectionWindowView CWV = new ConnectionWindowView();
					CWV.ShowDialog();

					if (!Layer.Connection.TestConnection())
					{
						Application.Current.Shutdown();
						return;
					}
				}


				LoginView LV = new LoginView();
				LV.ShowDialog();

				if (Loginner.Logined == "")
					Application.Current.Shutdown();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Program initialization error.\nCheck RedLog.txt for more information.\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				
				return;
			}

			
		}

		#region Команди меню

		private ICommand _authorAdd;
		private ICommand _genreListCommand;
		private ICommand _click_publishingListCommand;
		private ICommand _click_openReaderListCommand;
		private ICommand _click_openAddBookCommand;
		private ICommand _click_giveBookCommand;
		private ICommand _clock_acceptBookCommand;
		private ICommand _click_GiveBookViewCommand;
		private ICommand _click_AcceptBookViewCommand;


		public ICommand Click_AcceptBookViewCommand
		{
			get => (_click_AcceptBookViewCommand == null) ?
				_click_AcceptBookViewCommand = new RelayCommand<string>(Navigate_Method) :
				_click_AcceptBookViewCommand;
			set => SetProperty(ref _click_AcceptBookViewCommand, value);
		}
		public ICommand Click_GiveBookViewCommand
		{
			get => (_click_GiveBookViewCommand == null) ?
				_click_GiveBookViewCommand = new RelayCommand<string>(Navigate_Method) :
				_click_GiveBookViewCommand;

			set => SetProperty(ref _click_GiveBookViewCommand, value);
		}
		public ICommand GenreListCommand
		{
			get => (_genreListCommand == null) ?
				_genreListCommand = new RelayCommand<string>(GenreList_Method) :
				_genreListCommand;

			set => SetProperty(ref _genreListCommand, value);
		}
		public ICommand AuthorAdd
		{
			get
			{
				if (_authorAdd == null)
					_authorAdd = new RelayCommand(new Action<object>(AuthorAdd_Method));				

				return _authorAdd;
			}
			set
			{
				SetProperty(ref _authorAdd, value);
			}
		}
		public ICommand Click_PublishingListCommand
		{
			get => (_click_publishingListCommand == null) ?
				_click_publishingListCommand = new RelayCommand<string>(PublishingList_Method) :
				_click_publishingListCommand;

			set => SetProperty(ref _genreListCommand, value);
		}		
		public ICommand Click_AcceptBookCommand
		{
			get => (_clock_acceptBookCommand == null) ?
				_clock_acceptBookCommand = new RelayCommand<string>(Navigate_Method) :
				_clock_acceptBookCommand;

			set => SetProperty(ref _clock_acceptBookCommand, value);
		}
		public ICommand Click_GiveBookCommand
		{
			get => (_click_giveBookCommand == null) ?
				_click_giveBookCommand = new RelayCommand<string>(Navigate_Method) :
				_click_giveBookCommand;

			set => SetProperty(ref _click_giveBookCommand, value);
		}
		public ICommand Click_OpenAddBookCommand
		{
			get => (_click_openAddBookCommand == null) ?
				_click_openAddBookCommand = new RelayCommand<string>(Navigate_Method) :
				_click_openAddBookCommand;

			set => SetProperty(ref _click_openAddBookCommand, value);
		}	
		public ICommand Click_openReaderListCommand
		{
			get => (_click_openReaderListCommand == null) ?
				_click_openReaderListCommand = new RelayCommand<string>(Navigate_Method) :
				_click_openReaderListCommand;

			set => SetProperty(ref _click_openReaderListCommand, value);
		}

		#endregion

		#region методи команд меню
		
	
		
		private void Navigate_Method(string p)
		{
			
			if (p != null)
			{			
				_regionManager.RequestNavigate("CurrentView", p);
								
			}
		}

		

		private void PublishingList_Method(string obj)
		{
			PublishingView PV = new PublishingView();
			PV.ShowDialog();
		}
		private void GenreList_Method(string obj)
		{
			GenreView GV = new GenreView();
			GV.ShowDialog();
		}
		private void AuthorAdd_Method(object p)
		{
			AuthorView AV = new AuthorView();
			
			AV.ShowDialog();
			
		}
		#endregion



		#region кoманди для головного вікна	


		private Window _globalWindow;
		public Window GlobalWindow
		{
			get { return _globalWindow; }
			set { SetProperty(ref _globalWindow, value); }
		}

		private ICommand _closeMainWindowCommand;
		public ICommand CloseMainWindowCommand
		{
			get
			{
				if (_closeMainWindowCommand == null) _closeMainWindowCommand = new RelayCommand(CloseWindow);
				return _closeMainWindowCommand;
			}
			set { SetProperty(ref _closeMainWindowCommand, value); }
		}

		private void CloseWindow(object parameter = null)
		{

			GlobalWindow?.Close();
			Application.Current.Shutdown();
		}
		#endregion
	}
}
