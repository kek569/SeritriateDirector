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

namespace SeritriateDirector.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListGraphicsPage.xaml
    /// </summary>
    public partial class ListGraphicsPage : Page
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
        }
    }
}
