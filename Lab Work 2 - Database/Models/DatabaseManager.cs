using System;
using System.Collections.Generic;
using System.Data.SQLite; // Для работы с SQLite
using Dapper; // Для простых запросов

namespace DatabaseLab.Models
{
    public class DatabaseManager
    {
        private string connectionString = "Data Source=students.db;Version=3;"; // Путь к файлу БД

        public DatabaseManager()
        {
            CreateDatabase(); 
        }

        private void CreateDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Students (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Age INTEGER,
                        Grade REAL,
                        Email TEXT
                    );";
                connection.Execute(createTableQuery);
            }
        }

        // Добавление данных
        public void AddStudent(Student student)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Students (Name, Age, Grade, Email) VALUES (@Name, @Age, @Grade, @Email);";
                connection.Execute(insertQuery, student);
            }
        }

        // Получение всех данных
        public List<Student> GetAllStudents()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Students;";
                return connection.Query<Student>(selectQuery).AsList();
            }
        }

        // Изменение данных
        public void UpdateStudent(Student student)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Students SET Name = @Name, Age = @Age, Grade = @Grade, Email = @Email WHERE ID = @ID;";
                connection.Execute(updateQuery, student);
            }
        }

        // Удаление данных
        public void DeleteStudent(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Students WHERE ID = @ID;";
                connection.Execute(deleteQuery, new { ID = id });
            }
        }

        // Поиск по имени 
        public List<Student> SearchByName(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string searchQuery = "SELECT * FROM Students WHERE Name LIKE @Name;";
                return connection.Query<Student>(searchQuery, new { Name = "%" + name + "%" }).AsList();
            }
        }

        // Сортировка по возрасту 
        public List<Student> SortByAge()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sortQuery = "SELECT * FROM Students ORDER BY Age ASC;";
                return connection.Query<Student>(sortQuery).AsList();
            }
        }

        // Проверка данных 
        public bool StudentExists(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string checkQuery = "SELECT COUNT(*) FROM Students WHERE ID = @ID;";
                int count = connection.ExecuteScalar<int>(checkQuery, new { ID = id });
                return count > 0;
            }
        }
    }
}