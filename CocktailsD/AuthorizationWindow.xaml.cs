using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
	/// Логика взаимодействия для AuthorizationWindow.xaml
	/// </summary>
	public partial class AuthorizationWindow : Window
	{

		DataBaseWork database;

		string userName = string.Empty;

		public AuthorizationWindow()
		{
			InitializeComponent();
		}

		public Guid GetHashString(string s)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(s);
			MD5CryptoServiceProvider CSP =
				new MD5CryptoServiceProvider();
			byte[] byteHash = CSP.ComputeHash(bytes);

			string hash = string.Empty;
			foreach (byte b in byteHash)
				hash += string.Format("{0:x2}", b);

			return new Guid(hash);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			database = new DataBaseWork();

			textName.Focus();

			// Программное Добавление пользователей в БД

			//User admin = new User();
			//admin.Name = "admin";
			//admin.Password = GetHashString("1111");
			//database.AddUserToDatabase(admin);
			//User barman = new User();
			//barman.Name = "barman";
			//barman.Password = GetHashString("1111");
			//database.AddUserToDatabase(barman);
		}

		public string GetUserName()
		{
			return userName;
		}

		private void Authorization()
		{
			if (textName.Text != string.Empty && textPassword.Password != string.Empty)
			{
				User user = new User();
				user.Name = textName.Text;
				user.Password = GetHashString(textPassword.Password);
				if (database.IsExistUserInDatabase(user) == true)
				{
					userName = textName.Text;
					DialogResult = true;
				}
				else
				{
					labelError.Content = "Неверный логин или пароль!";
				}
			}
			else
			{
				labelError.Content = "Заполните все поля!";
			}
		}

		private void buttonOk_Click(object sender, RoutedEventArgs e)
		{
			Authorization();
		}

		private void textPassword_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Authorization();
			}
		}
	}
}
