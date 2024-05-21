using Microsoft.Office.Interop.Excel;
using SeritriateDirector.ClassFolder;
using SeritriateDirector.PageFolder;
using SeritriateDirector.PageFolder.DirectorPageFolder;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeritriateDirector.WindowFolder.DirectorWindowFolder
{
    /// <summary>
    /// Логика взаимодействия для MainWindowDirector.xaml
    /// </summary>
    public partial class MainWindowDirector : System.Windows.Window
    {
        public MainWindowDirector()
        {
            string pathDictionary = (App.Current as App).PathDictionary;

            if (pathDictionary != null && pathDictionary != "")
            {
                this.Resources = new ResourceDictionary() { Source = new Uri(pathDictionary) };
            }
            InitializeComponent();

            MainFrame.Navigate(new ListLettersPage());
            ListOfLettersBtn.IsChecked = true;

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                DirectorLb.Content = "ДИРЕКТОР";
                ListOfLettersBtn.Content = "Список писем";
                ListOfOrdersBtn.Content = "Список приказов";
                ListOfChartsBtn.Content = "Список графиков";
                BugsTb.Text = "  Баги";
                ThemeTb.Text = "  Тема";
                BackTb.Text = "  Назад";
            }
            else if (globalSettingLanguage == "en")
            {
                DirectorLb.Content = "DIRECTOR";
                ListOfLettersBtn.Content = "List letters";
                ListOfOrdersBtn.Content = "List orders";
                ListOfChartsBtn.Content = "List charts";
                BugsTb.Text = "  Bugs";
                ThemeTb.Text = "  Theme";
                BackTb.Text = "  Back";
            }
            else
            {
                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
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
            MBClass.ExitMB("");
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

                bool ret = MBClass.QestionMB(leng, "");
                if (ret == true)
                {
                    new AuthorizationWindowNoneCapchaWindow().Show();
                    this.Close();
                }
            }
        }

        private void ListOfLettersBtn_Click(object sender, RoutedEventArgs e)
        {
            string check = this.MainFrame.Content.ToString();
            check = new string(check.Reverse().ToArray()).Remove(15);
            check = new string(check.Reverse().ToArray());

            if (check == "ListLettersPage")
            {
                ListOfLettersBtn.IsChecked = true;
            }
            else
            {
                MainFrame.Navigate(new ListLettersPage());
                ListOfOrdersBtn.IsChecked = false;
                ListOfChartsBtn.IsChecked = false;
            }
        }

        private void ListOfOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            string check = this.MainFrame.Content.ToString();
            check = new string(check.Reverse().ToArray()).Remove(14);
            check = new string(check.Reverse().ToArray());

            if (check == "ListOrdersPage")
            {
                ListOfOrdersBtn.IsChecked = true;
            }
            else
            {
                MainFrame.Navigate(new ListOrdersPage());
                ListOfLettersBtn.IsChecked = false;
                ListOfChartsBtn.IsChecked = false;
            }
        }

        private void ListOfChartsBtn_Click(object sender, RoutedEventArgs e)
        {
            string check = this.MainFrame.Content.ToString();
            check = new string(check.Reverse().ToArray()).Remove(16);
            check = new string(check.Reverse().ToArray());

            if (check == "ListGraphicsPage")
            {
                ListOfChartsBtn.IsChecked = true;
            }
            else
            {
                MainFrame.Navigate(new ListGraphicsPage());
                ListOfLettersBtn.IsChecked = false;
                ListOfOrdersBtn.IsChecked = false;
            }
        }

        private void ThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).MainWindowDirector = new MainWindowDirector();

            new ThemeSelectionWindow().Show();
            this.Close();
        }

        private void BugsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SendingBugsPage());
        }
    }
}

