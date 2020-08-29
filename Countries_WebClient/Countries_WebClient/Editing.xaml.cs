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

using System.Net;
using System.IO;

using System.Data.SqlClient;
using System.Data;

using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

using System.ComponentModel;

namespace Countries_WebClient
{
    /// <summary>
    /// Логика взаимодействия для Editing.xaml
    /// </summary>
    /// public class NumericValidationRule : ValidationRule
   
    public partial class Editing : UserControl
    {
        CountryForValid Country;
        public Editing()
        {
            InitializeComponent();
            Country = new CountryForValid();
            this.DataContext = Country;
            CloseButton(BEditing);
        }
        private void BEditing_Click(object sender, RoutedEventArgs e)
        {
            if (Server.HTTPRequestAllow())
            {
                string Result = HTTP.HttpRequest($"{Server.Link}updateconcretecountry.ashx?Name={Country.Name}&Code={Country.Code}&Capital={Country.Capital}&Area={Country.Area.ToString().Replace(",", ".")}&Population={Country.Population}&Region={Country.Region}");
                bool ResultIsNull = HTTP.HTTPIsNull(Result);
                if (ResultIsNull == true)
                {
                    TCondition.Text = $"Ошибка сервера";
                    TCondition.Background = Brushes.Red;
                }
                else if (Result == "2")
                {
                    TCondition.Text = $"Страна \"{TName.Text}\" с таким именем уже существует";
                    TCondition.Background = Brushes.Red;
                }
                else
                {
                    TCondition.Text = $"Операция выполнена успешно";
                    TCondition.Background = Brushes.Green;
                }
            }
        }
        private void CloseButton(Button Button)
        {
            Button.IsEnabled = false;
        }
        private void OpenButton(Button Button)
        {
            Button.IsEnabled = true;
        }

        private void NameValid(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }
        private void CodeValid(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }
        private void CapitalValid(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }
        private void AreaValid(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }
        private void PopulationValid(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }
        private void RegionValid(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OpenButton(BEditing);
        }
    }
}
