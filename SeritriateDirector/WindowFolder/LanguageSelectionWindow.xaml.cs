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

namespace SeritriateDirector.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для LanguageSelectionWindow.xaml
    /// </summary>
    public partial class LanguageSelectionWindow : Window
    {
        public LanguageSelectionWindow()
        {
            InitializeComponent();
        }

        private bool SaveFileNull = true;
        private bool capcha = false;
        private string path;

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

        private void ImageMouseMoveRussia(object sender, MouseEventArgs e)
        {
            LanguageSelectionLb.Content = "ВЫБОР ЯЗЫКА";
        }

        private void ImageMouseMoveEnglish(object sender, MouseEventArgs e)
        {
            LanguageSelectionLb.Content = "LANGUAGE SELECTION";
        }

        private void ImageMouseLeftButtonDownRussia(object sender, MouseButtonEventArgs e)
        {
            (App.Current as App).GlobalSettingLanguage = "ru";

            using (StreamWriter newTask = new StreamWriter(path + "SaveLanguageSetting.txt", false))
            {
                newTask.WriteLine("ru");
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

            using (StreamWriter newTask = new StreamWriter(path + "SaveLanguageSetting.txt", false))
            {
                newTask.WriteLine("en");
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
                    (App.Current as App).GlobalSettingLanguage = newTask.ReadLine();
                    new AuthorizationWindowNoneCapchaWindow().Show();
                    this.Close();
                }
            }
        }
            
    }
}
