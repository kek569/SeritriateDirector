using SeritriateDirector.ClassFolder;
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

namespace SeritriateDirector.PageFolder.SecretaryPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListLettersPage.xaml
    /// </summary>
    public partial class ListLettersPage : Page
    {
        public ListLettersPage()
        {
            InitializeComponent();

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                Title = "Список писем";
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List letters";
            }
            else
            {
                Title = "Список писем";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!");
            }
        }
    }
}
