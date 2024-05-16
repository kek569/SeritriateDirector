using SeritriateDirector.ClassFolder;
using SeritriateDirector.WindowFolder.DirectorWindowFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
using System.Windows.Threading;

namespace SeritriateDirector.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для ThemeSelectionWindow.xaml
    /// </summary>
    public partial class ThemeSelectionWindow : Window
    {
        public ThemeSelectionWindow()
        {
            string pathDictionary = (App.Current as App).PathDictionary;

            if (pathDictionary != null && pathDictionary != "")
            {
                this.Resources = new ResourceDictionary() { Source = new Uri(pathDictionary) };
                if (pathDictionary == "pack://application:,,,/ResourceFolder/DictionaryDark.xaml")
                {
                    ThemeCb.IsChecked = true;
                }
            }

            InitializeComponent();

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                LanguageSelectionLb.Content = "ВЫБОР ТЕМЫ";
                SaveSettingsBtn.Content = "Сохранить настройки";
                LightLb.Content = "Светлый";
                DarkLb.Content = "Темный";
            }
            else if (globalSettingLanguage == "en")
            {
                LanguageSelectionLb.Content = "CHOOSING THEME";
                SaveSettingsBtn.Content = "Save settings";
                LightLb.Content = "Light";
                DarkLb.Content = "Dark";
            }
            else
            {
                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!");
            }
        }

        private string pathDictionary;

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool ret = MBClass.QestionMB("Вы действительно желаете выйти?");
            if (ret == true)
            {
                this.Close();
            }
        }

        private void ThemeCb_Click(object sender, RoutedEventArgs e)
        {

            if (ThemeCb.IsChecked == true)
            {
                ThemeCb.IsEnabled = false;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.45) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryDark.xaml") };
                    pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryDark.xaml";
                    ThemeCb.IsEnabled = true;
                };
            }
            else if (ThemeCb.IsChecked == false)
            {
                ThemeCb.IsEnabled = false;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.45) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryLight.xaml") };
                    pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryLight.xaml";
                    ThemeCb.IsEnabled = true;
                };
            }
        }

        private void SaveSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).PathDictionary = pathDictionary;

            var a = new MainWindowDirector();
            a.Show();

            this.Close();
        }
    }
}
