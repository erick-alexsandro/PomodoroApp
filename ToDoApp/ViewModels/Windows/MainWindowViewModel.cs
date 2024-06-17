using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace ToDoApp.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "PomodoroApp";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Work",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Clock24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Break",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Bed24 },
                TargetPageType = typeof(Views.Pages.BreakPage)
            },
            new NavigationViewItem()
            {
                Content = "Todo",
                Icon = new SymbolIcon { Symbol = SymbolRegular.TaskListLtr20 },
                TargetPageType = typeof(Views.Pages.TodoPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

    }
}
