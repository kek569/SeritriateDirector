using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
using Microsoft.Win32;
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
using System.Data.Entity.Validation;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditStaffPage.xaml
    /// </summary>
    public partial class EditStaffPage : Page
    {
        Staff staff = new Staff();
        Staff staffLogin = new Staff();

        public EditStaffPage(Staff staff)
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
                FirstNameLb.Content = "Фамилия";
                SurNameLb.Content = "Имя";
                MiddleNameLb.Content = "Отчество";
                PassportLb.Content = "Серия и № паспорта";
                GenderLb.Content = "Пол";
                JobTitleLb.Content = "Должность";
                SecretaryLb.Content = "Секретарь";
                DateOfBirthLb.Content = "День рождения";
                AdressLb.Content = "Адрес";
                NumberPhoneLb.Content = "Номер телефона";
                PasswordLb.Content = "Пароль";
                RoleLb.Content = "Роль";
                ResetPasswordTb.Text = "  Сбросить пароль";
                LoadPhotoTb.Text = " Загрузить фото";
                SaveTb.Text = " Сохранить изменения";
                Title = "Редактирования сотрудника";
            }
            else if (globalSettingLanguage == "en")
            {
                FirstNameLb.Content = "First name";
                SurNameLb.Content = "Sur name";
                MiddleNameLb.Content = "Middle name";
                PassportLb.Content = "Series and № passport";
                GenderLb.Content = "Gender";
                JobTitleLb.Content = "Job title";
                SecretaryLb.Content = "Secretary";
                DateOfBirthLb.Content = "Birthday";
                AdressLb.Content = "Address";
                NumberPhoneLb.Content = "Number phone";
                PasswordLb.Content = "Password";
                RoleLb.Content = "Role";
                ResetPasswordTb.Text = "  Reset password";
                LoadPhotoTb.Text = " Load photo";
                SaveTb.Text = " Save changes";
                Title = "Editing staff";
            }
            else
            {
                Title = "Редактирования сотрудника";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            DataContext = staff;
            this.staff.IdStaff = staff.IdStaff;

            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
            JobTitleCb.ItemsSource = DBEntities.GetContext().JobTitle.ToList();
            SecretaryCb.ItemsSource = DBEntities.GetContext().Staff.ToList();
            RoleCb.ItemsSource = DBEntities.GetContext().Role.ToList();
            NumberPassportTb.MaxLength = 4;
            SeriesPassportTb.MaxLength = 6;
            NumberPhoneStaffTb.MaxLength = 28;

            PasswordPb.PasswordChar = '*';

            SecretaryCb.ItemsSource = DataFolder.DBEntities.GetContext().Staff.
                        Where(s => s.User.Role.NameRole.StartsWith("Секретарь")).ToList();

            string deptName = (App.Current as App).DeptName;
            staffLogin = DBEntities.GetContext()
                        .Staff
                        .FirstOrDefault(o => o.User.LoginUser == staff.User.LoginUser);

            var timer = new DispatcherTimer
            { Interval = TimeSpan.FromSeconds(0.01) };
            timer.Start();
            timer.Tick += (senders, args) =>
            {
                timer.Stop();
                RoleCb.SelectedValue = staffLogin.User.Role.IdRole;
                if (staffLogin.Director1 != null)
                {
                    SecretaryCb.SelectedValue = staffLogin.Director1.Secretary.Staff.IdStaff;
                }
                DateOfBirthStaffDp.Text = staffLogin.DateOfBirthStaff.ToString();
                NumberPhoneStaffTb.Text = NumberPhoneStaffTb.Text + "          ";
            };

            if (staffLogin.User.LoginUser != deptName)
            {
                PasswordLb.IsEnabled = false;
                PasswordLb.Opacity = 0;
                PasswordLb.Margin = new Thickness(1000);
                PasswordPb.IsEnabled = false;
                PasswordPb.Opacity = 0;
                PasswordPb.Margin = new Thickness(1000);
                PasswordTb.IsEnabled = false;
                PasswordTb.Opacity = 0;
                PasswordTb.Margin = new Thickness(1000);
                CloseEyesIm.IsEnabled = false;
                CloseEyesIm.Opacity = 0;
                CloseEyesIm.Margin = new Thickness(1000);
                OpenEyesIm.IsEnabled = false;
                OpenEyesIm.Opacity = 0;
                OpenEyesIm.Margin = new Thickness(1000);
                EditedPassword = false;

                ResetPasswordBtn.IsEnabled = true;
                ResetPasswordBtn.Opacity = 1;
            }
            else
            {
                var timers = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(0.01) };
                timers.Start();
                timers.Tick += (senders, args) =>
                {
                    timers.Stop();
                    string Password = staffLogin.User.PasswordUser;
                    TextPassword = Password;
                    PasswordPb.Password = Password;
                    PasswordTb.Text = Password;
                };
            }
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        string selectedFileName = "";
        private string leng;
        private string TextPassword;
        private string MiddleName;
        private string MiddleNameFull;
        private int Check = 0;
        private int Rem = 0;
        private bool EditedPassword = true;

        private void AddPhoto()
        {
            try
            {

                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = "";
                op.Filter = "All support graphics|*.jpg;*.jpeg;*.png|" +
                    "JPEG (*.jpg;*jpeg)|*.jpg;*jpeg|" +
                    "Portable Network Graphic (*.png|*.png";

                if (op.ShowDialog() == true)
                {
                    selectedFileName = op.FileName;
                    staff.PhotoStaff = ClassImage.ConvertImageToArray(selectedFileName);
                    PhotoIM.Source = ClassImage.ConvertByteArrayToImage(staff.PhotoStaff);


                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LastNameStaffTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите имя";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter first name";
                    }
                    else
                    {
                        leng = "Введите имя";
                    }

                    MBClass.ErrorMB(leng, "");
                    LastNameStaffTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(FirstNameStaffTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите фамилию";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter last name";
                    }
                    else
                    {
                        leng = "Введите фамилию";
                    }

                    MBClass.ErrorMB(leng, "");
                    FirstNameStaffTb.Focus();
                }
                else if (NumberPhoneStaffTb.Text.Length != 28)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите номер телефона";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter number phone";
                    }
                    else
                    {
                        leng = "Введите номер телефона";
                    }

                    MBClass.ErrorMB(leng, "");
                    NumberPhoneStaffTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(DateOfBirthStaffDp.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите дату рождения";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter date of birth";
                    }
                    else
                    {
                        leng = "Введите дату рождения";
                    }

                    MBClass.ErrorMB(leng, "");
                    DateOfBirthStaffDp.Focus();
                }
                else if (string.IsNullOrWhiteSpace(AdressStaffTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите адрес";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter address";
                    }
                    else
                    {
                        leng = "Введите адрес";
                    }

                    MBClass.ErrorMB(leng, "");
                    AdressStaffTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(TextPassword) && 
                    EditedPassword == true)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите пароль";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter password";
                    }
                    else
                    {
                        leng = "Введите пароль";
                    }

                    MBClass.ErrorMB(leng, "");
                    PasswordTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(NumberPassportTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите серию паспорта";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter passport series";
                    }
                    else
                    {
                        leng = "Введите серию паспорта";
                    }

                    MBClass.ErrorMB(leng, "");
                    NumberPassportTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(SeriesPassportTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите номер паспорта";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter passport number";
                    }
                    else
                    {
                        leng = "Введите номер паспорта";
                    }

                    MBClass.ErrorMB(leng, "");
                    SeriesPassportTb.Focus();
                }
                else if (GenderCb.SelectedIndex <= -1)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите пол";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter gender";
                    }
                    else
                    {
                        leng = "Введите пол";
                    }

                    MBClass.ErrorMB(leng, "");
                    GenderCb.Focus();
                }
                else if (JobTitleCb.SelectedIndex <= -1)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите должность";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter job title";
                    }
                    else
                    {
                        leng = "Введите должность";
                    }

                    MBClass.ErrorMB(leng, "");
                    JobTitleCb.Focus();
                }
                else if (RoleCb.SelectedIndex <= -1)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите роль";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter role";
                    }
                    else
                    {
                        leng = "Введите роль";
                    }

                    MBClass.ErrorMB(leng, "");
                    RoleCb.Focus();
                }
                else if (SecretaryCb.IsEditable == true &&
                        SecretaryCb.SelectedIndex <= -1)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите секретаря";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter the secretary";
                    }
                    else
                    {
                        leng = "Введите секретаря";
                    }

                    MBClass.ErrorMB(leng, "");
                    SecretaryCb.Focus();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(MiddleNameStaffTb.Text))
                    {
                        MiddleName = null;
                        MiddleNameFull = "";
                    }
                    else
                    {
                        MiddleName = MiddleNameStaffTb.Text;
                        MiddleNameFull = MiddleNameStaffTb.Text;
                    }

                    staff = DBEntities.GetContext().Staff
                                .FirstOrDefault(s => s.IdStaff == staff.IdStaff);

                    if (EditedPassword == true)
                    {
                        staff.User.PasswordUser = TextPassword;
                    }
                    staff.User.IdRole = Int32.Parse(RoleCb.SelectedValue.ToString());

                    if (SecretaryCb.IsEditable == true)
                    {
                        staff.Director1.Secretary.IdStaff = Int32.Parse(SecretaryCb.SelectedValue.ToString());
                    }

                    staff.SurNameStaff = LastNameStaffTb.Text;
                    staff.FirstNameStaff = FirstNameStaffTb.Text;
                    staff.MiddleNameStaff = MiddleName;
                    staff.FullName = (FirstNameStaffTb.Text + " " +
                                    LastNameStaffTb.Text + " "
                                    + MiddleNameFull);
                    staff.NumberPhoneStaff = NumberPhoneStaffTb.Text.Remove(NumberPhoneStaffTb.Text.Length - 10, 10);
                    staff.DateOfBirthStaff = System.DateTime.Parse(DateOfBirthStaffDp.Text);
                    staff.SeriesPassport = Int32.Parse(NumberPassportTb.Text);
                    staff.NumberPassport = Int32.Parse(SeriesPassportTb.Text);
                    staff.IdGender = Int32.Parse(GenderCb.SelectedValue.ToString());
                    staff.AdressStaff = AdressStaffTb.Text;
                    staff.IdJobTitle = Int32.Parse(JobTitleCb.SelectedValue.ToString());
                    if (selectedFileName != "")
                    {
                        staff.PhotoStaff = ClassImage.ConvertImageToArray(selectedFileName);
                    }
                    DBEntities.GetContext().SaveChanges();

                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Данные о сотруднике успешно отредактированы";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Staff details have been successfully edited";
                    }
                    else
                    {
                        leng = "Данные о сотруднике успешно отредактированы";
                    }

                    MBClass.InfoMB(leng, "");
                    NavigationService.Navigate(new ListStaffPage());

                }
            }
            catch (DbEntityValidationException ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void OpenEyesIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Check == 0)
            {
                Check = 1;
                PasswordTb.Text = TextPassword;
                PasswordTb.IsEnabled = true;
                PasswordTb.Opacity = 1;

                PasswordPb.IsEnabled = false;
                PasswordPb.Opacity = 0;

                PasswordTb.Margin = new Thickness(10);
                PasswordPb.Margin = new Thickness(1000);

                CloseEyesIm.Opacity = 0;
                OpenEyesIm.Opacity = 1;
            }
            else if (Check == 1)
            {
                Check = 0;
                PasswordPb.Password = TextPassword;
                PasswordPb.IsEnabled = true;
                PasswordPb.Opacity = 1;

                PasswordTb.IsEnabled = false;
                PasswordTb.Opacity = 0;

                PasswordPb.Margin = new Thickness(10);
                PasswordTb.Margin = new Thickness(1000);

                CloseEyesIm.Opacity = 1;
                OpenEyesIm.Opacity = 0;
            }
        }

        private void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            staff = DBEntities.GetContext().Staff
                                .FirstOrDefault(s => s.IdStaff == staff.IdStaff);

            staff.User.PasswordUser = "123456789";

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Пароль успешно сброшен на 123456789";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Password successfully reset to 123456789";
            }
            else
            {
                leng = "Пароль успешно сброшен на 123456789";
            }

            MBClass.InfoMB(leng, "");
            NavigationService.Navigate(new ListStaffPage());
        }

        private void PasswordTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Check == 1)
            {
                TextPassword = PasswordTb.Text;
            }
        }

        private void PasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Check == 0)
            {
                TextPassword = PasswordPb.Password;
            }
        }

        private void RoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Int32.Parse(RoleCb.SelectedValue.ToString()) == 3)
            {
                SecretaryLb.Opacity = 1;
                SecretaryCb.IsEnabled = true;
                SecretaryCb.Opacity = 1;
            }
            else
            {
                SecretaryLb.Opacity = 0;
                SecretaryCb.IsEnabled = false;
                SecretaryCb.Opacity = 0;
            }
        }

        private void NumberPhoneStaffTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NumberPhoneStaffTb.SelectionStart != 0)
            {
                int a = NumberPhoneStaffTb.SelectionStart;
                NumberPhoneStaffTb.Text = NumberPhoneStaffTb.Text.Remove(a, 1) + " ";
                Return();
            }
        }

        private void NumberPhoneStaffTb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete ||
                e.Key == Key.Space || e.Key == Key.LeftShift ||
                e.Key == Key.LeftAlt || e.Key == Key.LeftCtrl ||
                e.Key == Key.Tab || e.Key == Key.RightCtrl ||
                e.Key == Key.RightShift || e.Key == Key.Right ||
                e.Key == Key.Left || e.Key == Key.Up ||
                e.Key == Key.Down)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Back && NumberPhoneStaffTb.Text.Length != 18)
            {
                e.Handled = true;
                if (NumberPhoneStaffTb.SelectionStart == 9)
                {
                    NumberPhoneStaffTb.SelectionStart = 7;
                }
                else if (NumberPhoneStaffTb.SelectionStart == 13)
                {
                    NumberPhoneStaffTb.SelectionStart = 12;
                }
                else if (NumberPhoneStaffTb.SelectionStart == 16)
                {
                    NumberPhoneStaffTb.SelectionStart = 15;
                }
                int a = NumberPhoneStaffTb.SelectionStart;
                NumberPhoneStaffTb.Text = NumberPhoneStaffTb.Text.Remove(a - 1, 1);
                NumberPhoneStaffTb.Text = NumberPhoneStaffTb.Text.Remove(NumberPhoneStaffTb.Text.Length - 1, 1);
                NumberPhoneStaffTb.Text = NumberPhoneStaffTb.Text.Insert(a - 1, new string(' ', 1));
                Rem = 1;
                Return();
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = true;
            }
        }

        private void NumberPhoneStaffTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SelectionSp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Return();
        }

        private void Return()
        {
            if (NumberPhoneStaffTb.Text.Length >= 18 &&
                NumberPhoneStaffTb.Text.Length <= 20 + Rem)
            {
                NumberPhoneStaffTb.SelectionStart = NumberPhoneStaffTb.Text.Length - 14;
                NumberPhoneStaffTb.Focus();
            }
            else if (NumberPhoneStaffTb.Text.Length >= 21 + Rem &&
                     NumberPhoneStaffTb.Text.Length <= 23 + Rem)
            {
                NumberPhoneStaffTb.SelectionStart = NumberPhoneStaffTb.Text.Length - 12;
                NumberPhoneStaffTb.Focus();
            }
            else if (NumberPhoneStaffTb.Text.Length >= 24 + Rem &&
                     NumberPhoneStaffTb.Text.Length <= 25 + Rem)
            {
                NumberPhoneStaffTb.SelectionStart = NumberPhoneStaffTb.Text.Length - 11;
                NumberPhoneStaffTb.Focus();
            }
            else if (NumberPhoneStaffTb.Text.Length >= 26 + Rem &&
                     NumberPhoneStaffTb.Text.Length <= 28)
            {
                NumberPhoneStaffTb.SelectionStart = NumberPhoneStaffTb.Text.Length - 10;
                NumberPhoneStaffTb.Focus();
            }

            Rem = 0;
        }
    }
}
