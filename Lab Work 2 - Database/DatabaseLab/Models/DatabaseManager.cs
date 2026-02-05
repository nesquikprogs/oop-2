using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using DatabaseLab.Models;


namespace DatabaseLab.Models
{
    /// <summary>
    /// Класс для управления базой данных студентов.
    /// Теперь хранение осуществляется только в коллекции ObservableCollection<Student> в ViewModel.
    /// DatabaseManager отвечает только за чтение и запись данных в JSON-файл.
    /// </summary>
    public class DatabaseManager
    {
        /// <summary>
        /// Путь к файлу JSON.
        /// </summary>
        private string FilePath = "students.json";

        /// <summary>
        /// Устанавливает путь к файлу базы данных.
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public void SetFilePath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                FilePath = path;
        }

        /// <summary>
        /// Загружает студентов из файла JSON в ObservableCollection.
        /// </summary>
        /// <returns>Коллекция студентов</returns>
        public ObservableCollection<Student> LoadFromFile()
        {
            if (!File.Exists(FilePath))
                return new ObservableCollection<Student>();

            try
            {
                string json = File.ReadAllText(FilePath);
                var students = JsonSerializer.Deserialize<ObservableCollection<Student>>(json);
                return students ?? new ObservableCollection<Student>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ObservableCollection<Student>();
            }
        }

        /// <summary>
        /// Сохраняет коллекцию студентов в JSON-файл.
        /// </summary>
        /// <param name="students">Коллекция студентов</param>
        public void SaveToFile(ObservableCollection<Student> students)
        {
            try
            {
                string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
