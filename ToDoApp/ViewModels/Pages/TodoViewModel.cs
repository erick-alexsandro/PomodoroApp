using System.Collections.ObjectModel;
using MongoDB.Driver;
using ToDoApp.Models;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ToDoApp.ViewModels.Pages
{
    //Classe associada a página de tarefas durante o estudo do aluno. Esta classe em específico foi a escolhida em utilizar um banco de dados para
    //armazenar as tarefas. Aqui, o aluno poderá executar funcionalidades CRUD com suas tarefas
    public partial class TodoViewModel : ObservableObject
    {
        private bool value; // Variável auxiliar para marcar se uma tarefa está completa ou não.
        private IMongoCollection<TodoItem> _collection; // Coleção do MongoDB para operações de banco de dados.

        [ObservableProperty]
        private ObservableCollection<TodoItem> todoItems; // Coleção observável de itens de tarefa para atualização automática da interface.

        [ObservableProperty]
        private string newTodo = ""; // Nova tarefa a ser adicionada.

        // Construtor padrão que inicializa a conexão com o banco de dados MongoDB usando um connectionString e um nome de banco de dados.
        public TodoViewModel() : this(
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("MongoDBConnectionString"),
        "PomodoroDatabase")
            {
            }

        // Construtor alternativo que recebe uma connectionString e um databaseName para conexão com o MongoDB.
        public TodoViewModel(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<TodoItem>("TodoList");

            TodoItems = new ObservableCollection<TodoItem>();
            LoadTodoItems(); 
        }

        // Método assíncrono para carregar os itens de tarefa do banco de dados MongoDB na coleção local.
        private async Task LoadTodoItems()
        {
            var filter = Builders<TodoItem>.Filter.Empty;
            var todoItemsList = await _collection.Find(filter).ToListAsync();
            TodoItems.Clear();
            foreach (var item in todoItemsList)
            {
                TodoItems.Add(item);
            }
        }

        // Método para marcar uma tarefa como completa ou incompleta.
        [RelayCommand]
        private void MarkAsComplete(TodoItem item)
        {
            if (item != null)
            {
                value = item.Done;
                value = !value;

                // Atualiza o item no banco de dados se necessário.
                var filter = Builders<TodoItem>.Filter.Eq("_id", item.Id);
                var update = Builders<TodoItem>.Update.Set("done", !value);
                _collection.UpdateOneAsync(filter, update);

                // Atualiza o objeto item.
                item.Done = value;

                // Atualiza a UI.
                var index = TodoItems.IndexOf(item);
                if (index >= 0)
                {
                    TodoItems[index] = item;
                }
            }
        }

        // Método para deletar uma tarefa.
        [RelayCommand]
        private void Delete(TodoItem item)
        {
            if (item != null)
            {
                // Deleta o item no banco de dados.
                var filter = Builders<TodoItem>.Filter
                    .Eq("_id", item.Id);

                _collection.DeleteOneAsync(filter);
                LoadTodoItems();
            }
            
        }

        // Método assíncrono para adicionar uma nova tarefa.
        [RelayCommand]
        private async Task AddNewTodo()
        {
            if (!string.IsNullOrEmpty(NewTodo))
            {
                var document = new TodoItem { Task = NewTodo, Done = false };
                await _collection.InsertOneAsync(document);
                NewTodo = "";
                LoadTodoItems();
            }
        }
    }
}