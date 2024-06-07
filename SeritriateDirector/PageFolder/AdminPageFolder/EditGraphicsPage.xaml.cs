using SeritriateDirector.ClassFolder;
using SeritriateDirector.DataFolder;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace SeritriateDirector.PageFolder.AdminPageFolder
{
    /// <summary>
    /// Логика взаимодействия для EditGraphicsPage.xaml
    /// </summary>
    public partial class EditGraphicsPage : Page
    {
        DataFolder.Graphics graphics = new DataFolder.Graphics();

        public EditGraphicsPage(DataFolder.Graphics graphics)
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
                NameEventsTbl.Text = "Название мероприятия";
                PlaceEventsLb.Content = "Место";
                DateEventsLb.Content = "Дата и время";
                TargetEventsLb.Content = "Цель";
                DirectorTbl.Text = "Назначено на директора";
                LoadPhotoTb.Text = " Загрузить фото";
                SaveTb.Text = " Сохранить изменения";
                Title = "добавление графика";
            }
            else if (globalSettingLanguage == "en")
            {
                NameEventsTbl.Text = "Name event";
                PlaceEventsLb.Content = "Place";
                DateEventsLb.Content = "Date and time";
                TargetEventsLb.Content = "Target";
                DirectorTbl.Text = "Assigned to Director";
                LoadPhotoTb.Text = " Load photo";
                SaveTb.Text = " Save changes";
                Title = "Add graphic";
            }
            else
            {
                Title = "добавление графиков";

                MBClass.ErrorMB("Языковая настройка слетела! Язык по умолчанию русский!\n\n" +
                    "The language setting is gone! The default language is Russian!", "");
            }

            DataContext = graphics;
            this.graphics.IdGraphics = graphics.IdGraphics;

            DirectorCb.ItemsSource = DBEntities.GetContext().Staff.ToList();
            DirectorCb.ItemsSource = DataFolder.DBEntities.GetContext().Staff.
                        Where(s => s.User.Role.NameRole.StartsWith("Директор")).ToList();
            TimeEventsTb.MaxLength = 5;

            var timer = new DispatcherTimer
            { Interval = TimeSpan.FromSeconds(0.01) };
            timer.Start();
            timer.Tick += (senders, args) =>
            {
                timer.Stop();
                graphics = DBEntities.GetContext().Graphics
                                .FirstOrDefault(g => g.IdGraphics == graphics.IdGraphics);
                DateEventsDp.Text = graphics.DateEvents.ToString();
            };
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPhoto();
        }

        string selectedFileName = "";
        private string leng;
        private string TimeSearch = "";
        private bool timeSearch = false;

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
                    graphics.PhotoEvents = ClassImage.ConvertImageToArray(selectedFileName);
                    PhotoIM.Source = ClassImage.ConvertByteArrayToImage(graphics.PhotoEvents);


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


                if (string.IsNullOrWhiteSpace(NameEventsTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите названия мероприятия";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter event names";
                    }
                    else
                    {
                        leng = "Введите названия мероприятия";
                    }

                    MBClass.ErrorMB(leng, "");
                    NameEventsTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(PlaceEventsTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите место проведения мероприятия";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter event place";
                    }
                    else
                    {
                        leng = "Введите место проведения мероприятия";
                    }

                    MBClass.ErrorMB(leng, "");
                    PlaceEventsTb.Focus();
                }
                else if (string.IsNullOrWhiteSpace(DateEventsDp.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите дату проведения мероприятия";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter the date of the event";
                    }
                    else
                    {
                        leng = "Введите дату проведения мероприятия";
                    }

                    MBClass.ErrorMB(leng, "");
                    DateEventsDp.Focus();
                }
                else if (string.IsNullOrWhiteSpace(TimeEventsTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите время начало проведения мероприятия";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter the start time of the event";
                    }
                    else
                    {
                        leng = "Введите время начало проведения мероприятия";
                    }

                    MBClass.ErrorMB(leng, "");
                    Return();
                }
                else if (TimeEventsTb.Text.Length != 5)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите время начало проведения мероприятия";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter the start time of the event";
                    }
                    else
                    {
                        leng = "Введите время начало проведения мероприятия";
                    }

                    MBClass.ErrorMB(leng, "");
                    Return();
                }
                else if (string.IsNullOrWhiteSpace(TargetEventsTb.Text))
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Введите цель мероприятия";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Enter the target of the event";
                    }
                    else
                    {
                        leng = "Введите цель мероприятия";
                    }

                    MBClass.ErrorMB(leng, "");
                    TargetEventsTb.Focus();
                }
                else if (DirectorCb.SelectedIndex <= -1)
                {
                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Выберите директора на которого назначен данный график";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Select the director to whom this schedule is assigned";
                    }
                    else
                    {
                        leng = "Выберите директора на которого назначен данный график";
                    }

                    MBClass.ErrorMB(leng, "");
                    DirectorCb.Focus();
                }
                else
                {
                    graphics = DBEntities.GetContext().Graphics
                            .FirstOrDefault(g => g.IdGraphics == graphics.IdGraphics);
                    graphics.NameEvents = NameEventsTb.Text;
                    graphics.PlaceEvents = PlaceEventsTb.Text;
                    graphics.DateEvents = DateTime.Parse(DateEventsDp.Text);
                    graphics.TimeEvents = TimeSpan.Parse(TimeEventsTb.Text);
                    graphics.TargetEvents = TargetEventsTb.Text;
                    graphics.IdStaff = Int32.Parse(DirectorCb.SelectedValue.ToString());
                    if (selectedFileName != "")
                    {
                        graphics.PhotoEvents = ClassImage.ConvertImageToArray(selectedFileName);
                    }
                    DBEntities.GetContext().SaveChanges();

                    string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

                    if (globalSettingLanguage == "ru")
                    {
                        leng = "Данные о графике успешно отредактированы";
                    }
                    else if (globalSettingLanguage == "en")
                    {
                        leng = "Graphic data successfully edited";
                    }
                    else
                    {
                        leng = "Данные о графике успешно отредактированы";
                    }

                    MBClass.InfoMB(leng, "");
                    NavigationService.Navigate(new ListGraphicsPage());
                }
            }
            catch (DbEntityValidationException ex)
            {
                MBClass.ErrorMB(ex);
            }
        }

        private void TimeEvents_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TimeEventsTb.Text.Length == 3)
            {
                TimeEventsTb.SelectionStart = 3;
            }
            else if (TimeEventsTb.Text.Length > 5)
            {
                TimeEventsTb.Text = TimeEventsTb.Text.Remove(5, 3);
            }
        }

        private void TimeEvents_PreviewKeyDown(object sender, KeyEventArgs e)
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
            else if (e.Key == Key.Back && TimeEventsTb.Text.Length == 3)
            {
                e.Handled = true;
                TimeEventsTb.SelectionLength = 2;
                TimeEventsTb.Text = TimeEventsTb.Text.Remove(1, 1);
                TimeEventsTb.SelectionStart = 1;
                TimeSearch = "";
            }
        }

        private void TimeEvents_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (TimeEventsTb.Text.Length == 1)
            {
                Regex regex = new Regex("[^0-2]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimeEventsTb.Text.Length == 2)
            {
                if (TimeEventsTb.Text != "2:")
                {
                    Regex regex = new Regex("[^0-9]+");
                    e.Handled = regex.IsMatch(e.Text);
                }
                else
                {
                    Regex regex = new Regex("[^0-4]+");
                    e.Handled = regex.IsMatch(e.Text);
                }
            }
            else if (TimeEventsTb.Text.Length == 3)
            {
                Regex regex = new Regex("[^0-5]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            else if (TimeEventsTb.Text.Length <= 4)
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void TimeReturnSp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Return();
        }

        private void Return()
        {
            if (TimeEventsTb.Text.Length == 1)
            {

                TimeEventsTb.SelectionStart = 0;
                TimeEventsTb.Focus();
            }
            else if (TimeEventsTb.Text.Length == 2)
            {

                TimeEventsTb.SelectionStart = 1;
                TimeEventsTb.Focus();
            }
            else
            {

                TimeEventsTb.SelectionStart = TimeEventsTb.Text.Length;
                TimeEventsTb.Focus();
            }
        }
    }
}
