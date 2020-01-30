using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CRM.Model;
using DataLayer;
using System.Data.Entity;

//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;

namespace Prism_Library.Provider
{
	public class BookProvider
	{
		private static FolderBrowserDialog _FBD = new FolderBrowserDialog();



		internal static void Deleted(Book selectedBook)
		{

			try
			{		
				DatabaseConnection.Remove<Book>(x => x.BookId == selectedBook.BookId);
			}
			catch (Exception e)
			{
				System.Windows.MessageBox.Show("Cannot be deleted from database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		internal static void Add(Book newItems)
		{

			try
			{
				DatabaseConnection.Add<Book>(newItems);
			}
			catch (Exception e)
			{
				System.Windows.MessageBox.Show("Cannot add a record to the database.\n" + e, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			
		}

		internal static ObservableCollection<Book> List()
		{
			var list = DatabaseConnection.List<Book>();
			//list.Sort();


			return list;
		}

		internal static void ExcelView(ObservableCollection<Book> list)
		{
			using (var db = new CrmContext(DatabaseConnection.ConnectionString))
			{
				try
				{
					_FBD.SelectedPath = DatabaseConnection.DirectoryExcel;

					if (_FBD.ShowDialog() == DialogResult.OK)
					{
						DatabaseConnection.DirectoryExcel = _FBD.SelectedPath;

						if (!DatabaseConnection.DirectoryExcel.EndsWith("\\"))
							DatabaseConnection.DirectoryExcel += "\\";

						string _fileName = DatabaseConnection.DirectoryExcel + "books.xls";

						if (File.Exists(_fileName))
						{
							var app1 = new Excel.Application();
							app1.Workbooks.Open(Filename: _fileName);
							app1.Visible = true;
						}
						else
						{
							var result = from book in list
										 join a in db.Authors on book.AuthorId equals a.AuthorId
										 join g in db.Genres on book.GenreId equals g.GenreId
										 join p in db.Publishings on book.PublishingId equals p.PublishingId
										 select new
										 {
											 _Author = a.NS,
											 _Genre = g.Name,
											 _Publish = p.Name,
											 _Inventory = book.InventoryNum,
											 _Name = book.Name,
											 _Year = book.Year,
											 _BookId = book.BookId
										 };


							var app = new Excel.Application { Visible = true };




							Excel.Workbook workbook = app.Workbooks.Add();
							Excel.Worksheet worksheet = (Excel.Worksheet)app.ActiveSheet;

							int row = 1;

							for (int i = 0; i < 1; i++)
							{
								//worksheet.Cells.Font.Bold = true;
								worksheet.Range["A1", "G1"].Font.Bold = true;
								worksheet.Range["A1", "G1"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
								worksheet.Range["A1", "G1"].Borders.Weight = Excel.XlBorderWeight.xlThick;
								//worksheet.Range["A1", "G1"].AutoFit();
								worksheet.Columns.AutoFit();


								worksheet.Cells[row, 1] = "BookId";
								worksheet.Cells[row, 2] = "Author";
								worksheet.Cells[row, 3] = "Name";
								worksheet.Cells[row, 4] = "Genre";
								worksheet.Cells[row, 5] = "Publishing";
								worksheet.Cells[row, 6] = "Year";
								worksheet.Cells[row, 7] = "InventoryNum";

								row++;

							}


							foreach (var books in result)
							{
								worksheet.Cells[row, 1] = books._BookId;
								worksheet.Cells[row, 2] = books._Author;
								worksheet.Cells[row, 3] = books._Name;
								worksheet.Cells[row, 4] = books._Genre;
								worksheet.Cells[row, 5] = books._Publish;
								worksheet.Cells[row, 6] = books._Year;
								worksheet.Cells[row, 7] = books._Inventory;
								row++;
							}

							worksheet.Range["A2", "G" + row.ToString()].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

							workbook.SaveAs(Filename: _fileName, FileFormat: XlFileFormat.xlWorkbookNormal);
						}
					}


					_FBD.Dispose();
				}
				catch (Exception e)
				{
					System.Windows.MessageBox.Show(e.ToString());
					
				}



			}
			

			//app.Application.Quit();
		}
	}
}
