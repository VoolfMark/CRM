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
	/// Логика взаимодействия для AuthorView.xaml
	/// </summary>
	public partial class AuthorView : Window
	{
		public AuthorView()
		{
			InitializeComponent();

			//this.Activated += AuthorView_Activated;
			
		}

		//private void AuthorView_Activated(object sender, EventArgs e)
		//{
		//	AuthorViewModel.Update(sender);
		//}
	}
}
