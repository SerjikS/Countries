using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Media;

namespace Countries_WebClient
{
    static class  ServerReactionCheck
    {
        public static void ReactionCheck()
        {
            MainWindow SingleTonMainWindow = MainWindow.STMainWindow;
            Settings SingleTonSettingsWindow = Settings.STSettings;
            if (Server.ServerConnection == 0)
            {
                SingleTonSettingsWindow.TCondition.Background = Brushes.Red;
                SingleTonSettingsWindow.TCondition.Text = "Нет соединения";
                SingleTonMainWindow.OpenSettingsWindow();
            }
            if (Server.ServerConnection == 1)
            {
                SingleTonSettingsWindow.TCondition.Background = Brushes.Green;
                SingleTonSettingsWindow.TCondition.Text = "Соединение установлено";
                SingleTonMainWindow.OpenButtons();
            }
            if (Server.ServerConnection == 2)
            {
                SingleTonSettingsWindow.TCondition.Background = Brushes.DarkOrange;
                SingleTonSettingsWindow.TCondition.Text = "Внутренняя ошибка сервера";
                SingleTonMainWindow.OpenSettingsWindow();
            }
        }
    }
}
