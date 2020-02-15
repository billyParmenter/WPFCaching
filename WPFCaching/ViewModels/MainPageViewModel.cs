using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Windows.Input;

namespace WPFCaching.ViewModels
{
    class MainPageViewModel : WrapperViewModel
    {
        public ICommand GoToTesting_Cmd { get; set; }

        

        public MainPageViewModel()
        {
            GoToTesting_Cmd = new RelayCommand(ChangeView);
        }

        

        private void ChangeView()
        {
            Navigate(new TestingViewModel());
        }

    }
}
