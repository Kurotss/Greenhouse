using Greenhouse.Classes.WindowModels;
using System;
using System.Windows;

namespace Greenhouse
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OpenGroupsAndTypesCulture(object sender, RoutedEventArgs e)
		{
			var win = new WorkWindow
			{
				Title = "Группы и виды культур",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/GroupsAndTypesCulture.xaml") },
				DataContext = new GroupsAndTypesCultureModel()
			};
			win.Show();
		}

		private void OpenTypesGreenhouse(object sender, RoutedEventArgs e)
		{
			var win = new WorkWindow
			{
				Title = "Виды оранжерей",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/TypesGreenhouse.xaml") },
				DataContext = new TypesGreenhouseModel()
			};
			win.Show();
		}

		private void OpenTypesPlant(object sender, RoutedEventArgs e)
		{
			var win = new WorkWindow
			{
				Title = "Виды растений",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/TypesPlant.xaml") },
				DataContext = new TypesPlantModel()
			};
			win.Show();
		}

		private void OpenGreenhouses(object sender, RoutedEventArgs e)
		{
			var win = new WorkWindow
			{
				Title = "Оранжереи",
				Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceDictionaries/Greenhouses.xaml") },
				DataContext = new GreenhousesModel()
			};
			win.Show();
		}
	}
}
