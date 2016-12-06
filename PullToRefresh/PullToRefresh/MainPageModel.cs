using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PullToRefresh
{public class MainPageModel : FreshMvvm.FreshBasePageModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private Command _loadTweetsCommand;
        private ObservableCollection<Tweet> _tweets = new ObservableCollection<Tweet>();

        public Command LoadTweetsCommand
        {
            get
            {
                return _loadTweetsCommand ?? (_loadTweetsCommand = new Command(ExecuteLoadTweetsCommand, () => !IsBusy));
            }
        }

        public ObservableCollection<Tweet> Tweets
        {
            get { return _tweets; }
            set
            {
                if (Equals(value, _tweets)) return;
                _tweets = value;
                RaisePropertyChanged();
            }
        }

        private async void ExecuteLoadTweetsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadTweetsCommand.ChangeCanExecute();

            await LoadEmUp();

            IsBusy = false;
            LoadTweetsCommand.ChangeCanExecute();
        }

        private int _counter = 2;

        private async Task LoadEmUp()
        {
            await Task.Delay(1000);

            if (Tweets.Any())
            {
                Tweets.Add(new Tweet() { Author = "Dom", Text = "Text " + ++_counter });
            }
            else
            {
                Tweets = new ObservableCollection<Tweet>()
                {
                    new Tweet() { Author = "Dom", Text = "Test" },
                    new Tweet() { Author = "Dom", Text = "Test1" },
                    new Tweet() { Author = "Dom", Text = "Test2" }
                };
            }
        }
    }
}