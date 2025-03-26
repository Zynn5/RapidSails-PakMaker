using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pak_Maker.VM;
using System.Windows;

namespace Pak_Maker.Commands
{
    public class WindowManager
    {
        private static WindowManager _instance;
        private Window _currentWindow;

        public static WindowManager Instance => _instance ??= new WindowManager();

        private WindowManager() { } 

        internal void CloseWindow()
        {
            Application.Current.Shutdown();
        }

        internal void MinimizeWindow()
        {
            if (_currentWindow != null)
            {
                _currentWindow.WindowState = WindowState.Minimized;
            }
        }

    }



}
