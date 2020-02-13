using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Windows.Input;

namespace WPFCaching.ViewModels
{
    class MainPageViewModel : WrapperViewModel
    {
        public ICommand GetCache_Cmd { get; set; }

        private string _fileContents;
        public string FileContents
        {
            get
            {
                return _fileContents;
            }
            set
            {
                OnPropertyChanged(ref _fileContents, value);
            }
        }

        public MainPageViewModel()
        {
            FileContents = "";
            GetCache_Cmd = new RelayCommand(GetCache);
        }

        private bool CheckCache()
        {
            throw new NotImplementedException();
        }

        private void GetCache()
        {
            // Instantiate the cache object 
            ObjectCache cache = MemoryCache.Default;

            // Read the contents of a cache entry named filecontents
            FileContents = cache["filecontents"] as string;

            // Check whether the cache entry named filecontents exists
            if (FileContents == null)
            {
                // Create a new CacheItemPolicy object that expires after 10 seconds
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20.0)
                };

                // Create a collection for the file paths that you want to monitor
                List<string> filePaths = new List<string>
                {
                    "c:\\cache\\cacheText.txt"
                };

                // Add a new HostFileChangeMonitor
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));
                

                // Read the contents of the text file
                FileContents = File.ReadAllText("c:\\cache\\cacheText.txt") + "\n" + DateTime.Now;

                // Insert the contents of the file into the cache object
                cache.Set("filecontents", FileContents, policy);
            }
        }

    }
}
