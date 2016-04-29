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

namespace CocktailsD
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DataBaseWork database;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void LoadElement()
		{
			if (listCocktails.SelectedItems.Count != 0)
			{
				labelDescription.Content =
				((Cocktail)((ListBoxItem)listCocktails.Items[listCocktails.SelectedIndex]).Tag).Description;
				buttonDelete.IsEnabled = true;
				buttonEdit.IsEnabled = true;
			}
			else
			{
				labelDescription.Content = string.Empty;
				buttonDelete.IsEnabled = false;
				buttonEdit.IsEnabled = false;
			}
		}

		private void ReloadData()
		{
			List<Cocktail> cocktails = database.GetCocktailsList();
			listCocktails.Items.Clear();
			foreach (Cocktail elem in cocktails)
			{
				ListBoxItem item = new ListBoxItem();
				item.Content = elem.Name.TrimEnd();
				item.Tag = elem;
				listCocktails.Items.Add(item);
			}
			if (listCocktails.Items.Count != 0)
			{
				LoadElement();
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.Visibility = System.Windows.Visibility.Hidden;
			database = new DataBaseWork();
			AuthorizationWindow authorization = new AuthorizationWindow();
			if (authorization.ShowDialog() == (Nullable<bool>)true)
			{
				this.Name = authorization.GetUserName();
				this.Visibility = System.Windows.Visibility.Visible;
				ReloadData();
			}
			else
			{
				Application.Current.Shutdown();
			}
		}

		private void buttonDelete_Click(object sender, RoutedEventArgs e)
		{
			if (listCocktails.SelectedItems.Count != 0)
			{
				database.DeleteCocktailInDatabase(
					((Cocktail)((ListBoxItem)listCocktails.Items[listCocktails.SelectedIndex]).Tag));
				ReloadData();
			}
		}

		private void buttonEdit_Click(object sender, RoutedEventArgs e)
		{
			if (listCocktails.SelectedItems.Count != 0)
			{
				EditWindow editWindow = new EditWindow(
					((Cocktail)((ListBoxItem)listCocktails.Items[listCocktails.SelectedIndex]).Tag));
				if (editWindow.ShowDialog() == (Nullable<bool>)true)
				{
					database.UpdateCocktailToDatabase(editWindow.GetCocktail());
					ReloadData();
				}
			}
		}

		private void buttonAdd_Click(object sender, RoutedEventArgs e)
		{ 
			EditWindow editWindow = new EditWindow();
			if (editWindow.ShowDialog() == (Nullable<bool>)true)
			{
				database.AddCocktailToDatabase(editWindow.GetCocktail());
				ReloadData();
			}
		}

		private void listCocktails_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			LoadElement();
		}
	}
}
