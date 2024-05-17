using SeritriateDirector.ClassFolder;
using SeritriateDirector.WindowFolder.AdminWindowFolder;
using SeritriateDirector.WindowFolder.DirectorWindowFolder;
using SeritriateDirector.WindowFolder.SecretaryWindowFolder;
using System;
using System.Collections.Generic;
using System.IO;
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
            string selected_pathDictionary = (App.Current as App).PathDictionary;

            if (selected_pathDictionary != null && selected_pathDictionary != "")
            {
                this.Resources = new ResourceDictionary() { Source = new Uri(selected_pathDictionary) };
            }

            InitializeComponent();

            if (selected_pathDictionary == "pack://application:,,,/ResourceFolder/DictionaryDark.xaml")
            {
                ThemeCb.IsChecked = true;
            }

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                LanguageSelectionLb.Content = "ВЫБОР ТЕМЫ";
                SaveSettingsBtn.Content = "  Сохранить настройки  ";
                LightLb.Content = "Светлый";
                DarkLb.Content = "Темный";
            }
            else if (globalSettingLanguage == "en")
            {
                LanguageSelectionLb.Content = "CHOOSING THEME";
                SaveSettingsBtn.Content = "  Save settings  ";
                LightLb.Content = "Light";
                DarkLb.Content = "Dark";
            }
            else
            {
                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }
        }

        private string pathDictionary = (App.Current as App).PathDictionary;
        private string path = (App.Current as App).Path;
        private string leng;

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MBClass.ExitMB("");
        }

        private void ThemeCb_Click(object sender, RoutedEventArgs e)
        {

            if (ThemeCb.IsChecked == true)
            {
                ThemeCb.IsEnabled = false;
                SaveSettingsBtn.IsEnabled = false;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.45) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryDark.xaml") };
                    pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryDark.xaml";
                    ThemeCb.IsEnabled = true;
                    SaveSettingsBtn.IsEnabled = true;
                };
            }
            else if (ThemeCb.IsChecked == false)
            {
                ThemeCb.IsEnabled = false;
                SaveSettingsBtn.IsEnabled = false;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.45) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryLight.xaml") };
                    pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryLight.xaml";
                    ThemeCb.IsEnabled = true;
                    SaveSettingsBtn.IsEnabled = true;
                };
            }
        }

        private void SaveSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;
            (App.Current as App).PathDictionary = pathDictionary;

            using (StreamWriter newTask = new StreamWriter(path + "SaveLanguageSetting.txt", false))
            {
                newTask.WriteLine(globalSettingLanguage + "\n" + pathDictionary);
            }

                var AdminWindow = (App.Current as App).MainWindowAdmin;
                var SecrWindow = (App.Current as App).MainWindowSecretary;
                var DirWindow = (App.Current as App).MainWindowDirector;


            if (AdminWindow != null)
            {
                (App.Current as App).MainWindowAdmin = null;
                new MainWindowAdmin().Show();
                this.Close();
            }
            else if (SecrWindow != null)
            {
                (App.Current as App).MainWindowSecretary = null;
                new MainWindowSecretary().Show();
                this.Close();
            }
            else if (DirWindow != null)
            {
                (App.Current as App).MainWindowDirector = null;
                new MainWindowDirector().Show();
                this.Close();
            }
            else
            {
                if (globalSettingLanguage == "ru")
                {
                    leng = "Ошибка перехода окна";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Window transition error";
                }
                else
                {
                    leng = "Ошибка перехода окна";
                }

                MBClass.ErrorMB(leng, "");
            }
        }
    }
}
