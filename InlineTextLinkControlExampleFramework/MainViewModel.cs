using System.ComponentModel;
using System.Windows.Input;

namespace InlineTextLinkControlExampleFramework
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ICommand linkCommand;
        private string linkText;

        public MainViewModel()
        {
            LinkCommand = new DelegateCommand<int>(OnLinkCommand);
        }

        private void OnLinkCommand(int num)
        {
            // this is the value of the clicked link
        }

        public string LinkText
        {
            get => linkText;
            set
            {
                linkText = value;
                OnPropertyChanged();
            }
        }

        public ICommand LinkCommand
        {
            get => linkCommand;
            set
            {
                linkCommand = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}