using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DQ7
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
			if (files == null) return;
			if (!System.IO.File.Exists(files[0])) return;

			if (SaveData.Instance().Open(files[0], false) == false)
			{
				MessageBox.Show("読込失敗");
				return;
			}
			Init();
			MessageBox.Show("読込成功");
		}

		private void MenuItemFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load(false);
		}

		private void MenuItemFileOpenForce_Click(object sender, RoutedEventArgs e)
		{
			Load(true);
		}

		private void MenuItemFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemFileSaveAs_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().SaveAs(dlg.FileName) == true) MessageBox.Show("書込成功");
			else MessageBox.Show("書込失敗");
		}

		private void MenuItemExit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow dlg = new AboutWindow();
			dlg.ShowDialog();
		}

		private void ToolBarFileOpen_Click(object sender, RoutedEventArgs e)
		{
			Load(false);
		}

		private void ToolBarFileSave_Click(object sender, RoutedEventArgs e)
		{
			Save();
		}

		private void MenuItemBattleMagicCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var magic in (ListBoxCharactor.SelectedItem as Charactor)?.BattleMagics)
			{
				magic.Leam = true;
			}
		}

		private void MenuItemBattleMagicUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var magic in (ListBoxCharactor.SelectedItem as Charactor)?.BattleMagics)
			{
				magic.Leam = false;
			}
		}

		private void MenuItemPlaceCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.Places)
			{
				place.Visit = true;
			}
		}

		private void MenuItemPlaceUnCheck_Click(object sender, RoutedEventArgs e)
		{
			foreach (var place in (DataContext as DataContext)?.Places)
			{
				place.Visit = true;
			}
		}

		private void ButtonCharactorItem_Click(object sender, RoutedEventArgs e)
		{
			CharactorItem item = (sender as Button)?.DataContext as CharactorItem;
			if (item == null) return;
			ItemSelectWindow dlg = new ItemSelectWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.ID = dlg.ID;
			item.Count = dlg.ID == 0 ? 0 : 1U;
		}

		private void ButtonBagItem_Click(object sender, RoutedEventArgs e)
		{
			BagItem item = (sender as Button)?.DataContext as BagItem;
			if (item == null) return;
			ItemSelectWindow dlg = new ItemSelectWindow();
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.Count = dlg.ID == 0 ? 0 : 1U;
			item.ID = dlg.ID;
		}

		private void Load(bool force)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			if (SaveData.Instance().Open(dlg.FileName, force) == false)
			{
				MessageBox.Show("読込失敗");
				return;
			}
			Init();
			MessageBox.Show("読込成功");
		}

		private void Init()
		{
			DataContext = new DataContext();
		}

		private void Save()
		{
			if (SaveData.Instance().Save() == true) MessageBox.Show("書込成功");
			else MessageBox.Show("書込失敗");
		}
	}
}
