using SeritriateDirector.Properties;
using SeritriateDirector.WindowFolder.AdminWindowFolder;
using SeritriateDirector.WindowFolder.DirectorWindowFolder;
using SeritriateDirector.WindowFolder.SecretaryWindowFolder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SeritriateDirector
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string GlobalSettingLanguage { get; set; }

        public string Path { get; set; }

        public string ColumsExcel { get; set; }

        public string DeptName { get; set; }

        public string PathDictionary { get; set; }

        public MainWindowDirector MainWindowDirector { get; set; }

        public MainWindowAdmin MainWindowAdmin { get; set; }

        public MainWindowSecretary MainWindowSecretary { get; set; }

        public string AddLoginName { get; set; }
    }
}
