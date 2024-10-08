﻿using EasyCaptcha.Wpf;
using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
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

namespace SeritriateDirector.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindowCapchaWindow.xaml
    /// </summary>
    public partial class AuthorizationWindowCapchaWindow : Window
    {
        public AuthorizationWindowCapchaWindow()
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
                AuthorizationLb.Content = "АВТОРИЗАЦИЯ";
                LoginLb.Content = "Логин";
                Passwordlb.Content = "Пароль";
                SaveMeLb.Content = "Запомнить меня";
                LogInTb.Text = "Войти";
                LanguageTb.Text = "Язык";
            }
            else if (globalSettingLanguage == "en")
            {
                AuthorizationLb.Content = "AUTHORIZATION";
                LoginLb.Content = "Login";
                Passwordlb.Content = "Password";
                SaveMeLb.Content = "Remember me";
                LogInTb.Text = "To come in";
                LanguageTb.Text = "Language";
            }
            else
            {
                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
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
            MBClass.ExitMB("");
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

                MBClass.ErrorMB(leng, "");
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

                MBClass.ErrorMB(leng, "");
                PasswordPB.Focus();
                return;
            }
            else if (CheckTb.Text == "" || CheckTb == null)
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Пройдите капчу";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Complete the captcha";
                }
                else
                {
                    leng = "Пройдите капчу";
                }

                MBClass.ErrorMB(leng, "");
                CheckTb.Focus();
                return;
            }
            else if (CheckCapcha == true || CheckTb.Text == text)
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
                CheckCapcha = false;
                return;
            }
            else
            {
                string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                if (globalSettingLanguage == "ru")
                {
                    leng = "Капча не пройдено";
                }
                else if (globalSettingLanguage == "en")
                {
                    leng = "Captcha failed";
                }
                else
                {
                    leng = "Капча не пройдено";
                }

                MBClass.ErrorMB(leng, "");
                CaptchaEs.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                text = CaptchaEs.CaptchaText;
                CheckTb.Text = "";
                CheckTb.Focus();
                Fail = Fail + 1;
                if (Fail >= 1 && ActiveTime == false)
                {
                    ActiveTime = true;
                    CopyTime = (int)Time;
                    double time = (int)Time + 0.5;
                    LogInBtn.IsEnabled = false;
                    Disable = 1;
                    var timer = new DispatcherTimer
                    { Interval = TimeSpan.FromSeconds(time) };
                    timer.Start();
                    TextTime();
                    timer.Tick += (senders, args) =>
                    {
                        timer.Stop();

                        if (globalSettingLanguage == "ru")
                        {
                            leng = "Войти";
                        }
                        else if (globalSettingLanguage == "en")
                        {
                            leng = "To come in";
                        }
                        else
                        {
                            leng = "Войти";
                        }

                        LogInBtn.Content = leng;
                        LogInBtn.IsEnabled = true;
                        Time = Time * 1.5;
                        Fail = 2;
                        Disable = 0;
                        ActiveTime = false;
                    };

                }
                return;
            }
        }

        private void TextTime()
        {
            if (CopyTime > 0)
            {
                int a = (int)CopyTime;
                var timer = new DispatcherTimer
                { Interval = TimeSpan.FromSeconds(1) };
                timer.Start();
                LogInBtn.Content = a;
                timer.Tick += (senders, args) =>
                {
                    timer.Stop();
                    CopyTime = CopyTime - 1;
                    TextTime();
                };
            }
        }

        private bool SaveFileNull = true;
        private bool SaveMe = false;
        private string LoginSave;
        private string PasswordSave;
        private int faille = 0;
        private string leng;
        private bool CheckCapcha = false;
        private bool ActiveTime = false;
        private double Time = 10;
        private double CopyTime = 10;
        private int Fail = 0;
        private string text;
        private int Disable = 0;
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
                else
                {
                    leng = "Введите логин";
                }

                MBClass.ErrorMB(leng, "");
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
                else
                {
                    leng = "Введите пароль";
                }

                MBClass.ErrorMB(leng, "");
                PasswordPB.Focus();
                return;
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.LoginUser == LoginSave);

                    if (user == null)
                    {
                        string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                        if (globalSettingLanguage == "ru")
                        {
                            leng = "Введен не правильный логин или пароль";
                        }
                        else if (globalSettingLanguage == "en")
                        {
                            leng = "Incorrect login or password entered";
                        }
                        else
                        {
                            leng = "Введен не правильный логин или пароль";
                        }

                        CaptchaEs.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                        CheckTb.Text = "";
                        text = CaptchaEs.CaptchaText;

                        MBClass.ErrorMB(leng, "");
                        LoginTB.Focus();
                        return;
                    }
                    if (user.PasswordUser != PasswordSave)
                    {
                        string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                        if (globalSettingLanguage == "ru")
                        {
                            leng = "Введен не правильный логин или пароль";
                        }
                        else if (globalSettingLanguage == "en")
                        {
                            leng = "Incorrect login or password entered";
                        }
                        else
                        {
                            leng = "Введен не правильный логин или пароль";
                        }

                        CaptchaEs.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
                        CheckTb.Text = "";
                        text = CaptchaEs.CaptchaText;

                        MBClass.ErrorMB(leng, "");
                        PasswordPB.Focus();
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
                                (App.Current as App).DeptName = LoginSave;
                                new AdminWindowFolder.MainWindowAdmin().Show();
                                this.Close();
                                break;
                            case 2:
                                (App.Current as App).DeptName = LoginSave;
                                new SecretaryWindowFolder.MainWindowSecretary().Show();
                                this.Close();
                                break;
                            case 3:
                                (App.Current as App).DeptName = LoginSave;
                                new DirectorWindowFolder.MainWindowDirector().Show();
                                this.Close();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMB(ex, "");
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CaptchaEs.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
            text = CaptchaEs.CaptchaText;

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

            (App.Current as App).GlobalSettingLanguage = "1";

            new LanguageSelectionWindow().Show();
            this.Close();
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            CaptchaEs.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
            CheckTb.Text = "";
            text = CaptchaEs.CaptchaText;
        }
    }
}
