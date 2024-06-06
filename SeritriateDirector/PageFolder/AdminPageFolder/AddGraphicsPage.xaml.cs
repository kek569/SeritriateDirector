using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
using Microsoft.Win32;
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

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddGraphicsPage.xaml
    /// </summary>
    public partial class AddGraphicsPage : Page
    {
        public AddGraphicsPage()
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
                Title = "добавление графиков";
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "Add graphics";
            }
            else
            {
                Title = "добавление графиков";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            DirectorCb.ItemsSource = DBEntities.GetContext().Staff.ToList();
            TimeEventsTb.MaxLength = 5;
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        Graphics graphics = new Graphics();
        string selectedFileName = "";
        private string TimeSearch = "";
        private bool timeSearch = false;

        private void AddPhoto()
        {
            try
            {

                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = "";
                op.Filter = "All support graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*jpeg)|*.jpg;*jpeg|" +
                    "Portable Network Graphic (*.png|*.png";

                if (op.ShowDialog() == true)
                {
                    selectedFileName = op.FileName;
                    graphics.PhotoEvents = ClassImage.ConvertImageToArray(selectedFileName);
                    PhotoIM.Source = ClassImage.ConvertByteArrayToImage(graphics.PhotoEvents);


                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TimeEvents_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TimeEventsTb.Text.Length == 3)
            {
                TimeEventsTb.SelectionStart = 3;
            }
        }

        private void TimeEvents_PreviewKeyDown(object sender, KeyEventArgs e)
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
            else if (e.Key == Key.Back && TimeEventsTb.Text.Length == 3)
            {
                e.Handled = true;
                TimeEventsTb.SelectionLength = 2;
                TimeEventsTb.Text = TimeEventsTb.Text.Remove(1, 1);
                TimeEventsTb.SelectionStart = 1;
                TimeSearch = "";
            }
        }

        private void TimeEvents_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TimeEventsTb.Text.Length == 1)
            {
                Regex regex = new Regex("[^0-2]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimeEventsTb.Text.Length == 2)
            {
                if (TimeEventsTb.Text != "2:")
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
            else if (TimeEventsTb.Text.Length == 3)
            {
                Regex regex = new Regex("[^0-5]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimeEventsTb.Text.Length <= 4)
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void TimeReturnSp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TimeEventsTb.Text.Length == 1)
            {

                TimeEventsTb.SelectionStart = 0;
                TimeEventsTb.Focus();
            }
            else if (TimeEventsTb.Text.Length == 2)
            {

                TimeEventsTb.SelectionStart = 1;
                TimeEventsTb.Focus();
            }
            else
            {

                TimeEventsTb.SelectionStart = TimeEventsTb.Text.Length;
                TimeEventsTb.Focus();
            }
        }
    }
}
