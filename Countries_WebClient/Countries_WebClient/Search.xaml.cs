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
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    /// 

    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        private void ClearResult(ListBox ListBox)
        {
            ListBox.Items.Clear();
        }
        private void AddResult(ListBox ListBox, List<string> Data)
        {
            for (int i = 0; i != Data.Count; i++)
            {
                ListBox.Items.Add(Data[i]);
            }
        }
        private void SearchConcreteCountry_Click(object sender, RoutedEventArgs e)
        {
            Server.Check();
            ServerReactionCheck.ReactionCheck();
            if (Server.ServerConnection == 1)
            {
                ClearResult(CountriesInfo);
                string CountryName = SearchTextBox.Text.ToString();
                string Result = HTTP.HttpRequest($"{Server.Link}SelectConcreteCountry.ashx?Country={CountryName}");
                bool ResultIsNull = HTTP.HTTPIsNull(Result);
                if (ResultIsNull == true)
                {
                    TCondition.Text = "Страна не найдена";
                    TCondition.Background = Brushes.Red;
                }
                else
                {
                    AddResult(CountriesInfo, HTTP.HTTPResponseToData(Result));
                    TCondition.Background = Brushes.Green;
                    TCondition.Text = "Страна найдена";
                }
            }
        }

        private void SearchAllCounties_Click(object sender, RoutedEventArgs e)
        {
            if (Server.HTTPRequestAllow())
            {
                ClearResult(CountriesInfo);
                string Result = HTTP.HttpRequest($"{Server.Link}SelectAllCountries.ashx");
                bool ResultIsNull = HTTP.HTTPIsNull(Result);
                if (ResultIsNull == true)
                {
                    TCondition.Text = "Страны не найдены";
                    TCondition.Background = Brushes.Red;
                }
                else
                {
                    AddResult(CountriesInfo, HTTP.HTTPResponseToData(Result));
                    TCondition.Background = Brushes.Green;
                    TCondition.Text = "Страны найдены";
                }
            }
        }
    }
}
