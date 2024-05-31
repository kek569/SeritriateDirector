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

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStaffPage.xaml
    /// </summary>
    public partial class AddStaffPage : Page
    {
        public AddStaffPage()
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
                LoginLb.Content = "Логин";
                PasswordLb.Content = "Пароль";
                RoleLb.Content = "Роль";
                LoadPhotoTb.Text = " Загрузить фото";
                AddStaffTb.Text = " Добавить сотрудника";
                Title = "Добавления сотрудника";
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
                LoginLb.Content = "Login";
                PasswordLb.Content = "Password";
                RoleLb.Content = "Role";
                LoadPhotoTb.Text = " Load photo";
                AddStaffTb.Text = " Add staff";
                Title = "Adding staff";
            }
            else
            {
                Title = "Добавления сотрудника";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            GenderCb.ItemsSource = DBEntities.GetContext().Gender.ToList();
            JobTitleCb.ItemsSource = DBEntities.GetContext().JobTitle.ToList();
            SecretaryCb.ItemsSource = DBEntities.GetContext().Staff.ToList();
            RoleCb.ItemsSource = DBEntities.GetContext().Role.ToList();
            NumberPassportTb.MaxLength = 4;
            SeriesPassportTb.MaxLength = 6;

            PasswordPb.PasswordChar = '*';

            SecretaryCb.ItemsSource = DataFolder.DBEntities.GetContext().Staff.
                        Where(s => s.User.Role.NameRole.StartsWith("Секретарь")).ToList();
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        Staff staff = new Staff();
        string selectedFileName = "";
        private string TextPassword;
        private string MiddleName;
        private string MiddleNameFull;
        private int Check = 0;
        private int LastIdUser;
        private int LastIdSecretary;
        private int LastIdDirector;

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

        private void AddStaffBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userOne = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.LoginUser == LoginTb.Text);

                if (userOne == null)
                {
                    if (string.IsNullOrWhiteSpace(LastNameStaffTb.Text))
                    {
                        MBClass.ErrorMB("Введите имя", "");
                        LastNameStaffTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(FirstNameStaffTb.Text))
                    {
                        MBClass.ErrorMB("Введите фамилию", "");
                        FirstNameStaffTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(NumberPhoneStaffTb.Text))
                    {
                        MBClass.ErrorMB("Введите номер телефона", "");
                        NumberPhoneStaffTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(DateOfBirthStaffDp.Text))
                    {
                        MBClass.ErrorMB("Введите дату рождения", "");
                        DateOfBirthStaffDp.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(AdressStaffTb.Text))
                    {
                        MBClass.ErrorMB("Введите адрес", "");
                        AdressStaffTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(LoginTb.Text))
                    {
                        MBClass.ErrorMB("Введите логин", "");
                        LoginTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(TextPassword))
                    {
                        MBClass.ErrorMB("Введите пароль", "");
                        PasswordTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(NumberPassportTb.Text))
                    {
                        MBClass.ErrorMB("Введите серию паспорта", "");
                        NumberPassportTb.Focus();
                    }
                    else if (string.IsNullOrWhiteSpace(SeriesPassportTb.Text))
                    {
                        MBClass.ErrorMB("Введите номер паспорта", "");
                        SeriesPassportTb.Focus();
                    }
                    else if (GenderCb.SelectedIndex <= -1)
                    {
                        MBClass.ErrorMB("Введите пол", "");
                        GenderCb.Focus();
                    }
                    else if (JobTitleCb.SelectedIndex <= -1)
                    {
                        MBClass.ErrorMB("Введите должность", "");
                        JobTitleCb.Focus();
                    }
                    else if (RoleCb.SelectedIndex <= -1)
                    {
                        MBClass.ErrorMB("Введите роль", "");
                        RoleCb.Focus();
                    }
                    else if (SecretaryCb.IsEditable == true &&
                            SecretaryCb.SelectedIndex <= -1)
                    {
                        MBClass.ErrorMB("Введите секретаря", "");
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

                        using (DBEntities db = new DBEntities())
                        {
                            LastIdUser = db.User.Max(a => a.IdUser);
                        }

                        LastIdUser = LastIdUser + 1;

                        if (SecretaryCb.IsEditable == true)
                        {
                            using (DBEntities db = new DBEntities())
                            {
                                LastIdSecretary = db.Secretary.Max(a => a.IdSecretary);
                            }

                            using (DBEntities db = new DBEntities())
                            {
                                LastIdDirector = db.Director.Max(a => a.IdDirector);
                            }

                            LastIdSecretary = LastIdSecretary + 1;
                            LastIdDirector = LastIdDirector + 1;

                            if (selectedFileName == "")
                            {
                                var userAdd = new User()
                                {
                                    LoginUser = LoginTb.Text,
                                    PasswordUser = TextPassword,
                                    IdRole = Int32.Parse(RoleCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().User.Add(userAdd);
                                DBEntities.GetContext().SaveChanges();
                                var secretaryAdd = new Secretary()
                                {
                                    IdStaff = Int32.Parse(SecretaryCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().Secretary.Add(secretaryAdd);
                                DBEntities.GetContext().SaveChanges();
                                var directorAdd = new Director()
                                {
                                    IdSecretary = LastIdSecretary
                                };
                                DBEntities.GetContext().Director.Add(directorAdd);
                                DBEntities.GetContext().SaveChanges();
                                var staffAdd = new Staff()
                                {

                                    SurNameStaff = LastNameStaffTb.Text,
                                    FirstNameStaff = FirstNameStaffTb.Text,
                                    MiddleNameStaff = MiddleName,
                                    FullName = (FirstNameStaffTb.Text + " " +
                                                    LastNameStaffTb.Text + " "
                                                    + MiddleNameFull),
                                    NumberPhoneStaff = NumberPhoneStaffTb.Text,
                                    DateOfBirthStaff = System.DateTime.Parse(DateOfBirthStaffDp.Text),
                                    SeriesPassport = Int32.Parse(SeriesPassportTb.Text),
                                    NumberPassport = Int32.Parse(NumberPassportTb.Text),
                                    IdGender = Int32.Parse(GenderCb.SelectedValue.ToString()),
                                    IdUser = LastIdUser,
                                    IdDirector = LastIdDirector,
                                    AdressStaff = AdressStaffTb.Text,
                                    IdJobTitle = Int32.Parse(JobTitleCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().Staff.Add(staffAdd);
                                DBEntities.GetContext().SaveChanges();

                                MBClass.InfoMB("Данные о сотруднике успешно добавлены", "");
                                NavigationService.Navigate(new ListStaffPage());
                            }
                            else
                            {
                                var userAdd = new User()
                                {
                                    LoginUser = LoginTb.Text,
                                    PasswordUser = TextPassword,
                                    IdRole = Int32.Parse(RoleCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().User.Add(userAdd);
                                DBEntities.GetContext().SaveChanges();
                                var staffAdd = new Staff()
                                {
                                    SurNameStaff = LastNameStaffTb.Text,
                                    FirstNameStaff = FirstNameStaffTb.Text,
                                    MiddleNameStaff = MiddleName,
                                    FullName = (FirstNameStaffTb.Text + " " +
                                                    LastNameStaffTb.Text + " "
                                                    + MiddleNameFull),
                                    NumberPhoneStaff = NumberPhoneStaffTb.Text,
                                    DateOfBirthStaff = System.DateTime.Parse(DateOfBirthStaffDp.Text),
                                    SeriesPassport = Int32.Parse(SeriesPassportTb.Text),
                                    NumberPassport = Int32.Parse(NumberPassportTb.Text),
                                    IdGender = Int32.Parse(GenderCb.SelectedValue.ToString()),
                                    IdUser = LastIdUser,
                                    AdressStaff = AdressStaffTb.Text,
                                    IdJobTitle = Int32.Parse(JobTitleCb.SelectedValue.ToString()),
                                    PhotoStaff = ClassImage.ConvertImageToArray(selectedFileName)
                                };
                                DBEntities.GetContext().Staff.Add(staffAdd);
                                DBEntities.GetContext().SaveChanges();

                                MBClass.InfoMB("Данные о сотруднике успешно добавлены", "");
                                NavigationService.Navigate(new ListStaffPage());
                            }
                        }
                        else
                        {
                            if (selectedFileName == "")
                            {
                                var userAdd = new User()
                                {
                                    LoginUser = LoginTb.Text,
                                    PasswordUser = TextPassword,
                                    IdRole = Int32.Parse(RoleCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().User.Add(userAdd);
                                DBEntities.GetContext().SaveChanges();
                                var staffAdd = new Staff()
                                {

                                    SurNameStaff = LastNameStaffTb.Text,
                                    FirstNameStaff = FirstNameStaffTb.Text,
                                    MiddleNameStaff = MiddleName,
                                    FullName = (FirstNameStaffTb.Text + " " +
                                                    LastNameStaffTb.Text + " "
                                                    + MiddleNameFull),
                                    NumberPhoneStaff = NumberPhoneStaffTb.Text,
                                    DateOfBirthStaff = System.DateTime.Parse(DateOfBirthStaffDp.Text),
                                    SeriesPassport = Int32.Parse(SeriesPassportTb.Text),
                                    NumberPassport = Int32.Parse(NumberPassportTb.Text),
                                    IdGender = Int32.Parse(GenderCb.SelectedValue.ToString()),
                                    IdUser = LastIdUser,
                                    AdressStaff = AdressStaffTb.Text,
                                    IdJobTitle = Int32.Parse(JobTitleCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().Staff.Add(staffAdd);
                                DBEntities.GetContext().SaveChanges();

                                MBClass.InfoMB("Данные о сотруднике успешно добавлены", "");
                                NavigationService.Navigate(new ListStaffPage());
                            }
                            else
                            {
                                var userAdd = new User()
                                {
                                    LoginUser = LoginTb.Text,
                                    PasswordUser = TextPassword,
                                    IdRole = Int32.Parse(RoleCb.SelectedValue.ToString())
                                };
                                DBEntities.GetContext().User.Add(userAdd);
                                DBEntities.GetContext().SaveChanges();
                                var staffAdd = new Staff()
                                {
                                    SurNameStaff = LastNameStaffTb.Text,
                                    FirstNameStaff = FirstNameStaffTb.Text,
                                    MiddleNameStaff = MiddleName,
                                    FullName = (FirstNameStaffTb.Text + " " +
                                                    LastNameStaffTb.Text + " "
                                                    + MiddleNameFull),
                                    NumberPhoneStaff = NumberPhoneStaffTb.Text,
                                    DateOfBirthStaff = System.DateTime.Parse(DateOfBirthStaffDp.Text),
                                    SeriesPassport = Int32.Parse(SeriesPassportTb.Text),
                                    NumberPassport = Int32.Parse(NumberPassportTb.Text),
                                    IdGender = Int32.Parse(GenderCb.SelectedValue.ToString()),
                                    IdUser = LastIdUser,
                                    AdressStaff = AdressStaffTb.Text,
                                    IdJobTitle = Int32.Parse(JobTitleCb.SelectedValue.ToString()),
                                    PhotoStaff = ClassImage.ConvertImageToArray(selectedFileName)
                                };
                                DBEntities.GetContext().Staff.Add(staffAdd);
                                DBEntities.GetContext().SaveChanges();

                                MBClass.InfoMB("Данные о сотруднике успешно добавлены", "");
                                NavigationService.Navigate(new ListStaffPage());
                            }
                        }
                    }
                }
                else
                {
                    MBClass.ErrorMB("Данный логин уже существует", "");
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
    }
}
