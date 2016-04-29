using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Linq;

namespace CocktailsD
{
	class DataBaseWork
	{
		private DataContext dataContext;

		public DataContext GetDataContext()
		{
			return dataContext;
		}

		public DataBaseWork()
		{
			String DbName = "DatabaseMain.mdf";
			String DbDirName = Path.GetDirectoryName(
				Path.GetDirectoryName(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)));
			if (File.Exists(DbDirName + "\\" + DbName))
			{
				dataContext = new DataContext(String.Format(
					@"Data Source=(LocalDB)\v11.0;AttachDbFilename=""{0}\{1}"";Integrated Security=True",
					DbDirName,
					DbName
				));
			}
			else
			{
				dataContext = new DataContext(String.Format(
					@"Data Source=(LocalDB)\v11.0;AttachDbFilename=""{0}\{1}"";Integrated Security=True",
					Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory),
					DbName
				));
			}
			if (!dataContext.DatabaseExists())
			{
				string message = "База данных не существует!";
				string caption = "Ошибка!";
				MessageBox.Show(message, caption, MessageBoxButton.OK);
				Application.Current.Shutdown();
			}
		}

		public bool IsExistUserInDatabase(User user)
		{
			return (from elem in dataContext.GetTable<User>()
					where (elem.Name.Trim() == user.Name &&
					elem.Password == user.Password)
					select elem.Id).ToArray().Length != 0;
		}

		public void AddUserToDatabase(User user)
		{
			dataContext.GetTable<User>().InsertOnSubmit(user);
			dataContext.SubmitChanges();
		}

		public List<Cocktail> GetCocktailsList()
		{
			return (from elem in dataContext.GetTable<Cocktail>()
					select elem).ToList<Cocktail>();
		}

		public void AddCocktailToDatabase(Cocktail cocktail)
		{
			dataContext.GetTable<Cocktail>().InsertOnSubmit(cocktail);
			dataContext.SubmitChanges();
		}

		public void UpdateCocktailToDatabase(Cocktail cocktail)
		{
			Cocktail temp = (from elem in dataContext.GetTable<Cocktail>()
							 where elem.Id == cocktail.Id
								select elem).First();
			temp.Name = cocktail.Name;
			temp.Description = cocktail.Description;
			dataContext.SubmitChanges();
		}

		public void DeleteCocktailInDatabase(Cocktail cocktail)
		{
			dataContext.GetTable<Cocktail>().DeleteOnSubmit(
				(from elem in dataContext.GetTable<Cocktail>()
				 where elem.Id == cocktail.Id
				 select elem).First());
			dataContext.SubmitChanges();
		}
	}
}
