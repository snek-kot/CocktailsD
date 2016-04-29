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

namespace CocktailsD
{
	/// <summary>
	/// Логика взаимодействия для EditWindow.xaml
	/// </summary>
	public partial class EditWindow : Window
	{
		public EditWindow()
		{
			InitializeComponent();
		}

		Cocktail cocktail = new Cocktail();
		bool editMode = false;

		public EditWindow(Cocktail cocktail)
		{
			InitializeComponent();
			this.cocktail = cocktail;
			editMode = true;
		}

		public Cocktail GetCocktail()
		{
			return cocktail;
		}

		private void buttonOk_Click(object sender, RoutedEventArgs e)
		{
			if (textName.Text != string.Empty && textDescription.Text != string.Empty)
			{
				cocktail.Name = textName.Text;
				cocktail.Description = textDescription.Text;
				DialogResult = true;
			}
			else
			{
				labelError.Content = "Заполните все поля!";
			}
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (editMode == true)
			{
				textDescription.Text = cocktail.Description.TrimEnd();
				textName.Text = cocktail.Name.TrimEnd();
			}
		}
	}
}
