using System.Windows;

namespace InlineTextLinkControlExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            DataContext = vm;
            vm.LinkText = @"this string 123456 is an example of a set of links 884555 to the following numbers
401177
155879

998552";
        }
    }
}