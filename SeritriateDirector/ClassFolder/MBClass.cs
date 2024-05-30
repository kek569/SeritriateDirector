using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.Office.Core;
using System.CodeDom.Compiler;

namespace SeritriateDirector.ClassFolder
{
    internal class MBClass
    {
        public static void ErrorMB(string text, string leng)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Ошибка";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Error";
            }
            else
            {
                leng = "Ошибка";
            }

            MessageBox.Show(text, leng,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public static void ErrorMB(Exception ex, string leng)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Ошибка";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Error";
            }
            else
            {
                leng = "Ошибка";
            }

            MessageBox.Show(ex.Message, leng,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public static void InfoMB(string text, string leng)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Информация";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Information";
            }
            else
            {
                leng = "Информация";
            }

            MessageBox.Show(text, leng,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public static bool QestionMB(string text, string leng)
        {
            string globalSettingLanguage = (App.Current as App).GlobalSettingLanguage;

            if (globalSettingLanguage == "ru")
            {
                leng = "Вопрос";
            }
            else if (globalSettingLanguage == "en")
            {
                leng = "Question";
            }
            else
            {
                leng = "Вопрос";
            }

            return MessageBoxResult.Yes ==
            MessageBox.Show(text, leng,
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
        }
                
        public static void ExitMB(string leng)
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

            bool result = QestionMB(leng, "");
            if (result == true)
            {
                App.Current.Shutdown();
            }
        }

        public static void ErrorMB(DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult validationError in
                ex.EntityValidationErrors)
            {
                foreach (DbValidationError err in validationError
                    .ValidationErrors)
                {
                    InfoMB(err.ErrorMessage + " \n" + "Object: " + validationError
                        .Entry.Entity.ToString(), "");
                }
            }
        }
    }
}
