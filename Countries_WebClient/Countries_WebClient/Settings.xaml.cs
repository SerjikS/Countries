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

using System.IO;

namespace Countries_WebClient
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        private static Settings sTSettings = null;
        public static Settings STSettings
        { 
            get
            {
                return sTSettings;
            }
        }

        private static string Path = $@"C:\\Users\{Environment.UserName}\Documents\";
        private static string Document = "Link.txt";
        private static string PathDocument = Path + Document;
        public Settings()
        {
            InitializeComponent();
            TCondition.Text = "Проверьте подключение";
            sTSettings = this;
            try
            {
                if (File.Exists(PathDocument) == true)
                {
                    Server.Link = ReadDocumentLink(PathDocument);
                }
                else
                {
                    CreateDocumentLink(PathDocument, Server.Link);
                }
            }
            finally
            {

            }
            CloseButton(BSaveLink);
            TSite.Text = Server.Link;
        }
        private void CloseButton(Button button)
        {
            button.IsEnabled = false;
        }
        private void OpenButton(Button button)
        {
            button.IsEnabled = true;
        }
        private void CreateDocumentLink(string PathDpcument, string Data)
        {
            FileStream fileStream = new FileStream(PathDpcument, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(Server.Link);
            streamWriter.Close();
        }
        private string ReadDocumentLink(string PathDpcument)
        {
            string Data = null;
            FileStream fileStream = new FileStream(PathDocument, FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            Data = streamReader.ReadToEnd();
            streamReader.Close();
            return Data;
        }

        private void BSaveLink_Click(object sender, RoutedEventArgs e)
        {
            Server.Link = TSite.Text;
            try
            {
                CreateDocumentLink(PathDocument, Server.Link);   
            }
            finally
            {

            }
            CloseButton(BSaveLink);
        }

        private void BTestLink_Click(object sender, RoutedEventArgs e)
        {
            TestLink();
        }

        private void TestLink()
        {
            Server.Check();
            ServerReactionCheck.ReactionCheck();
        }
        private void TSite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TSite.Text != Server.Link)
            {
                OpenButton(BSaveLink);
            }
        }

        private void TSite_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (TSite.Text != Server.Link)
            {
                OpenButton(BSaveLink);
            }
        }
    }
    }
