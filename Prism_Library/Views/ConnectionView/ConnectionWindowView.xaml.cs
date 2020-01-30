using Prism_Library.ViewModels.Connection;
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
using System.Windows.Shapes;

namespace Prism_Library.Views.ConnectionView
{
	/// <summary>
	/// Логика взаимодействия для ConnectionWindowView.xaml
	/// </summary>
	public partial class ConnectionWindowView : Window
	{
		public ConnectionWindowView()
		{
			InitializeComponent();

			ConnectionWindowViewModel context = new ConnectionWindowViewModel() { ConnectionWindow = this };

			DataContext = context;
		}
	}
}
