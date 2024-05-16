using SeritriateDirector.ClassFolder;
using SeritriateDirector.PageFolder.AdminPageFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace SeritriateDirector.WindowFolder.AdminWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MainWindowAdmin.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
        public MainWindowAdmin()
        {
            string pathDictionary = (App.Current as App).PathDictionary;

            if (pathDictionary != null && pathDictionary != "")
            {
                this.Resources = new ResourceDictionary() { Source = new Uri(pathDictionary) };
            }
            InitializeComponent();

            MainFrame.Navigate(new ListStaffPage());

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                AdminLb.Content = "АДМИНИСТРАТОР СИСТЕМЫ";
                ListStaffBtn.Content = "Список сотрудников";
                ListOfLettersBtn.Content = "Список писем";
                ListOfOrdersBtn.Content = "Список приказов";
                ListOfChartsBtn.Content = "Список графиков";
                BackBtn.Content = "Назад";
            }
            else if (globalSettingLanguage == "en")
            {
                AdminLb.Content = "SYSTEM ADMINISTRATOR";
                ListStaffBtn.Content = "List staff";
                ListOfLettersBtn.Content = "List letters";
                ListOfOrdersBtn.Content = "List orders";
                ListOfChartsBtn.Content = "List charts";
                BackBtn.Content = "Back";
            }
            else
            {
                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!");
            }
        }

        private string leng;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Вы действительно желаете выйти?";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Do you really want to go out?";
            }
            else
            {
                leng = "Вы действительно желаете выйти?";
            }

            bool ret = MBClass.QestionMB(leng);
            if (ret == true)
            {
                this.Close();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            if (this.MainFrame.CanGoBack)
            {
                /*if (this.MainFrame.Navigate(new ListStaffPage()))
                {
                    MainFrame.Navigate(new ListVeterinaryEquipmentPage());
                }
                else
                {*/
                this.MainFrame.GoBack();
                //}
            }
            else
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Вы действительно желаете выйти в окно авторизации?";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Are you sure you want to exit the login window?";
                }
                else
                {
                    leng = "Вы действительно желаете выйти в окно авторизации?";
                }

                bool ret = MBClass.QestionMB("Вы действительно желаете " +
                            "выйти в окно авторизации?");
                if (ret == true)
                {
                    new AuthorizationWindowNoneCapchaWindow().Show();
                    this.Close();
                }
            }
        }

        private void ListStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ListStaffPage());
        }

        private void ListOfLettersBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListOfOrdersBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListOfChartsBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
