﻿using Prism_Library.ViewModels.Login;
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

namespace Prism_Library.Views.Login
{
	/// <summary>
	/// Логика взаимодействия для Register.xaml
	/// </summary>
	public partial class RegisterView : Window
	{
		public RegisterView()
		{
			InitializeComponent();

			RegisterViewModel RVM = new RegisterViewModel();
			RVM.RegWindow = this;
		}
	}
}
