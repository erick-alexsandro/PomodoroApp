# Visão Geral
O PomodoroApp é uma aplicação que ajuda usuários a gerenciar suas tarefas e sessões de estudo usando a técnica Pomodoro. Ele inclui funcionalidades para criar, editar e completar tarefas, além de controlar o tempo de trabalho e pausas.
## Estrutura do Código
Aqui está uma visão geral das classes principais e suas funcionalidades:

### 1. DashboardViewModel

Responsável pela funcionalidade do cronômetro de contagem regressiva durante as sessões de trabalho do Pomodoro. Manipula eventos de cronômetro, atualiza o tempo restante e notifica a conclusão das sessões.

Métodos Principais:

**UpdateTimeRemaining():** Atualiza o tempo restante com base nas configurações escolhidas pelo usuário.

**OnCounterSubtraction():** Decrementa o tempo restante durante uma sessão de trabalho e notifica quando a sessão é concluída.

**CompleteSession():** Interrompe o cronômetro e marca a sessão de trabalho como concluída.

**StartTimer():** Inicia ou reinicia o cronômetro.

### 2. BreakViewModel

Gerencia a funcionalidade do cronômetro durante os intervalos de descanso entre as sessões de trabalho do Pomodoro.

Métodos Principais:

**UpdateTimeRemaining():** Atualiza o tempo restante com base nas configurações de intervalo escolhidas pelo usuário.

**OnCounterSubtraction():** Decrementa o tempo restante durante um intervalo de descanso e notifica quando o intervalo é concluído.

**CompleteSession():** Interrompe o cronômetro de intervalo e marca o intervalo como concluído.

**StartTimer():** Inicia ou reinicia o cronômetro de intervalo.

### 3. TodoViewModel

Gerencia a lista de tarefas do usuário, permitindo operações CRUD (Criar, Ler, Atualizar, Deletar) através da interação com um banco de dados MongoDB.

Métodos Principais:

**LoadTodoItems():** Carrega as tarefas do banco de dados e atualiza a coleção observável.

**MarkAsComplete(TodoItem item):** Marca uma tarefa como concluída, atualizando o banco de dados e a interface do usuário.

**Delete(TodoItem item):** Exclui uma tarefa do banco de dados e atualiza a lista de tarefas.

**AddNewTodo():** Adiciona uma nova tarefa ao banco de dados e atualiza a lista de tarefas.

*Obs : É necessário um arquivo appsettings.json para configurar o banco de dados MongoDB.*

### 4. SettingsViewModel

Controla as configurações do aplicativo, como tempo dos timers Pomodoro e temas de aparência.

Métodos Principais:

**OnChangeMinutes():** Atualiza o tempo de trabalho e notifica outras partes do aplicativo.

**OnChangeBreak():** Atualiza o tempo de intervalo e notifica outras partes do aplicativo.

**OnChangeTheme(string parameter):** Altera o tema do aplicativo entre claro e escuro.

**InitializeViewModel():** Inicializa o estado inicial do ViewModel ao navegar para a página de configurações.

### 5. SettingsService

Serve como um serviço singleton para armazenar e notificar mudanças nas configurações do aplicativo.

Propriedades Principais:

**ChosenTime**: Armazena e notifica mudanças no tempo escolhido para as sessões de trabalho.

**ChosenBreak**: Armazena e notifica mudanças no tempo escolhido para os intervalos de descanso.

**WorkInProgress**, **BreakInProgress**: Indicam se uma sessão de trabalho ou intervalo de descanso está em andamento.

**WorkCompleted**, **BreakCompleted**: Indicam se uma sessão de trabalho ou intervalo de descanso foi concluído.

Métodos Principais:

**OnPropertyChanged(string propertyName):** Método para notificar mudanças de propriedade aos observadores.

## Conclusão

O PomodoroApp utiliza essas classes para oferecer uma experiência de gerenciamento de tarefas eficiente, combinando técnicas de produtividade como o método Pomodoro com funcionalidades avançadas de persistência de dados através do MongoDB. Cada classe e método desempenha um papel crucial na funcionalidade geral da aplicação, fornecendo aos usuários as ferramentas necessárias para melhorar sua produtividade e gerenciar suas tarefas de maneira eficaz.
