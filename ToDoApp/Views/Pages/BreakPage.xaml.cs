using ToDoApp.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace ToDoApp.Views.Pages
{
    public partial class TodoPage : INavigableView<TodoViewModel>
    {
        public TodoViewModel ViewModel { get; }
        public TodoPage(TodoViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
