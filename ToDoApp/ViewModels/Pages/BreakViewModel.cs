/// <summary>
/// Representa o view model para a funcionalidade do cronômetro de intervalo na aplicação.
/// Este view model é responsável por gerenciar o cronômetro regressivo e atualizar o tempo restante do intervalo.
/// </summary>
using Microsoft.Toolkit.Uwp.Notifications;
using System.ComponentModel;
using System.Timers;
using ToDoApp.Services;

namespace ToDoApp.ViewModels.Pages
{
    //Classe responsável pela exibição do timer de descanso das atividades do aluno
    public partial class BreakViewModel : BaseViewModel
    {
        private System.Timers.Timer countdownTimer; // Timer que decrementa o tempo a cada segundo.
        private TimeSpan timeRemaining; // Variável que armazena o tempo restante do intervalo.

        public BreakViewModel():this(true, true) { }

        // Construtor que inicializa o timer e inscreve-se para mudanças no SettingsService.
        public BreakViewModel(bool resetAutomatico, bool habilitado)
        {
            // Subscribe to changes in the SettingsService
            SettingsService.Instance.PropertyChanged += OnSettingsServicePropertyChanged;
            UpdateTimeRemaining(); // Initialize timeRemaining
            UpdateTimePassed(timeRemaining); // Initialize TimePassed

            // Cria um timer que "ticks" a cada 1 segundo
            countdownTimer = new System.Timers.Timer(1000); // 1000 ms = 1 segundo
            countdownTimer.Elapsed += OnTimedEvent;
            countdownTimer.AutoReset = resetAutomatico;
            countdownTimer.Enabled = habilitado;
        }

        // Método que atualiza o tempo restante quando uma propriedade relevante do SettingsService é alterada.
        private void OnSettingsServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsService.ChosenTime) || e.PropertyName == nameof(SettingsService.WorkInProgress) || e.PropertyName == nameof(SettingsService.BreakInProgress))
            {
                UpdateTimeRemaining(); // Update timeRemaining when ChosenTime changes
                UpdateTimePassed(timeRemaining); // Update TimePassed when ChosenTime changes
            }
        }

        // Método que define timeRemaining com base na configuração do intervalo de descanso.
        [RelayCommand]
        internal void UpdateTimeRemaining()
        {
            timeRemaining = TimeSpan.FromMinutes(SettingsService.Instance.ChosenBreak == 0 ? 5 : SettingsService.Instance.ChosenBreak);
            UpdateTimePassed(timeRemaining);
        }

        // Método que decrementa o tempo restante e atualiza a exibição. Se o tempo restante atingir zero, ele chama CompleteSession.
        [RelayCommand]
        private void OnCounterSubtraction()
        {
            if (!SettingsService.Instance.WorkInProgress && timeRemaining.TotalSeconds > 0)
            {
                // Subtrai um segundo do timer
                timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1));

                // Passa o valor formatado para a variável que será apresentada no wpf
                if (timeRemaining.TotalSeconds >= 0)
                    UpdateTimePassed(timeRemaining);
                SettingsService.Instance.BreakInProgress = true;
            }
            else if (timeRemaining.TotalSeconds <= 0)
            {
                // Para o timer quando antigirmos o total de segundos 0
                CompleteSession();
            }
        }

        // Método que para o timer e chama o método CompleteSession da classe base
        internal override void CompleteSession()
        {
            countdownTimer.Stop();
            base.CompleteSession();
        }

        // Método que inicia ou reinicia o timer dependendo do estado atual
        [RelayCommand]
        private void StartTimer()
        {
            if (!SettingsService.Instance.WorkInProgress)
            {
                if (timeRemaining.TotalSeconds <= 0)
                {
                    // Reset the timer only if it was not already started
                    UpdateTimeRemaining();
                    UpdateTimePassed(timeRemaining);
                }
                start = !start;
                if (start)
                {
                    countdownTimer.Start();
                }
            }
        }

        // Manipulador de eventos que chama OnCounterSubtraction a cada tick do timer, se start for verdadeiro.
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (start == true)
            {
                OnCounterSubtraction();
            }
            else
            {
                SettingsService.Instance.BreakInProgress = false;
            }
        }
    }
}
