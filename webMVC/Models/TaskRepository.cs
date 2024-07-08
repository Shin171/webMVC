using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using webMVC.Models;

namespace webMVC.Repositories
{
    public class TaskRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

     
        public TaskRepository()
        {
            CreateTableIfNotExists();
           //DropTasksTable();
        }

       
        private void CreateTableIfNotExists()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS Tasks (
                        Id INT PRIMARY KEY AUTO_INCREMENT,
                        Task VARCHAR(255) NOT NULL,
                        Progress VARCHAR(50) NOT NULL
                    )", conn);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Task> GetAllTasks()
        {
            var tasks = new List<Task>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM Tasks", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Task
                        {
                            Id = reader.GetInt32("Id"),
                            Tasks = reader.GetString("Task"),
                            Progress = reader.GetString("Progress")
                        });
                    }
                }
            }

            return tasks;
        }

        public void AddTask(Task task)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("INSERT INTO Tasks (Task, Progress) VALUES (@Task, @Progress)", conn);
                cmd.Parameters.AddWithValue("@Task", task.Tasks);
                cmd.Parameters.AddWithValue("@Progress", task.Progress);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTask(Task task)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE Tasks SET Task = @Task, Progress = @Progress WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Task", task.Tasks);
                cmd.Parameters.AddWithValue("@Progress", task.Progress);
                cmd.Parameters.AddWithValue("@Id", task.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM Tasks WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
        /*public void ResetAutoIncrement()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "SET @max_id = (SELECT MAX(Id) FROM Tasks);";
                query += "SET @stmt = CONCAT('ALTER TABLE Tasks AUTO_INCREMENT = ', @max_id + 1);";
                query += "PREPARE stmt FROM @stmt;";
                query += "EXECUTE stmt;";
                query += "DEALLOCATE PREPARE stmt;";

                using (var command = new MySqlCommand(query, connection))
                {
                   command.ExecuteNonQuery();
                }
            }
        }*/

        public void DropTasksTable()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var query = "DROP TABLE IF EXISTS Claims";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
