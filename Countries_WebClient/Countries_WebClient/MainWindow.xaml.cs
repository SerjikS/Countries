using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///     
    public partial class MainWindow : Window
    {
        private static MainWindow sTMainWindow = null;
        private static int MaxHeight = 815;

        public static UserWindow SearchWindow;
        public static UserWindow EditingWindow;
        public static UserWindow SettingsWindow;

        public static List<UserWindow> UserWindows;
        public static List<Button> UserButtons;
        public static MainWindow STMainWindow
        {
            get
            {
                return sTMainWindow;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            sTMainWindow = this;

            SearchWindow = new UserWindow(WindowSearch, MaxHeight);
            EditingWindow = new UserWindow(WindowEditing, MaxHeight);
            SettingsWindow = new UserWindow(WindowSettings, MaxHeight);

            UserWindows = new List<UserWindow>() { SearchWindow, EditingWindow, SettingsWindow };
            UserButtons = new List<Button>() { BSearch, BEditing, BSettings };

            CloseButtons();
            OpenSettingsWindow();
        }

        private void OpenOnlyOneWindow(UserWindow Name, List<UserWindow> List)
        {
            Name.OpenWindow();

            for (int i = 0; i != List.Count; i++)
            {
                if (Name.GetHashCode() != List[i].GetHashCode())
                {
                    List[i].CloseWindow();
                }
            }
        }

        private void AllowOnlyOneButton(Button Name, List<Button> List)
        {
            Name.IsEnabled = true;

            for (int i = 0; i != List.Count; i++)
            {
                if (Name.GetHashCode() != List[i].GetHashCode())
                {
                    List[i].IsEnabled = false;
                }
            }
        }

        private void AllowAllButtons(List<Button> List)
        {
            for (int i = 0; i != List.Count; i++)
            {
                List[i].IsEnabled = true;
            }
        }

        private void CloseButtons()
        {
            AllowOnlyOneButton(BSettings, UserButtons);
        }

        public void OpenSettingsWindow()
        {
            AllowOnlyOneButton(BSettings, UserButtons);
            OpenOnlyOneWindow(SettingsWindow, UserWindows);
        }

        public void OpenButtons()
        {
            AllowAllButtons(UserButtons);
        }

        public void OpenSearchButton()
        {
            if (HTTPClient.HTTPRequestAllow())
            {
                OpenOnlyOneWindow(SearchWindow, UserWindows);
            }
        }

        public void OpenEditingButton()
        {
            if (HTTPClient.HTTPRequestAllow())
            {
                OpenOnlyOneWindow(EditingWindow, UserWindows);
            }
        }

        public void OpenSettingsButton()
        {
            if (HTTPClient.HTTPRequestAllow())
            {
                OpenOnlyOneWindow(SettingsWindow, UserWindows);
            }
        }

        private void BSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenSearchButton();
        }

        private void BEditing_Click(object sender, RoutedEventArgs e)
        {
            OpenEditingButton();
        }

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            OpenSettingsButton();
        }
    }
}
