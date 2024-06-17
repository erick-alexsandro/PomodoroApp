using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using System.Threading;
using System.Timers;
using ToDoApp.Services;

namespace ToDoApp.ViewModels.Pages
{
    //Classe responsável pela página de configurações. Responsável em exibir as funcionalidades que podem ser alteradas, como o tempo dos timers e o tema do programa
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false; // Flag para controlar se o ViewModel foi inicializado.
        private DashboardViewModel dashboardViewModel; // Referência ao ViewModel da página principal.
        private BreakViewModel breakViewModel; // Referência ao ViewModel do timer de descanso.

        public SettingsViewModel(DashboardViewModel dashboard, BreakViewModel breakView)
        {
            dashboardViewModel = dashboard;
            breakViewModel = breakView;
        }
            
        [ObservableProperty]
        private string _appVersion = ""; // Versão atual da aplicação.

        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Unknown; // Tema atual da aplicação.

        [ObservableProperty]
        private string? userMinutes; // Minutos definidos pelo usuário (pode ser nulo inicialmente para evitar erros).


        [ObservableProperty]
        private string? userBreak; // Intervalo definido pelo usuário (pode ser nulo inicialmente para evitar erros).

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel(); // Inicializa o ViewModel quando navegado para a página.
        }
         
        public void OnNavigatedFrom() { } // Método chamado quando navegando para fora da página.
         
        private void InitializeViewModel()
        {
            CurrentTheme = ApplicationThemeManager.GetAppTheme(); // Obtém e define o tema atual da aplicação.
            AppVersion = $"Version 0.0.1 (Beta) - By Erick Alexsandro"; // Define a versão da aplicação.

            _isInitialized = true; // Marca o ViewModel como inicializado.
        }


        [RelayCommand]
        private void OnChangeMinutes()
        {
            if (double.TryParse(UserMinutes, out double minutes) && minutes > 0)
            {
                SettingsService.Instance.ChosenTime = minutes; // Atualiza o tempo escolhido para o timer principal.
                dashboardViewModel.UpdateTimeRemaining(); // Atualiza o tempo restante na página principal.
            } 
        }

        [RelayCommand]
        private void OnChangeBreak()
        {
            if (double.TryParse(UserBreak, out double minutes) && minutes > 0)
            {
                SettingsService.Instance.ChosenBreak = minutes; // Atualiza o tempo escolhido para o timer de pausa.
                breakViewModel.UpdateTimeRemaining(); // Atualiza o tempo restante no timer de pausa.
            } 
        }

        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == ApplicationTheme.Light) 
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Light); // Aplica o tema claro.
                    CurrentTheme = ApplicationTheme.Light; // Define o tema atual como claro.

                    break;

                default:
                    if (CurrentTheme == ApplicationTheme.Dark)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Dark);  // Aplica o tema escuro.
                    CurrentTheme = ApplicationTheme.Dark; // Define o tema atual como escuro.

                    break;
            }
        }
    }
}
