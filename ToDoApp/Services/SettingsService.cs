using System.ComponentModel;

namespace ToDoApp.Services
{
    // Esta classe atua como um serviço singleton para armazenar o tempo escolhido e notificar mudanças
    public class SettingsService : INotifyPropertyChanged
    {
        // Instância singleton do serviço
        private static SettingsService _instance;
        public static SettingsService Instance => _instance ??= new SettingsService();

        // Campo de suporte para a propriedade ChosenTime
        private double _chosenTime;

        // Propriedade ChosenTime que notifica observadores quando mudada
        public double ChosenTime
        {
            get => _chosenTime;
            set
            {
                if (_chosenTime != value)
                {
                    _chosenTime = value;
                    OnPropertyChanged(nameof(ChosenTime)); // Notifica os observadores da mudança
                }
            }
        }

        // Campo de suporte para a propriedade ChosenBreak
        private double _chosenBreak;

        // Propriedade ChosenBreak que notifica observadores quando mudada
        public double ChosenBreak
        {
            get => _chosenBreak;
            set
            {
                if (_chosenBreak != value)
                {
                    _chosenBreak = value;
                    OnPropertyChanged(nameof(ChosenBreak)); // Notifica os observadores da mudança
                }
            }
        }

        // Campo de suporte para a propriedade WorkInProgress
        private bool _workInProgress;

        // Propriedade WorkInProgress que notifica observadores quando mudada
        public bool WorkInProgress
        {
            get => _workInProgress;
            set
            {
                if (_workInProgress != value)
                {
                    _workInProgress = value;
                    OnPropertyChanged(nameof(_workInProgress)); // Notifica os observadores da mudança
                }
            }
        }

        // Campo de suporte para a propriedade BreakInProgress
        private bool _breakInProgress;

        // Propriedade BreakInProgress que notifica observadores quando mudada
        public bool BreakInProgress
        {
            get => _breakInProgress;
            set
            {
                if (_breakInProgress != value)
                {
                    _breakInProgress = value;
                    OnPropertyChanged(nameof(_breakInProgress)); // Notifica os observadores da mudança
                }
            }
        }

        // Campo de suporte para a propriedade BreakCompleted
        private bool _breakCompleted;

        // Propriedade BreakCompleted que notifica observadores quando mudada
        public bool BreakCompleted
        {
            get => BreakCompleted;
            set
            {
                if (_breakCompleted != value)
                {
                    _breakCompleted = value;
                    OnPropertyChanged(nameof(_breakCompleted)); // Notifica os observadores da mudança
                }
            }
        }

        // Campo de suporte para a propriedade WorkCompleted
        private bool _workCompleted;

        // Propriedade WorkCompleted que notifica observadores quando mudada
        public bool WorkCompleted
        {
            get => WorkCompleted;
            set
            {
                if (_workCompleted != value)
                {
                    _workCompleted = value;
                    OnPropertyChanged(nameof(_workCompleted)); // Notifica os observadores da mudança
                }
            }
        }

        // Evento para notificar mudanças de propriedade
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para acionar o evento PropertyChanged
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Construtor privado para garantir o padrão singleton
        private SettingsService() { }
    }
}
