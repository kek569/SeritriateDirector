using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Xamarin.Forms.Internals;
using static System.Net.Mime.MediaTypeNames;

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListGraphicsPage.xaml
    /// </summary>
    public partial class ListGraphicsPage : System.Windows.Controls.Page
    {
        public ListGraphicsPage()
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
                Title = "Список графиков";
                SearchLb.Content = "Поиск";
                AddTb.Text = " Добавить";
                ExportTb.Text = " Экспорт";
                GraphicsListRusB.IsEnabled = true;
                GraphicsListRusB.Opacity = 1;
                ListGraphicsRusDg.IsEnabled = true;
                GraphicsListRusB.ItemsSource = DBEntities.GetContext()
                        .Graphics.ToList().OrderBy(s => s.IdGraphics);
                ListGraphicsRusDg.ItemsSource = DBEntities.GetContext()
                        .Graphics.ToList().OrderBy(s => s.IdGraphics);
                selectedList = GraphicsListRusB;
                selectedGrid = ListGraphicsRusDg;
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List graphics";
                SearchLb.Content = "Search";
                AddTb.Text = " Add";
                ExportTb.Text = " Export";
                GraphicsListEngB.IsEnabled = true;
                GraphicsListEngB.Opacity = 1;
                ListGraphicsEngDg.IsEnabled = true;
                GraphicsListEngB.ItemsSource = DBEntities.GetContext()
                        .Graphics.ToList().OrderBy(s => s.IdGraphics);
                ListGraphicsEngDg.ItemsSource = DBEntities.GetContext()
                        .Graphics.ToList().OrderBy(s => s.IdGraphics);
                selectedList = GraphicsListEngB;
                selectedGrid = ListGraphicsEngDg;
            }
            else
            {
                Title = "Список графиков";
                GraphicsListRusB.IsEnabled = true;
                GraphicsListRusB.Opacity = 1;
                ListGraphicsRusDg.IsEnabled = true;
                GraphicsListRusB.ItemsSource = DBEntities.GetContext()
                        .Graphics.ToList().OrderBy(s => s.IdGraphics);
                ListGraphicsRusDg.ItemsSource = DBEntities.GetContext()
                        .Graphics.ToList().OrderBy(s => s.IdGraphics);
                selectedList = GraphicsListRusB;
                selectedGrid = ListGraphicsRusDg;

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            TimePickerTb.MaxLength = 5;
        }

        private string leng;
        private ListBox selectedList;
        private DataGrid selectedGrid;
        private string TextSearch = "";
        private string DateSearch = "";
        private string TimeSearch = "";
        private bool timeSearch = false;

        private void EditGraphicsMi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGraphicsMi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateGraphicsMi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListGraphicsPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextSearch = SearchTb.Text;
            Search();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddGraphicsPage());
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex, "");
            }
        }

        private void TimePickerTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TimePickerTb.Text.Length == 3)
            {
                TimePickerTb.SelectionStart = 3;
            }
            else if (TimePickerTb.Text.Length == 5)
            {
                timeSearch = true;
                TimeSearch = TimePickerTb.Text;
                Search();
            }

            if (TimePickerTb.Text.Length != 5 && 
                timeSearch == true)
            {
                timeSearch = false;
                TimeSearch = "";
                Search();
            }
        }

        private void TimePickerTb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete ||
                e.Key == Key.Space || e.Key == Key.LeftShift ||
                e.Key == Key.LeftAlt || e.Key == Key.LeftCtrl ||
                e.Key == Key.Tab || e.Key == Key.RightCtrl ||
                e.Key == Key.RightShift || e.Key == Key.Right ||
                e.Key == Key.Left || e.Key == Key.Up ||
                e.Key == Key.Down)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Back && TimePickerTb.Text.Length == 3)
            {
                e.Handled = true;
                TimePickerTb.SelectionLength = 2;
                TimePickerTb.Text = TimePickerTb.Text.Remove(1, 1);
                TimePickerTb.SelectionStart = 1;
                TimeSearch = "";
            }
        }

        private void SelectionSp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
             if (TimePickerTb.Text.Length == 1)
             {
                
                TimePickerTb.SelectionStart = 0;
                TimePickerTb.Focus();
             }
             else if (TimePickerTb.Text.Length == 2)
             {
                
                TimePickerTb.SelectionStart = 1;
                TimePickerTb.Focus();
             }
             else
             {
                
                TimePickerTb.SelectionStart = TimePickerTb.Text.Length;
                TimePickerTb.Focus();
             }
        }

        private void TimePickerTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TimePickerTb.Text.Length == 1)
            {
                Regex regex = new Regex("[^0-2]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimePickerTb.Text.Length == 2)
            {
                if (TimePickerTb.Text != "2:")
                {
                    Regex regex = new Regex("[^0-9]+");
                    e.Handled = regex.IsMatch(e.Text);
                }
                else
                {
                    Regex regex = new Regex("[^0-4]+");
                    e.Handled = regex.IsMatch(e.Text);
                }
            }
            else if (TimePickerTb.Text.Length == 3)
            {
                Regex regex = new Regex("[^0-5]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimePickerTb.Text.Length <= 4)
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void Search()
        {
            try
            {
                selectedList.ItemsSource = DataFolder.DBEntities.GetContext().Graphics.
                    Where(g => (g.NameEvents.StartsWith(TextSearch) ||
                    g.PlaceEvents.StartsWith(TextSearch) ||
                    g.TargetEvents.StartsWith(TextSearch)) &&
                    g.DateEvents.ToString().StartsWith(DateSearch) &&
                    g.TimeEvents.ToString().StartsWith(TimeSearch)).ToList();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex, "");
            }
        }

        private void DateEventDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateSearch = new string(DateEventDp.Text.Reverse().ToArray());

            string[] split = DateSearch.Split('.');
            DateSearch = "";
            foreach (string s in split)
            {
                if (DateSearch != "")
                {
                    string a = new string(s.Reverse().ToArray());
                    DateSearch = DateSearch + "-" + a;
                }
                else
                {
                    string a = new string(s.Reverse().ToArray());
                    DateSearch = a;
                }
            }

            Search();
        }
    }
}
