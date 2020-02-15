using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFCaching.ViewModels
{
    class TestingViewModel : WrapperViewModel
    {
        public ICommand GetCache_Cmd { get; set; }
        public ICommand Back_Cmd { get; set; }
        public ICommand TestCache_Cmd { get; set; }
        public ICommand TestFile_Cmd { get; set; }

        private ObjectCache _cache = MemoryCache.Default;
        private string _fileContents;
        private string _fileTestText;
        private string _cacheTestText;
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

        
        public string FileTestText
        {
            get
            {
                return _fileTestText;
            }
            set
            {
                OnPropertyChanged(ref _fileTestText, value);
            }
        }
        public string CacheTestText
        {
            get
            {
                return _cacheTestText;
            }
            set
            {
                OnPropertyChanged(ref _cacheTestText, value);
            }
        }

        public TestingViewModel()
        {
            FileContents = "";
            GetCache_Cmd = new RelayCommand(GetCache);
            TestCache_Cmd = new RelayCommand(TestCache);
            TestFile_Cmd = new RelayCommand(TestFile);
            Back_Cmd = new RelayCommand(Back);
        }

        private void GetCache()
        {
            // Read the contents of the text file
            FileContents = _cache["filecontents"] as string;
        }

        private void GetFile()
        {
            FileContents = File.ReadAllText("c:\\cache\\text.txt");
        }

        private void TestCache()
        {
            LoadCache(5);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                GetCache();
            }
            stopwatch.Stop();

            CacheTestText = FileContents + "\n" + stopwatch.ElapsedMilliseconds;

        }

        private void TestFile()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                GetFile();
            }
            stopwatch.Stop();

            FileTestText = FileContents + "\n" + stopwatch.ElapsedMilliseconds;
        }



        private void LoadCache(double timeOut)
        {
            // Read the contents of a cache entry named filecontents
            GetCache();

            // Check whether the cache entry named filecontents exists
            if (FileContents == null)
            {
                // Create a new CacheItemPolicy object that expires after 10 seconds
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(timeOut)
                };

                // Read the contents of the text file
                GetFile();

                // Insert the contents of the file into the cache object
                _cache.Set("filecontents", FileContents, policy);
            }
        }

        private void Back()
        {
            Navigate(new MainPageViewModel());
        }
    }
    
}
