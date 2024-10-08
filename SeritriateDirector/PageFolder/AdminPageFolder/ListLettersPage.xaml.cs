﻿using SeritriateDirector.ClassFolder;
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
using System.Windows.Threading;

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
                LettersIncomingListRusB.IsEnabled = true;
                LettersOutgoingListRusB.IsEnabled = true;
                LettersIncomingListRusB.Margin = new Thickness(1000);
                LettersOutgoingListRusB.Margin = new Thickness(1000);
                ListLettersIncomingRusDg.IsEnabled = true;
                ListLettersOutgoingRusDg.IsEnabled = true;
                LettersIncomingListRusB.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                LettersOutgoingListRusB.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                ListLettersIncomingRusDg.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                ListLettersOutgoingRusDg.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                selectedListIncoming = LettersIncomingListRusB;
                selectedListOutgoing = LettersOutgoingListRusB;
                selectedGridIncoming = ListLettersIncomingRusDg;
                selectedGridOutgoing = ListLettersOutgoingRusDg;

                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.01) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    try
                    {
                        LettersIncomingListRusB.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Входящий")).ToList();
                        LettersOutgoingListRusB.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Исходящий")).ToList();
                        ListLettersIncomingRusDg.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Входящий")).ToList();
                        ListLettersOutgoingRusDg.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Исходящий")).ToList();

                        LettersIncomingListRusB.IsEnabled = false;
                        LettersOutgoingListRusB.IsEnabled = false;
                        ListLettersIncomingRusDg.IsEnabled = false;
                        ListLettersOutgoingRusDg.IsEnabled = false;
                    }
                    catch (Exception ex)
                    {
                        MBClass.ErrorMB(ex, "");
                    }
                };
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List letters";
                SearchLb.Content = "Search";
                TypeLeettersLb.Content = "Type letter";
                AddTb.Text = " Add";
                ExportTb.Text = " Export";
                LettersIncomingListEngB.IsEnabled = true;
                LettersOutgoingListEngB.IsEnabled = true;
                LettersIncomingListEngB.Margin = new Thickness(1000);
                LettersOutgoingListEngB.Margin = new Thickness(1000);
                ListLettersIncomingEngDg.IsEnabled = true;
                ListLettersOutgoingEngDg.IsEnabled = true;
                LettersIncomingListEngB.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                LettersOutgoingListEngB.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                ListLettersIncomingEngDg.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                ListLettersOutgoingEngDg.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                selectedListIncoming = LettersIncomingListEngB;
                selectedListOutgoing = LettersOutgoingListEngB;
                selectedGridIncoming = ListLettersIncomingEngDg;
                selectedGridOutgoing = ListLettersOutgoingEngDg;

                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.01) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    try
                    {
                        LettersIncomingListEngB.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Входящий")).ToList();
                        LettersOutgoingListEngB.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Исходящий")).ToList();
                        ListLettersIncomingEngDg.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Входящий")).ToList();
                        ListLettersOutgoingEngDg.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Исходящий")).ToList();

                        LettersIncomingListEngB.IsEnabled = false;
                        LettersOutgoingListEngB.IsEnabled = false;
                        ListLettersIncomingEngDg.IsEnabled = false;
                        ListLettersOutgoingEngDg.IsEnabled = false;
                    }
                    catch (Exception ex)
                    {
                        MBClass.ErrorMB(ex, "");
                    }
                };
            }
            else
            {
                Title = "Список писем";
                LettersIncomingListRusB.IsEnabled = true;
                LettersOutgoingListRusB.IsEnabled = true;
                LettersIncomingListRusB.Margin = new Thickness(1000);
                LettersOutgoingListRusB.Margin = new Thickness(1000);
                ListLettersIncomingRusDg.IsEnabled = true;
                ListLettersOutgoingRusDg.IsEnabled = true;
                LettersIncomingListRusB.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                LettersOutgoingListRusB.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                ListLettersIncomingRusDg.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                ListLettersOutgoingRusDg.ItemsSource = DBEntities.GetContext()
                                .Letters.ToList().OrderBy(l => l.IdLetters);
                selectedListIncoming = LettersIncomingListRusB;
                selectedListOutgoing = LettersOutgoingListRusB;
                selectedGridIncoming = ListLettersIncomingRusDg;
                selectedGridOutgoing = ListLettersOutgoingRusDg;

                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.01) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    try
                    {
                        LettersIncomingListRusB.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Входящий")).ToList();
                        LettersOutgoingListRusB.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Исходящий")).ToList();
                        ListLettersIncomingRusDg.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Входящий")).ToList();
                        ListLettersOutgoingRusDg.ItemsSource = DataFolder.DBEntities.GetContext().Letters.
                            Where(l => l.TypeLetters.NameTypeLetters.StartsWith("Исходящий")).ToList();

                        LettersIncomingListRusB.IsEnabled = false;
                        LettersOutgoingListRusB.IsEnabled = false;
                        ListLettersIncomingRusDg.IsEnabled = false;
                        ListLettersOutgoingRusDg.IsEnabled = false;
                    }
                    catch (Exception ex)
                    {
                        MBClass.ErrorMB(ex, "");
                    }
                };

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            TypeLettersCb.ItemsSource = DBEntities.GetContext().TypeLetters.ToList();
        }

        private string leng;
        private ListBox selectedListIncoming;
        private ListBox selectedListOutgoing;
        private DataGrid selectedGrid;
        private DataGrid selectedGridIncoming;
        private DataGrid selectedGridOutgoing;
        private bool IncomingEnable = false;
        private bool OutgoingEnable = false;

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IncomingEnable == true)
                {
                    selectedGrid = selectedGridIncoming;
                }
                else if (OutgoingEnable == true)
                {
                    selectedGrid = selectedGridOutgoing;
                }

                if (IncomingEnable == true ||
                    OutgoingEnable == true)
                {
                    selectedGrid.SelectAllCells();
                    int colCount = selectedGrid.SelectedCells.Count;
                    selectedGrid.SelectedIndex = selectedGrid.Items.Count - 1;
                    int a = colCount / (selectedGrid.SelectedIndex + 1);

                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Эксел";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Excel";
                    }
                    else
                    {
                        leng = "Эксел";
                    }

                    string selectedFileName = leng;
                    ExportClass.ToExcelFile(selectedGrid, selectedFileName, a);
                }
                else
                {
                    MBClass.ErrorMB("Выберете тип письма", "");
                    TypeLettersCb.Focus();
                }
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex, "");
            }
        }

        private void TypeLettersCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var timer = new DispatcherTimer
            { Interval = TimeSpan.FromSeconds(0.01) };
            timer.Start();
            timer.Tick += (senders, args) =>
            {
                timer.Stop();
                if (TypeLettersCb.Text == "Входящий")
                {
                    IncomingEnable = true;
                    OutgoingEnable = false;

                    selectedListIncoming.IsEnabled = true;
                    selectedListIncoming.Opacity = 1;
                    selectedListIncoming.Margin = new Thickness(0);
                    selectedListOutgoing.IsEnabled = false;
                    selectedListOutgoing.Opacity = 0;
                    selectedListOutgoing.Margin = new Thickness(1000);
                    selectedListOutgoing.SelectedIndex = -1;
                    selectedGridIncoming.IsEnabled = true;
                    selectedGridOutgoing.IsEnabled = false;
                }
                else if (TypeLettersCb.Text == "Исходящий")
                {
                    OutgoingEnable = true;
                    IncomingEnable = false;

                    selectedListOutgoing.IsEnabled = true;
                    selectedListOutgoing.Opacity = 1;
                    selectedListOutgoing.Margin = new Thickness(0);
                    selectedListIncoming.IsEnabled = false;
                    selectedListIncoming.Opacity = 0;
                    selectedListIncoming.Margin = new Thickness(1000);
                    selectedListIncoming.SelectedIndex = -1;
                    selectedGridOutgoing.IsEnabled = true;
                    selectedGridIncoming.IsEnabled = false;
                }
            };
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void EditLettersMi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteLettersMi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateLettersMi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListLettersPage());
        }
    }
}
