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

    }
}