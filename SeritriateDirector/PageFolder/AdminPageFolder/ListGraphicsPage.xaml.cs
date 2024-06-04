using Microsoft.Office.Interop.Excel;
using SeritriateDirector.ClassFolder;
using System;
using System.Collections.Generic;
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

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListGraphicsPage.xaml
    /// </summary>
    public partial class ListGraphicsPage : System.Windows.Controls.Page
    {
        public ListGraphicsPage()
        {
            InitializeComponent();

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                Title = "Список графиков";
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List charts";
            }
            else
            {
                Title = "Список графиков";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            TimePickerTb.MaxLength = 5;
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TimePickerTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TimePickerTb.Text.Length == 3)
            {
                TimePickerTb.SelectionStart = 3;
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
                Regex regex = new Regex("[^1-2]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimePickerTb.Text.Length == 2)
            {
                Regex regex = new Regex("[^1-4]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimePickerTb.Text.Length == 3)
            {
                Regex regex = new Regex("[^0-6]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimePickerTb.Text.Length <= 4)
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }
    }
}
