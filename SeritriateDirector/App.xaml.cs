using SeritriateDirector.Properties;
using SeritriateDirector.WindowFolder.DirectorWindowFolder;
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
        private object sss1;

        public string GlobalSettingLanguage { get; set; }

        public string Path { get; set; }

        public string ColumsExcel { get; set; }

        public string DeptName { get; set; }

        public string PathDictionary { get; set; }

        public MainWindowDirector MainWindowDirector { get; internal set; }
    }
}
