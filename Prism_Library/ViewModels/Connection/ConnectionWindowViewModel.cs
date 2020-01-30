using Prism.Mvvm;
using Prism_Library.Layer;
using Prism_Library.Model.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Prism_Library.ViewModels.Connection
{
	class ConnectionWindowViewModel: BindableBase
	{
		
		public Window ConnectionWindow { get; set; }

		#region Connected

		public string ConnectionState { get => Layer.Connection.TestConnection() ? "Connected" : "Not connected"; }
		public string ConnectedFile
		{
			get
			{
				string file = Layer.Connection.Directory + Layer.Connection.DbName;

				if (!string.IsNullOrWhiteSpace(file))
					file += ".mdf";

				return file;
			}
		}
		private static string _baseDir
		{
			get
			{
				string baseDir = AppDomain.CurrentDomain.BaseDirectory;
				if (!baseDir.EndsWith("\\"))
					baseDir += "\\";
				return baseDir;
			}
		}
		private string _directory;
		public string Directory
		{
			get
			{
				if (_directory == null)
				{
					_directory = (string.IsNullOrWhiteSpace(Layer.Connection.Directory) ? _baseDir : Layer.Connection.Directory);
					CollectDbNames(_directory);
				}
				return _directory;
			}
			set
			{
				if (_directory != value)
					CollectDbNames(value);
				SetProperty(ref _directory, value);
			}
		}

		private string _dbname;
		public string DbName
		{
			get
			{
				if (_dbname == null)
					_dbname = (string.IsNullOrWhiteSpace(Layer.Connection.DbName) ? "Database" : Layer.Connection.DbName);

				return _dbname;
			}
			set => SetProperty(ref _dbname, value); 
		}
		#endregion


		#region Method & List
		private void CollectDbNames(string dir)
		{
			List<string> list = new List<string>();

			var filenames = System.IO.Directory.GetFiles(dir, "*.mdf");

			foreach (var name in filenames)
			{
				list.Add(Path.GetFileNameWithoutExtension(name));
			}

			DbNameList = list;
		}
		
		private List<string> _dbnamelist;
		public List<string> DbNameList
		{
			get { return _dbnamelist; }
			set { SetProperty(ref _dbnamelist, value); }
		}

		#endregion


		#region Command

		private ICommand _connectDatabaseCommand;
		public ICommand ConnectDatabaseCommand
		{
			get
			{
				if (_connectDatabaseCommand == null)
					_connectDatabaseCommand = new RelayCommand(new Action<object>(ConnectDatabase), new Predicate<object>(CanConnectDatabase));

				return _connectDatabaseCommand;
			}
			set { SetProperty(ref _connectDatabaseCommand, value); }
		}
		private void ConnectDatabase(object parameter)
		{			
			if (Layer.Connection.ChangeDatabase(Directory, DbName))
			{
				ConnectionWindow?.Close();
			}
			else
				System.Windows.MessageBox.Show("Database connection failed.", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);

			RaisePropertyChanged("ConnectionState");
			RaisePropertyChanged("ConnectedFile");
		}
		private bool CanConnectDatabase(object parameter)
		{
			return File.Exists(Directory + DbName + ".mdf");
		}



		private ICommand _createDatabaseCommand;
		public ICommand CreateDatabaseCommand
		{
			get
			{
				if (_createDatabaseCommand == null)
					_createDatabaseCommand = new RelayCommand(new Action<object>(CreateDatabase), new Predicate<object>(CanCreateDatabase));
				return _createDatabaseCommand;
			}
			set { SetProperty(ref _createDatabaseCommand, value); }
		}
		private void CreateDatabase(object parameter)
		{			
			if (Layer.Connection.CreateDatabase(Directory, DbName))
			{
				RaisePropertyChanged("ConnectionState");
				RaisePropertyChanged("ConnectedFile");
				CollectDbNames(Directory);
				ConnectionWindow?.Close();
			}
			else
				System.Windows.MessageBox.Show("Database creation failed.", "CNew database error", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		private bool CanCreateDatabase(object parameter)
		{
			return !File.Exists(Directory + DbName + ".mdf");
		}



		private ICommand _selectDirectoryCommand;
		public ICommand SelectDirectoryCommand
		{
			get
			{
				if (_selectDirectoryCommand == null)
					_selectDirectoryCommand = new RelayCommand(new Action<object>(SelectDirectory));
				return _selectDirectoryCommand;
			}
			set { SetProperty(ref _selectDirectoryCommand, value); }
		}

		private FolderBrowserDialog _FBD = new FolderBrowserDialog();
		private void SelectDirectory(object parameter)
		{
			_FBD.SelectedPath = Directory;

			if (_FBD.ShowDialog() == DialogResult.OK)
			{
				Directory = _FBD.SelectedPath;

				if (!Directory.EndsWith("\\"))
					Directory += "\\";
			}


		}
		


		#endregion
	}
}
