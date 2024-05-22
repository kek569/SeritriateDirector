using SeritriateDirector.ClassFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SeritriateDirector.DataFolder;
using SeritriateDirector.PageFolder.AdminPageFolder;

namespace SeritriateDirector.PageFolder
{
    /// <summary>
    /// Логика взаимодействия для SendingBugsPage.xaml
    /// </summary>
    public partial class SendingBugsPage : Page
    {
        public SendingBugsPage()
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
                Title = "Отправка сообщения об баге";
                SendingBugsBtn.Content = "Отправить сообщения";
                LoadPhotoBtn.Content = "Загрузить фото";
                theme = "Баги";
            }
            else if (globalSettingLanguage == "en")
            {
                Title = "Submitting a bug report";
                SendingBugsBtn.Content = "Send messages";
                LoadPhotoBtn.Content = "Load a photo";
                theme = "Bugs";
            }
            else
            {
                Title = "Отправка сообщения об баге";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        string selectedFileOneName = "";
        string selectedFileTwoName = "";
        private string theme;
        private string leng;
        private bool TwoImage = false;
        AlternateView jpeg_view;

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
                    if(ImageOneIm.Source == null)
                    {
                        selectedFileOneName = op.FileName;
                        var bitmapImage = new BitmapImage();

                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(selectedFileOneName, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();

                        ImageOneIm.Source = bitmapImage;
                    }
                    else
                    {
                        selectedFileTwoName = op.FileName;
                        var bitmapImage = new BitmapImage();

                        bitmapImage.BeginInit();
                        bitmapImage.UriSource = new Uri(selectedFileTwoName, UriKind.RelativeOrAbsolute);
                        bitmapImage.EndInit();

                        ImageTwoIm.Source = bitmapImage;

                        TwoImage = true;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SendingBugsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFileOneName == "" || selectedFileTwoName == "")
            {
                string smtpServer = " smtp.mail.ru"; //smpt сервер(зависит от почты отправителя)
                int smtpPort = 25; // Обычно используется порт 587 для TLS
                string smtpUsername = "bugssend@mail.ru"; //твоя почта, с которой отправляется сообщение
                string smtpPassword = "XYjjbsjr9bVaDEvEZ7AG";//пароль приложения (от почты)

                // Создаем объект клиента SMTP
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    // Настройки аутентификации
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        string msg = MassageBugsTb.Text + @"<br>";
                        AlternateView html_view = AlternateView.CreateAlternateViewFromString(msg, null, "text/html");

                        mailMessage.IsBodyHtml = true;
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.To.Add("bugssend@mail.ru");
                        mailMessage.Subject = theme;
                        try
                        {
                            // Отправляем сообщение
                            mailMessage.AlternateViews.Add(html_view);
                            smtpClient.Send(mailMessage);

                            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                            if (globalSettingLanguage == "ru")
                            {
                                leng = "Сообщение об ошибке успешно отправлено";
                            }
                            else if (globalSettingLanguage == "en")
                            {
                                leng = "Message bug sent successfully";
                            }
                            else
                            {
                                leng = "Сообщение об ошибке успешно отправлено";
                            }

                            MBClass.InfoMB(leng, "");

                            NavigationService.GoBack();
                        }
                        catch (Exception ex)
                        {
                            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                            if (globalSettingLanguage == "ru")
                            {
                                leng = $"Ошибка отправки сообщения: {ex.Message}";
                            }
                            else if (globalSettingLanguage == "en")
                            {
                                leng = $"Error sending message: {ex.Message}";
                            }
                            else
                            {
                                leng = $"Ошибка отправки сообщения: {ex.Message}";
                            }

                            MBClass.ErrorMB(leng, "");
                        }
                    }
                }
            }
            else if (TwoImage == false)
            {
                string smtpServer = " smtp.mail.ru"; //smpt сервер(зависит от почты отправителя)
                int smtpPort = 25; // Обычно используется порт 587 для TLS
                string smtpUsername = "bugssend@mail.ru"; //твоя почта, с которой отправляется сообщение
                string smtpPassword = "XYjjbsjr9bVaDEvEZ7AG";//пароль приложения (от почты)

                // Создаем объект клиента SMTP
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    // Настройки аутентификации
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        string msg = MassageBugsTb.Text + @"<br> <img src=""cid:uniqueId"" width=""*"" height=""*"">";
                        AlternateView html_view = AlternateView.CreateAlternateViewFromString(msg, null, "text/html");

                        if (selectedFileTwoName == "")
                        {
                            jpeg_view = new AlternateView(selectedFileOneName, MediaTypeNames.Image.Jpeg);
                        }
                        else if (selectedFileOneName == "")
                        {
                            jpeg_view = new AlternateView(selectedFileTwoName, MediaTypeNames.Image.Jpeg);
                        }

                        jpeg_view.ContentId = "uniqueId";
                        jpeg_view.TransferEncoding = TransferEncoding.Base64;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.To.Add("bugssend@mail.ru");
                        mailMessage.Subject = theme;
                        try
                        {
                            // Отправляем сообщение
                            mailMessage.AlternateViews.Add(html_view);
                            mailMessage.AlternateViews.Add(jpeg_view);
                            smtpClient.Send(mailMessage);

                            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                            if (globalSettingLanguage == "ru")
                            {
                                leng = "Сообщение об ошибке успешно отправлено";
                            }
                            else if (globalSettingLanguage == "en")
                            {
                                leng = "Message bug sent successfully";
                            }
                            else
                            {
                                leng = "Сообщение об ошибке успешно отправлено";
                            }

                            MBClass.InfoMB(leng, "");

                            NavigationService.GoBack();
                        }
                        catch (Exception ex)
                        {
                            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                            if (globalSettingLanguage == "ru")
                            {
                                leng = $"Ошибка отправки сообщения: {ex.Message}";
                            }
                            else if (globalSettingLanguage == "en")
                            {
                                leng = $"Error sending message: {ex.Message}";
                            }
                            else
                            {
                                leng = $"Ошибка отправки сообщения: {ex.Message}";
                            }

                            MBClass.ErrorMB(leng, "");
                        }
                    }
                }
            }
            else
            {
                string smtpServer = " smtp.mail.ru"; //smpt сервер(зависит от почты отправителя)
                int smtpPort = 25; // Обычно используется порт 587 для TLS
                string smtpUsername = "bugssend@mail.ru"; //твоя почта, с которой отправляется сообщение
                string smtpPassword = "XYjjbsjr9bVaDEvEZ7AG";//пароль приложения (от почты)

                // Создаем объект клиента SMTP
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    // Настройки аутентификации
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        string msg = MassageBugsTb.Text + @"<br> <img src=""cid:uniqueOneId"" width=""*"" height=""*"">  
                                                            <br> <img src=""cid:uniqueTwoId"" width=""*"" height=""*"">";
                        AlternateView html_view = AlternateView.CreateAlternateViewFromString(msg, null, "text/html");

                        AlternateView jpeg_viewOne = new AlternateView(selectedFileOneName, MediaTypeNames.Image.Jpeg);
                        AlternateView jpeg_viewTwo = new AlternateView(selectedFileTwoName, MediaTypeNames.Image.Jpeg);

                        jpeg_viewOne.ContentId = "uniqueOneId";
                        jpeg_viewOne.TransferEncoding = TransferEncoding.Base64;

                        jpeg_viewTwo.ContentId = "uniqueTwoId";
                        jpeg_viewTwo.TransferEncoding = TransferEncoding.Base64;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.To.Add("bugssend@mail.ru");
                        mailMessage.Subject = theme;
                        try
                        {
                            // Отправляем сообщение
                            mailMessage.AlternateViews.Add(html_view);
                            mailMessage.AlternateViews.Add(jpeg_viewOne);
                            mailMessage.AlternateViews.Add(jpeg_viewTwo);
                            smtpClient.Send(mailMessage);

                            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                            if (globalSettingLanguage == "ru")
                            {
                                leng = "Сообщение об ошибке успешно отправлено";
                            }
                            else if (globalSettingLanguage == "en")
                            {
                                leng = "Message bug sent successfully";
                            }
                            else
                            {
                                leng = "Сообщение об ошибке успешно отправлено";
                            }

                            MBClass.InfoMB(leng, "");

                            NavigationService.GoBack();
                        }
                        catch (Exception ex)
                        {
                            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                            if (globalSettingLanguage == "ru")
                            {
                                leng = $"Ошибка отправки сообщения: {ex.Message}";
                            }
                            else if (globalSettingLanguage == "en")
                            {
                                leng = $"Error sending message: {ex.Message}";
                            }
                            else
                            {
                                leng = $"Ошибка отправки сообщения: {ex.Message}";
                            }

                            MBClass.ErrorMB(leng, "");
                        }
                    }
                }
            }
        }

        private void ImageOneIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageOneIm.Source = null;
            selectedFileOneName = "";
        }

        private void ImageTwoIm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageTwoIm.Source = null;
            selectedFileTwoName = "";
            TwoImage = false;
        }
    }
}
