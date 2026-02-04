using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows; 
using DatabaseLab.Interfaces; 

namespace DatabaseLab.Models
{
    /// <summary>
    /// Класс для управления базой данных студентов, реализующий интерфейс IStudentRepository. В модели MVVM представляет Model.
    /// Данные хранятся в файле JSON с именем "students.json".
    /// </summary>
    /// <remarks>
    /// Формат файла JSON:
    /// Файл содержит массив объектов Student. Каждый объект описывает одного студента.
    /// [
    ///   {
    ///     "Id": 1,          // уникальный идентификатор студента (int)
    ///     "Name": "Александров Данил",  // имя студента (string)
    ///     "Age": 22,        // возраст студента (int)
    ///     "Grade": 5,       // оценка студента, целое число от 2 до 5 (int)
    ///     "Email": "Nevsquik.2301@mail.ru"  // email студента (string)
    ///   },
    ///   {
    ///     "Id": 2,
    ///     "Name": "Рычков Родион",
    ///     "Age": 22,
    ///     "Grade": 5,
    ///     "Email": "wuffluv@mail.ru"
    ///   }
    /// ]
    /// </remarks>
    public class DatabaseManager : IStudentRepository
    {

        #region Поля

        /// <summary>
        /// Поле с именем файла для хранения данных студентов в формате JSON.
        /// </summary>
        private string FilePath = "students.json";

        /// <summary>
        /// Поле для хранения списка студентов.
        /// </summary>
        private List<Student> _students = new List<Student>();

        /// <summary>
        /// Поле для хранения следующего уникального идентификатора студента.
        /// </summary>
        private int _nextId = 1;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор класса DatabaseManager. Загружает данные из файла при создании экземпляра.
        /// </summary>
        public DatabaseManager()
        {
            LoadFromFile(); 
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод для выбора пути к файлу базы данных.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        public void SetFilePath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                FilePath = path;
                LoadFromFile(); 
            }
        }

        /// <summary>
        /// Метод для загрузки данных студентов из файла JSON.
        /// </summary>
        private void LoadFromFile()
        {
            if (!File.Exists(FilePath)) // Если файл не существует
            {
                SaveToFile(); // Создаем пустой файл
                return;
            }
            try
            {
                string json = File.ReadAllText(FilePath); // Читаем содержимое файла
                var loadedlList = JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>(); // Десериализуем JSON в список студентов (List<Student>)
                _students = loadedlList;
                _nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1; // Обновляем следующий идентификатор
            }
            catch (Exception ex)
            {
                _students.Clear(); // Очистка списка во избежание битых данных
                _nextId = 1; // Сброс идентификатора
                MessageBox.Show("Ошибка чтения файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод для сохранения данных студентов в файл JSON.
        /// </summary>
        private void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_students, new JsonSerializerOptions { WriteIndented = true }); // Сериализуем список студентов в JSON
                File.WriteAllText(FilePath, json); // Записываем JSON в файл
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // Сообщение об ошибке записи файла
            }
        }

        /// <summary>
        /// Метод для добавления нового студента в базу данных.
        /// </summary>
        /// <param name="student">Объект студента, которого хотим добавить в базу данных.</param>
        public void AddStudent(Student student)
        {
            student.Id = _nextId++; // Присваиваем уникальный идентификатор и увеличиваем счетчик
            _students.Add(student); // Добавляем студента в список(list)
            SaveToFile(); // Сохраняем студента в файл
        }

        /// <summary>
        /// Метод для получения всех студентов из базы данных.
        /// </summary>
        /// <returns>Всех копию списка всех студентов.</returns>
        public List<Student> GetAllStudents()
        {
            return new List<Student>(_students); 
        }

        /// <summary>
        /// Метод для обновления информации о студенте в базе данных.
        /// </summary>
        /// <param name="updated">Объект измененного студента.</param>
        public void UpdateStudent(Student updated)
        {
            var existing = _students.FirstOrDefault(s => s.Id == updated.Id); // Находим существующего студента по Id
            if (existing != null) // Если студент найден
            {
                // Обновляем поля студента
                existing.Name = updated.Name;
                existing.Age = updated.Age;
                existing.Grade = updated.Grade;
                existing.Email = updated.Email;
                SaveToFile(); // 
            }
        }

        /// <summary>
        /// Метод для удаления студента из базы данных по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор студента, которого нужно удалить.</param>
        public void DeleteStudent(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id); // Находим первого студента по совпадающему Id
            if (student != null) // Если студент найден
            {
                _students.Remove(student); // Удаляем студента из списка
                SaveToFile(); // Сохраняем изменения в файл
            }
        }

        /// <summary>
        /// Метод для поиска студентов по имени.
        /// </summary>
        /// <param name="searchText">Икомый текст</param>
        /// <returns>Список объектов student с подходящим именем.</returns>
        public List<Student> SearchByName(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) // Если искомый текст null или состоит только из пробелов
                return GetAllStudents(); // Возвращаем всех студентов
            searchText = searchText.Trim().ToLower(); // Подстравляем искомый текст к виду, удобному для сравнения
            return _students.Where(s => s.Name.ToLower().Contains(searchText)).ToList(); // Возвращаем список студентов, имена которых содержат искомый текст
        }

        /// <summary>
        /// Метод для сортировки студентов по возрасту.
        /// </summary>
        /// <returns>Список list, отсортированный по возрасту.</returns>
        public List<Student> SortByAge()
        {
            return _students.OrderBy(s => s.Age).ToList(); // Возвращаем список студентов (именно list<student>), отсортированный по критерию возраста
        }

        #endregion

        // сделаать подскахски для полей ввода явными - ready
        // выбор мия файла json - ready
        // определить где данные выгружаются в grid - с помощью привязок благодаря использовании коллекции student - ready
        // проверить отношения между mainviewmodule c mainwondow и interface

        // сделать чтобы все хранилось не в list и в коллекции, а только в коллекции
        // обновить uml
    }
}