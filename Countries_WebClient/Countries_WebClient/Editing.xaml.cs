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

        public void Edit()
        {
            if (HTTPClient.HTTPRequestAllow())
            {
                string Result = HTTPClient.HttpRequest($"{Server.Link}updateconcretecountry.ashx?Name={Country.Name}&Code={Country.Code}&Capital={Country.Capital}&Area={Country.Area.ToString().Replace(",", ".")}&Population={Country.Population}&Region={Country.Region}");
                bool ResultIsNull = HTTPClient.HTTPIsNull(Result);

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

        private void BEditing_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void CloseButton(Button Button)
        {
            Button.IsEnabled = false;
        }

        private void OpenButton(Button Button)
        {
            Button.IsEnabled = true;
        }

        private void ValidTextBox(object sender, ValidationErrorEventArgs e)
        {
            CloseButton(BEditing);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OpenButton(BEditing);
        }
    }
}