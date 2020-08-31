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
    public class UserWindow
    {
        protected UserControl userControl;
        protected int MaxHeight = 815;
        protected int MinHeight = 0;

        public UserWindow(UserControl userControl, int MaxHeight)
        {
            this.userControl = userControl;
            this.MaxHeight = MaxHeight;
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
}
