using SeritriateDirector.ClassFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace SeritriateDirector.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для LanguageSelectionWindow.xaml
    /// </summary>
    public partial class LanguageSelectionWindow : Window
    {
        public LanguageSelectionWindow()
        {
            string selected_pathDictionary = (App.Current as App).PathDictionary;

            if (selected_pathDictionary != null && selected_pathDictionary != "")
            {
                this.Resources = new ResourceDictionary() { Source = new Uri(selected_pathDictionary) };
                pathDictionary = selected_pathDictionary;
                isChecked = true;
            }
            else
            {
                this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryLight.xaml") };
            }

            InitializeComponent();


            if (selected_pathDictionary == "pack://application:,,,/ResourceFolder/DictionaryDark.xaml" &&
                isChecked == true)
            {
                ThemeCb.IsChecked = true;
            }
        }

        private bool SaveFileNull = true;
        private bool capcha = false;
        private string path;
        private string pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryLight.xaml";
        private bool isChecked = false;

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ImageMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool ret = MBClass.QestionMB("Вы действительно желаете выйти?", "");
            if (ret == true)
            {
                this.Close();
            }
        }

        private void ImageMouseMoveRussia(object sender, MouseEventArgs e)
        {
            LanguageSelectionLb.Content = "НАСТРОЙКИ";
            ChoosingThemeLb.Content = "Выбор темы";
            LightLb.Content = "Светлый";
            DarkLb.Content = "Темный";
        }

        private void ImageMouseMoveEnglish(object sender, MouseEventArgs e)
        {
            LanguageSelectionLb.Content = "SETTING";
            ChoosingThemeLb.Content = "Choosing theme";
            LightLb.Content = "Light";
            DarkLb.Content = "Dark";
        }

        private void ImageMouseLeftButtonDownRussia(object sender, MouseButtonEventArgs e)
        {
            (App.Current as App).GlobalSettingLanguage = "ru";
            (App.Current as App).PathDictionary = pathDictionary;

            using (StreamWriter newTask = new StreamWriter(path + "SaveLanguageSetting.txt", false))
            {
                newTask.WriteLine("ru\n" + pathDictionary);
            }

            if (capcha == false)
            {
                new AuthorizationWindowNoneCapchaWindow().Show();
                this.Close();
            }
            else
            {
                new AuthorizationWindowCapchaWindow().Show();
                this.Close();
            }
        }

        private void ImageMouseLeftButtonDownEnglish(object sender, MouseButtonEventArgs e)
        {
            (App.Current as App).GlobalSettingLanguage = "en";
            (App.Current as App).PathDictionary = pathDictionary;

            using (StreamWriter newTask = new StreamWriter(path + "SaveLanguageSetting.txt", false))
            {
                newTask.WriteLine("en\n" + pathDictionary);
            }

            

            if (capcha == false)
            {
                new AuthorizationWindowNoneCapchaWindow().Show();
                this.Close();
            }
            else
            {
                new AuthorizationWindowCapchaWindow().Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            path = Directory.GetCurrentDirectory();
            path = path.Remove(path.Length - 9);
            path = path + "SaveFolder\\";
            string pathDir = path;

            (App.Current as App).Path = path;

            using (StreamReader newTask = new StreamReader(path + "SaveLanguageSetting.txt", false))
            {
                if (newTask == null || newTask.ReadLine() == "")
                {
                    SaveFileNull = true;

                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;
                    if (globalSettingLanguage == "1")
                    {
                        capcha = true;
                    }
                }
                else
                {
                    SaveFileNull = false;
                }
            }
            if (SaveFileNull == false)
            {
                using (StreamReader newTask = new StreamReader(path + "SaveLanguageSetting.txt", false))
                {
                    (App.Current as App).PathDictionary = System.IO.File.ReadLines(path + "SaveLanguageSetting.txt").Skip(1).First();

                    (App.Current as App).GlobalSettingLanguage = newTask.ReadLine();
                    new AuthorizationWindowNoneCapchaWindow().Show();
                    this.Close();
                }
            }
        }

        private void ThemeCb_Click(object sender, RoutedEventArgs e)
        {

            if (ThemeCb.IsChecked == true)
            {
                ThemeCb.IsEnabled = false;
                RussiaLanguageIm.IsEnabled = false;
                EnglishLanguageIm.IsEnabled = false;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.45) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryDark.xaml") };
                    pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryDark.xaml";
                    ThemeCb.IsEnabled = true;
                    RussiaLanguageIm.IsEnabled = true;
                    EnglishLanguageIm.IsEnabled = true;
                };
            }
            else if (ThemeCb.IsChecked == false)
            {
                ThemeCb.IsEnabled = false;
                RussiaLanguageIm.IsEnabled = false;
                EnglishLanguageIm.IsEnabled = false;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.45) };
                timer.Start();
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/ResourceFolder/DictionaryLight.xaml") };
                    pathDictionary = "pack://application:,,,/ResourceFolder/DictionaryLight.xaml";
                    ThemeCb.IsEnabled = true;
                    RussiaLanguageIm.IsEnabled = true;
                    EnglishLanguageIm.IsEnabled = true;
                };
            }
        }
    }
}
