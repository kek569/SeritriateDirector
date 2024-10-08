﻿using SeritriateDirector.ClassFolder;
using SeritriateDirector.PageFolder;
using SeritriateDirector.PageFolder.AdminPageFolder;
using SeritriateDirector.WindowFolder.DirectorWindowFolder;
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
            ListStaffBtn.IsChecked = true;

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                AdminLb.Content = "АДМИНИСТРАТОР СИСТЕМЫ";
                ListStaffBtn.Content = "Список сотрудников";
                ListOfLettersBtn.Content = "Список писем";
                ListOfOrdersBtn.Content = "Список приказов";
                ListOfChartsBtn.Content = "Список графиков";
                BugsTb.Text = "  Баги";
                ThemeTb.Text = "  Тема";
                BackTb.Text = "  Назад";
            }
            else if (globalSettingLanguage == "en")
            {
                AdminLb.Content = "SYSTEM ADMINISTRATOR";
                ListStaffBtn.Content = "List staff";
                ListOfLettersBtn.Content = "List letters";
                ListOfOrdersBtn.Content = "List orders";
                ListOfChartsBtn.Content = "List graphics";
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

        private void ListStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            string check = this.MainFrame.Content.ToString();
            check = new string(check.Reverse().ToArray()).Remove(14);
            check = new string(check.Reverse().ToArray());

            if (check == ".ListStaffPage")
            {
                ListStaffBtn.IsChecked = true;
            }
            else
            {
                MainFrame.Navigate(new ListStaffPage());

                if (check == "r.AddStaffPage" ||
                        check == ".EditStaffPage")
                {
                    ListStaffBtn.IsChecked = true;
                }
                else
                {
                    ListOfLettersBtn.IsChecked = false;
                    ListOfOrdersBtn.IsChecked = false;
                    ListOfChartsBtn.IsChecked = false;
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
                ListStaffBtn.IsChecked = false;
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
                ListStaffBtn.IsChecked = false;
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

                if (check == ".AddGraphicsPage" ||
                        check == "EditGraphicsPage")
                {
                    ListOfChartsBtn.IsChecked = true;
                }
                else
                {
                    ListOfLettersBtn.IsChecked = false;
                    ListOfOrdersBtn.IsChecked = false;
                    ListStaffBtn.IsChecked = false;
                }
            }
        }

        private void ThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).MainWindowAdmin = new MainWindowAdmin();

            new ThemeSelectionWindow().Show();
            this.Close();
        }

        private void BugsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SendingBugsPage());
        }
    }
}
