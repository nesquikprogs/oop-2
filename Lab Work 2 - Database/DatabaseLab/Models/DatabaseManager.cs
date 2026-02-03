using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DatabaseLab.Models
{
    public class DatabaseManager
    {
        private const string FilePath = "students.json";

        private List<Student> _students = new List<Student>();
        private int _nextId = 1;

        public DatabaseManager()
        {
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                SaveToFile(); 
                return;
            }

            try
            {
                string json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();

                _students = loaded;
                _nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            }
            catch (Exception ex)
            { 
                _students.Clear();
                _nextId = 1;
                Console.WriteLine("Ошибка чтения файла: " + ex.Message);
            }
        }

        private void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_students, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи файла: " + ex.Message);
            }
        }

        public void AddStudent(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
            SaveToFile();
        }

        public List<Student> GetAllStudents()
        {
            return new List<Student>(_students); 
        }

        public void UpdateStudent(Student updated)
        {
            var existing = _students.FirstOrDefault(s => s.Id == updated.Id);
            if (existing != null)
            {
                existing.Name = updated.Name;
                existing.Age = updated.Age;
                existing.Grade = updated.Grade;
                existing.Email = updated.Email;
                SaveToFile();
            }
        }

        public void DeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _students.Remove(student);
                SaveToFile();
            }
        }

        public List<Student> SearchByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return GetAllStudents();

            searchText = searchText.Trim().ToLower();
            return _students
                .Where(s => s.Name.ToLower().Contains(searchText))
                .ToList();
        }

        public List<Student> SortByAge()
        {
            return _students
                .OrderBy(s => s.Age)
                .ToList();
        }

        public bool StudentExists(int id)
        {
            return _students.Any(s => s.Id == id);
        }
    }
}