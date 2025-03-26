using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Pak_Maker.Commands;

namespace Pak_Maker.VM
{
    internal class MainVM
    {
        private WindowManager _windowManager;
        public ICommand CloseWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }

        
        public MainVM()
        {

            CloseWindowCommand = new RelayCommand(_windowManager.CloseWindow);
            MinimizeWindowCommand = new RelayCommand(_windowManager.MinimizeWindow);
        }
    }
}
