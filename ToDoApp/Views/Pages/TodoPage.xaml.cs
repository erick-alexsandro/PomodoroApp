using ToDoApp.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace ToDoApp.Views.Pages
{
    public partial class BreakPage : INavigableView<BreakViewModel>
    {
        public BreakViewModel ViewModel { get; }

        public BreakPage(BreakViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
