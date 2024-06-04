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

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListOrdersPage.xaml
    /// </summary>
    public partial class ListOrdersPage : Page
    {
        public ListOrdersPage()
        {
            InitializeComponent();

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                Title = "Список приказов";
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List orders";
            }
            else
            {
                Title = "Список приказов";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }
        }
    }
}
