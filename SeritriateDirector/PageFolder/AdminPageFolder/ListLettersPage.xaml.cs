using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
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

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListLettersPage.xaml
    /// </summary>
    public partial class ListLettersPage : Page
    {
        public ListLettersPage()
        {
            string pathDictionary = (App.Current as App).PathDictionary;

            if (pathDictionary != null && pathDictionary != "")
            {
                this.Resources = new ResourceDictionary() { Source = new Uri(pathDictionary) };
            }
            InitializeComponent();

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                Title = "Список писем";
                SearchLb.Content = "Поиск";
                TypeLeettersLb.Content = "Тип письма";
                AddTb.Text = " Добавить";
                ExportTb.Text = " Экспорт";
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List letters";
                SearchLb.Content = "Search";
                TypeLeettersLb.Content = "Type letter";
                AddTb.Text = " Add";
                ExportTb.Text = " Export";
            }
            else
            {
                Title = "Список писем";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            TypeLettersCb.ItemsSource = DBEntities.GetContext().TypeLetters.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TypeLettersCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
