/// <summary>
/// Representa um ViewModel para a página Inicial, manipulando a funcionalidade do cronômetro de contagem regressiva 
/// e o rastreamento da conclusão da sessão pomodoro.
/// Fornece métodos para atualizar o tempo restante, iniciar/parar o cronômetro e manipular eventos do cronômetro.
/// </summary>

using Microsoft.Toolkit.Uwp.Notifications;
using System.ComponentModel;
using System.Timers;
using ToDoApp.Services;

namespace ToDoApp.ViewModels.Pages
{
    //Classe principal do programa. Responsável pela exibição do timer Pomodoro
    public partial class DashboardViewModel : BaseViewModel
    {
        private System.Timers.Timer countdownTimer; // Timer que decrementa o tempo a cada segundo
        private TimeSpan timeRemaining; // Variável que armazena o tempo restante da sessão pomodoro

        [ObservableProperty]
        private string? messageSession; // Variável observável que armazena a mensagem de conclusão da sessão

        [ObservableProperty]
        private int sessionsCompleted; // Variável observável que armazena o número de sessões concluídas

        public DashboardViewModel():this(true, true) { }

        // Construtor que inicializa o timer e inscreve-se para mudanças no SettingsService.
        public DashboardViewModel(bool resetAutomatico, bool habilitado)
        {
            // Inicializa as propriedades relacionadas ao tempo
            // e assina as alterações de propriedade no SettingsService (Oque permite ser compartilhado com outros arquivos enquanto
            // está mudando ativamente).
            SettingsService.Instance.PropertyChanged += OnSettingsServicePropertyChanged;
            UpdateTimeRemaining();
            UpdateTimePassed(timeRemaining);

            // Cria um timer que "ticks" a cada 1 segundo
            countdownTimer = new System.Timers.Timer(1000); // 1000 ms = 1 segundo
            countdownTimer.Elapsed += OnTimedEvent;
            countdownTimer.AutoReset = resetAutomatico;
            countdownTimer.Enabled = habilitado;
        }

        // Método que atualiza o tempo restante quando uma propriedade relevante do SettingsService é alterada
        private void OnSettingsServicePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Checa se a propriedade que foi mudada é o 'tempo escolhido', o 'trabalho em progresso' ou 'descanso em progresso'.
            if (e.PropertyName == nameof(SettingsService.ChosenTime) || e.PropertyName == nameof(SettingsService.WorkInProgress) || e.PropertyName == nameof(SettingsService.BreakInProgress))
            {
                UpdateTimeRemaining();
                UpdateTimePassed(timeRemaining);
            }
        }

        // Método que define timeRemaining com base na configuração do tempo de trabalho
        [RelayCommand]
        internal void UpdateTimeRemaining()
        {
            // Se o usuário não escolher um período de estudo, o default será de 25 minutos
            timeRemaining = TimeSpan.FromMinutes(SettingsService.Instance.ChosenTime == 0 ? 25 : SettingsService.Instance.ChosenTime);
            UpdateTimePassed(timeRemaining);
        }
         
        // Método que decrementa o tempo restante e atualiza a exibição. Se o tempo restante atingir zero, ele chama CompleteSession.
        [RelayCommand]
        private void OnCounterSubtraction()
        {
            // Caso o intervalo não esteja rodando e o tempo restante esteja maior que 0, o temporazidor do dashboard poderá rodar
            if (!SettingsService.Instance.BreakInProgress && timeRemaining.TotalSeconds > 0)
            {
                timeRemaining = timeRemaining.Subtract(TimeSpan.FromSeconds(1));
                if (timeRemaining.TotalSeconds >= 0)
                    UpdateTimePassed(timeRemaining);
                SettingsService.Instance.WorkInProgress = true;
            }
            // Caso o tempo restante pomodoro esteja menor ou igual à 0, a sessão está completa e o usuário é notificado.
            else if (timeRemaining.TotalSeconds <= 0)
            {
                CompleteSession();
            }
        }

        // Método que para o timer, incrementa o número de sessões concluídas, define a mensagem de conclusão da sessão, e envia uma notificação ao usuário
        internal override void CompleteSession()
        {
            countdownTimer.Stop();
            SessionsCompleted++;
            MessageSession = $"You have completed {SessionsCompleted} sessions.";
            TimePassed = "Work completed!";
            SettingsService.Instance.WorkInProgress = false;
            NotifyUser("Work done!", "Time to relax!");
        }



        // Método que inicia ou reinicia o timer dependendo do estado atual
        [RelayCommand]
        private void StartTimer()
        {
            // Caso o intervalo não esteja rodando, o pomodoro pode ser iniciado (Isto evita que os dois esteja rodando ao mesmo tempo
            if (!SettingsService.Instance.BreakInProgress)
            {
                // Caso o tempo restante seja menor ou igual à 0, o tempo é atualizado/resetado
                if (timeRemaining.TotalSeconds <= 0)
                {
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

        // Manipulador de eventos que chama OnCounterSubtraction a cada tick do timer, se start for verdadeiro
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (start)
            {
                OnCounterSubtraction();
            }
            else
            {
                SettingsService.Instance.WorkInProgress = false;
            }
        }

    }

}