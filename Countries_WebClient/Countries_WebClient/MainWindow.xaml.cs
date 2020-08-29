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
    public class UserWindow
    {
        protected UserControl userControl;
        protected int MaxHeight = 815; // Додумать // Добавить /// <sumary/ для всего комментарии к параметрам и к методам
        protected int MinHeight = 0;
        public UserWindow(UserControl userControl)
        {
            this.userControl = userControl;
        }
        public void SetHeightWindow(int Number)
        {
            userControl.Height = Number;
        }
        public void OpenWindow()
        {
            SetHeightWindow(MaxHeight);
        }
        public void CloseWindow()
        {
            SetHeightWindow(MinHeight);
        }
    }

    public partial class MainWindow : Window
    {
        private static MainWindow sTMainWindow = null;
        public static MainWindow STMainWindow
        {
            get
            {
                return sTMainWindow;
            }
        }
        public static UserWindow SearchWindow;
        public static UserWindow EditingWindow;
        public static UserWindow SettingsWindow;
        public static List<UserWindow> UserWindows;
        public static List<Button> UserButtons;
        public MainWindow()
        {
            InitializeComponent();

            sTMainWindow = this; // переделать в синглтон

            SearchWindow = new UserWindow(WindowSearch);
            EditingWindow = new UserWindow(WindowEditing);
            SettingsWindow = new UserWindow(WindowSettings);

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

        private void BSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Server.HTTPRequestAllow())
            {
                OpenOnlyOneWindow(SearchWindow, UserWindows);
            }
        }

        private void BEditing_Click(object sender, RoutedEventArgs e)
        {
            if (Server.HTTPRequestAllow())
            {
                OpenOnlyOneWindow(EditingWindow, UserWindows);
            }
        }

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            if (Server.HTTPRequestAllow())
            {
                OpenOnlyOneWindow(SettingsWindow, UserWindows);
            }
        }
    }
}
