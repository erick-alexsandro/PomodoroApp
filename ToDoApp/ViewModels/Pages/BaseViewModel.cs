/// <summary>
/// Fornece uma implementação base para view models na aplicação.
/// </summary>
/// <remarks>
/// A classe `BaseViewModel` é uma classe abstrata que fornece funcionalidades comuns para os view models na aplicação. 
/// Ela inclui propriedades e métodos para gerenciar a exibição de informações relacionadas ao tempo, como o tempo decorrido durante uma sessão, 
/// e para notificar o usuário sobre eventos, como a conclusão de um intervalo.
/// </remarks>

using System;
using System.ComponentModel;
using System.Timers;
using Microsoft.Toolkit.Uwp.Notifications;
using ToDoApp.Services;

namespace ToDoApp.ViewModels.Pages
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        // Variável observável que armazena o tempo passado formatado como string
        [ObservableProperty]
        private string timePassed;

        // Variável booleana que indica se o timer está ativo.
        protected bool start;

        protected BaseViewModel() : this(false) { }

        protected BaseViewModel(bool iniciado)
        {
            start = iniciado;
        }

        // Método que envia uma notificação toast para o usuário com um título e mensagem específicos
        internal void NotifyUser(string title, string message)
        {
            new ToastContentBuilder()
                .AddText(title)
                .AddText(message)
                .SetToastScenario(ToastScenario.IncomingCall)
                .Show();
        }

        // Método que atualiza a variável timePassed com o tempo restante formatado
        internal void UpdateTimePassed(TimeSpan timeRemaining)
        {
            TimePassed = $"{timeRemaining.Minutes} minutes : {timeRemaining.Seconds} seconds";
        }

        // Método virtual que é chamado para completar a sessão. Ele define start como falso, atualiza timePassed para "Break completed!", e envia uma notificação ao usuário
        internal virtual void CompleteSession()
        {
            TimePassed = "Break completed!";
            SettingsService.Instance.BreakInProgress = false;
            NotifyUser("Break done!", "Time to get back to work!");
        }
    }
}
