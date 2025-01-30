using Dapper;
using System.Data.SqlClient;
using ToDoapp.Models;

namespace ToDoapp.Repositories
{
    public class TodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TodoDbConnection");
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Todo>("SELECT * FROM Todos ORDER BY CreatedAt DESC"); 
            }
        }

        public Todo GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Todo>("SELECT * FROM Todos WHERE Id = @id", new { Id = id });
            }
        }

        public void Add (Todo todo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = @"
                    INSERT INTO Todos (Title, Description, IsCompleted, CreatedAt, DueDate, Priority) 
                    VALUES (@Title, @Description, @IsCompleted, @CreatedAt, @DueDate, @Priority)";

                connection.Execute(sql, new
                {
                    todo.Title,
                    todo.Description,
                    IsCompleted = false,
                    CreatedAt = DateTime.Now,
                    todo.DueDate,
                    todo.Priority
                });
            }
        }

        public void Update(Todo todo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string sql = @"
                    UPDATE Todos 
                    SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted, DueDate = @DueDate, Priority = @Priority
                    WHERE Id = @Id";

                connection.Execute(sql, todo);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM Todos WHERE Id = @id", new { Id = id });
            }
        }

    }
}
