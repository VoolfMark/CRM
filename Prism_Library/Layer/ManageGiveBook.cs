using System;
using System.Collections.Generic;
using System.Windows;
using CRM.Model;
using Prism_Library.Provider;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Prism_Library.Layer
{
	class ManageGiveBook
	{
		public static string ReturnNumOrder
		{
			get => GiveBookProvider.NumOrder;
		}

		internal static string ReturnAuthorProperty(Book selectedItem)
		{
			try
			{
				if (selectedItem != null)
					return GiveBookProvider.AuthorName(selectedItem);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return "";
		}

		internal static string ReturnGenreName(Book selectedItem)
		{
			try
			{
				if (selectedItem != null)
					return GiveBookProvider.GenreName(selectedItem);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return "";
		}

		internal static Book ReturnBookCopyes(CopyesBook entity)
		{
			try
			{
				if (entity != null)
					return GiveBookProvider.CopyToBook(entity);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return null;
		}

		//internal static async Task ReturnBookCopyes(CopyesBook entity)
		//{
		//	try
		//	{
		//		if (entity != null)
		//			return GiveBookProvider.CopyToBook(entity);
		//	}
		//	catch (Exception e)
		//	{
		//		MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
		//	}

		//	return 
		//}

		internal static string ReturnPublishingName(Book selectedItem)
		{
			try
			{
				if (selectedItem != null)
					return GiveBookProvider.PublishingName(selectedItem);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

			return "";
		}
	}

}
