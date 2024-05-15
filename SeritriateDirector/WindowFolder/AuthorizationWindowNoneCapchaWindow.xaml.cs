﻿using SeritriateDirector.ClassFolder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Threading;
using static System.Net.WebRequestMethods;

namespace SeritriateDirector.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindowNoneCapchaWindow.xaml
    /// </summary>
    public partial class AuthorizationWindowNoneCapchaWindow : Window
    {
        public AuthorizationWindowNoneCapchaWindow()
        {
            InitializeComponent();

            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                AuthorizationLb.Content = "АВТОРИЗАЦИЯ";
                LoginLb.Content = "Логин";
                Passwordlb.Content = "Пароль";
                SaveMeLb.Content = "Запомнить меня";
                LogInBtn.Content = "Войти";
                LanguageBtn.Content = "Язык";
            }
            else if (globalSettingLanguage == "en")
            {
                AuthorizationLb.Content = "AUTHORIZATION";
                LoginLb.Content = "Login";
                Passwordlb.Content = "Password";
                SaveMeLb.Content = "Remember me";
                LogInBtn.Content = "To come in";
                LanguageBtn.Content = "Language";
            }
            else
            {
                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Вы действительно желаете выйти?";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Do you really want to go out?";
            }
            else
            {
                leng = "Вы действительно желаете выйти?";
            }

            bool ret = MBClass.QestionMB(leng);
            if (ret == true)
            {
                this.Close();
            }
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Введите логин";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Enter login";
                }
                else
                {
                    leng = "Введите логин";
                }

                faille = faille + 1;

                if (faille >= 3)
                {
                    Capcha();
                }

                MBClass.ErrorMB(leng);
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPB.Password))
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

                MBClass.ErrorMB(leng);
                PasswordPB.Focus();
                return;
            }
            else
            {
                LoginSave = LoginTB.Text;
                PasswordSave = PasswordPB.Password;
                if (SaveMeCb.IsChecked == true)
                {
                    SaveMe = true;
                    Login();
                }
                else
                {
                    SaveMe = false;
                    using (StreamWriter newTask = new StreamWriter("Save.txt", false)) { }
                    Login();
                }
                return;
            }
                
        }

        private bool SaveFileNull = true;
        private bool SaveMe = false;
        private string LoginSave;
        private string PasswordSave;
        private int faille = 0;
        private string leng;
        private string path = (App.Current as App).Path;

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(LoginSave))
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Введите логин";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Enter login";
                }

                MBClass.ErrorMB(leng);
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordSave))
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

                MBClass.ErrorMB(leng);
                PasswordPB.Focus();
                return;
            }
            else
            {
                /*try
                {
                    var user = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.LoginUser == LoginSave);

                    if (user == null)
                    {
                        faille = faille + 1;
                        MBClass.ErrorMB("Введен не правильный логин или пароль");
                        LoginTB.Focus();

                        if (faille >= 3)
                        {
                            Capcha();
                        }

                        return;
                    }
                    if (user.PasswordUser != PasswordSave)
                    {
                        faille = faille + 1;
                        MBClass.ErrorMB("Введен не правильный логин или пароль");
                        PasswordPB.Focus();

                        if (faille >= 3)
                        {
                            Capcha();
                        }

                        return;
                    }
                    else
                    {
                        if (SaveMe == true)
                        {
                            using (StreamWriter newTask = new StreamWriter(path + "Save.txt", false))
                            {
                                newTask.WriteLine(LoginSave + "\n" + PasswordSave);
                            }
                        }

                        switch (user.IdRole)
                        {
                            case 1:
                                new AdminWindowFolder.AdminWindow().Show();
                                this.Close();
                                break;
                            case 2:
                                (App.Current as App).DeptName = LoginSave;
                                new StaffWindowFolder.StaffWindow().Show();
                                this.Close();
                                break;
                }
            }
        }
        catch (Exception ex)
        {
            MBClass.ErrorMB(ex);
        }*/
            }
        }

        private void Capcha()
        {
            new AuthorizationWindowCapchaWindow().Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (StreamReader newTask = new StreamReader(path + "Save.txt", true))
            {
                if (newTask == null || newTask.ReadToEnd() == "")
                {
                    SaveFileNull = true;
                }
                else
                {
                    SaveFileNull = false;
                }
            }
            if (SaveFileNull == false)
            {
                using (StreamReader newTask = new StreamReader(path + "Save.txt", false))
                {
                    LoginSave = newTask.ReadLine();
                    PasswordSave = System.IO.File.ReadLines(path + "Save.txt").Skip(1).First();

                    LoginTB.Text = LoginSave;
                    PasswordPB.Password = PasswordSave;
                    SaveMeCb.IsChecked = true;

                    //Login();
                }
            }
        }

        private void LanguageBtn_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter newTask = new StreamWriter(path + "SaveLanguageSetting.txt", false))
            {
                newTask.WriteLine("");
            }

            new LanguageSelectionWindow().Show();
            this.Close();
        }
    }
}
