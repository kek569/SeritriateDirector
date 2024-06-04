using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для ListStaffPage.xaml
    /// </summary>
    public partial class ListStaffPage : Page
    {
        public ListStaffPage()
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
                Title = "Список сотрудников";
                SearchLb.Content = "Поиск";
                AddTb.Text = " Добавить";
                ExportTb.Text = " Экспорт";
                StaffListRusB.IsEnabled = true;
                StaffListRusB.Opacity = 1;
                StaffListRusB.IsEnabled = true;
                StaffListRusB.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.IdStaff);
                ListStaffRusDg.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.IdStaff);
                selectedList = StaffListRusB;
                selectedGrid = ListStaffRusDg;
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "List staff";
                SearchLb.Content = "Search";
                AddTb.Text = " Add";
                ExportTb.Text = " Export";
                StaffListEngB.IsEnabled = true;
                StaffListEngB.Opacity = 1;
                ListStaffEngDg.IsEnabled = true;
                StaffListEngB.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.IdStaff);
                ListStaffEngDg.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.IdStaff);
                selectedList = StaffListEngB;
                selectedGrid = ListStaffEngDg;
            }
            else
            {
                Title = "Список сотрудников";
                StaffListRusB.IsEnabled = true;
                StaffListRusB.Opacity = 1;
                ListStaffRusDg.IsEnabled = true;
                StaffListRusB.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.IdStaff);
                ListStaffRusDg.ItemsSource = DBEntities.GetContext()
                        .Staff.ToList().OrderBy(s => s.IdStaff);
                selectedList = StaffListRusB;
                selectedGrid = ListStaffRusDg;

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            

            

            UserListB.ItemsSource = DBEntities.GetContext()
                .User.ToList().OrderBy(u => u.IdUser);
        }

        Staff staff = new Staff();
        private string leng;
        private ListBox selectedList;
        private DataGrid selectedGrid;

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddStaffPage());
        }

        private void EditStaffMi_Click(object sender, RoutedEventArgs e)
        {
            if (selectedList.SelectedItem == null)
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Выберите строку для редактирование";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Select line to edit";
                }
                else
                {
                    leng = "Выберите строку для редактирование";
                }

                MBClass.ErrorMB(leng, "");
            }
            else
            {

                Staff staff = selectedList.SelectedItem as Staff;
                NavigationService.Navigate
                    (new EditStaffPage(selectedList.SelectedItem as Staff));
            }
        }

        private void DeleteStafffMi_Click(object sender, RoutedEventArgs e)
        {
            if (selectedList.SelectedItem == null)
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Выберите строку для удаления";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Select line to delete";
                }
                else
                {
                    leng = "Выберите строку для удаления";
                }

                MBClass.ErrorMB(leng, "");
            }
            else
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Вы действительно хотите удалить данную строку?";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Are you sure you want to delete this line?";
                }
                else
                {
                    leng = "Вы действительно хотите удалить данную строку?";
                }

                bool ret = MBClass.QestionMB(leng, "");
                if (ret == true)
                {
                    Staff staff = selectedList.SelectedItem as Staff;
                    User user = UserListB.SelectedItem as User;

                    staff = DBEntities.GetContext().Staff
                                .FirstOrDefault(s => s.IdStaff == staff.IdStaff);
                    staff.User.LoginUser = "ㅤ";
                    staff.User.PasswordUser = "ㅤ";

                    user = DBEntities.GetContext().User
                                .FirstOrDefault(u => u.LoginUser == staff.User.LoginUser);

                    DBEntities.GetContext().Staff.Remove(staff);
                    DBEntities.GetContext().SaveChanges();

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Данные успешно были удалены!";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "The data was successfully deleted!";
                    }
                    else
                    {
                        leng = "Данные успешно были удалены!";
                    }

                    MBClass.InfoMB(leng, "");
                    NavigationService.Navigate(new ListStaffPage());
                }
            }
        }

        private void UpdateStaffMi_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListStaffPage());
        }

        private void ExportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedGrid.SelectAllCells();
                int colCount = selectedGrid.SelectedCells.Count;
                selectedGrid.SelectedIndex = selectedGrid.Items.Count - 1;
                int a = colCount / (selectedGrid.SelectedIndex + 1);

                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Эксел";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Excel";
                }
                else
                {
                    leng = "Эксел";
                }

                string selectedFileName = leng;
                ExportClass.ToExcelFile(selectedGrid, selectedFileName, a);
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex, "");
            }
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                selectedList.ItemsSource = DataFolder.DBEntities.GetContext().Staff.
                    Where(s => s.FirstNameStaff.StartsWith(SearchTb.Text) ||
                    s.SurNameStaff.StartsWith(SearchTb.Text) ||
                    s.MiddleNameStaff.StartsWith(SearchTb.Text) ||
                    s.NumberPhoneStaff.StartsWith(SearchTb.Text) ||
                    s.Gender.NameGender.StartsWith(SearchTb.Text)).ToList();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMB(ex, "");
            }
        }
    }
}
