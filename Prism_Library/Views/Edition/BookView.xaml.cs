using Prism_Library.Model.Command;
using Prism_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prism_Library.Views
{
	/// <summary>
	/// Логика взаимодействия для AddBookView.xaml
	/// </summary>
	public partial class BookView : UserControl
	{
		BookViewModel _context = new BookViewModel();

		public BookView()
		{
			InitializeComponent();

			

			cbcA.ItemsSource = _context.AuthorSearch;
			cbcG.ItemsSource = _context.GenreSearch;
			cbcP.ItemsSource = _context.PublishingsSearch;

			//context.thisWindow = this;

			//DataContext = context;

			//((RelayCommand)bookVM.UpdateListCommand).CheckAndExecute(bookVM);



		
			
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//cmA.ItemsSource = _context.AuthorSearch;
			cmP.ItemsSource = _context.PublishingsSearch;
			cmG.ItemsSource = _context.GenreSearch;
		}
	}
}
