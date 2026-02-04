using DatabaseLab.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System;
using System.Collections.Generic;

namespace DatabaseLab.ViewModels
{
    /// <summary>
    /// Класс ViewModel для главного окна приложения. В модели MVVM представляет ViewModel. 
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Поля

        /// <summary>
        /// Поле для доступа к репозиторию студентов.
        /// </summary>
        private readonly DatabaseLab.Interfaces.IStudentRepository _db;

        /// <summary>
        /// Свойство для хранения списка студентов.
        /// </summary>
        public ObservableCollection<Student> Students { get; } = new ObservableCollection<Student>();

        /// <summary>
        /// Поле для хранения текущего студента.
        /// </summary>
        private Student _currentStudent = new Student();

        /// <summary>
        /// Свойство для доступа к текущему студенту.
        /// </summary>
        public Student CurrentStudent
        {
            get => _currentStudent; // Геттер
            set // Сеттер
            {
                _currentStudent = value ?? new Student();
                // Обновление всех связанных свойств
                OnPropertyChanged(nameof(CurrentStudent));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(AgeText));
                OnPropertyChanged(nameof(GradeText));
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Свойство для доступа к имени текущего студента.
        /// </summary>
        public string Name
        {
            get => CurrentStudent.Name; // Геттер
            set { CurrentStudent.Name = value ?? ""; OnPropertyChanged(nameof(Name)); } // Сеттер
        }

        /// <summary>
        /// Свойство для доступа к возрасту текущего студента в текстовом формате.
        /// </summary>
        public string AgeText
        {
            get => CurrentStudent.Age.ToString(); // Геттер
            set // Сеттер   
            {
                if (int.TryParse(value, out int age)) // Попытка преобразования строки в целое число
                    CurrentStudent.Age = age; 
                OnPropertyChanged(nameof(AgeText)); // Уведомление об изменении свойства
            }
        }

        /// <summary>
        /// Свойство для доступа к оценке текущего студента в текстовом формате.
        /// </summary>

        public string GradeText
        {
            get => CurrentStudent.Grade.ToString(); // Геттер
            set // Сеттер
            {
                string cleaned = value?.Replace(".", ",") ?? ""; // Подготовка строки для парсинга
                if (int.TryParse(cleaned, out int grade)) // Попытка преобразования строки в int
                    CurrentStudent.Grade = grade; 
                OnPropertyChanged(nameof(GradeText)); // Уведомление об изменении свойства
            }
        }

        /// <summary>
        /// Свойство для доступа к email текущего студента.
        /// </summary>
        public string Email
        {
            get => CurrentStudent.Email; // Геттер
            set { CurrentStudent.Email = value?.Trim() ?? ""; OnPropertyChanged(nameof(Email)); } // Сеттер
        }

        /// <summary>
        /// Поле для хранения текста поиска, который заведомо пуст
        /// </summary>
        private string _searchText = "";

        /// <summary>
        /// Свойство для доступа к тексту поиска.
        /// </summary>
        public string SearchText
        {
            get => _searchText; // Геттер
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); } // Сеттер
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор класса MainViewModel.
        /// </summary>
        public MainViewModel()
        {
            _db = new DatabaseLab.Models.DatabaseManager();  // Инициализация менеджера базы данных
            LoadStudents(); // Загрузка студентов из базы данных
        }

        #endregion

        #region Методы

        /// <summary>
        /// Метод для загрузки всех студентов из базы данных в коллекцию Students.
        /// </summary>
        private void LoadStudents()
        {
            Students.Clear(); // Очистка текущей коллекции студентов
            foreach (var s in _db.GetAllStudents()) // Получение всех студентов из базы данных (из list)
                Students.Add(s); // Добавление каждого студента в коллекцию 
        }

        /// <summary>
        /// Мецод для добавления нового студента.
        /// </summary>
        public void AddStudent()
        {
            if (!ValidateCurrentStudent(out string error)) // Валидация текущего студента(дополнительно возвращает ошибку)
            {
                MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning); // Показ сообщения об ошибке
                return;
            }

            _db.AddStudent(CurrentStudent); // Добавление текущего студента в базу данных
            LoadStudents(); // Перезагрузка списка студентов в коллекцию
            ClearCurrentStudent(); // Очистка текущего студента
        }

        /// <summary>
        /// Метод для обновления информации о текущем студенте.
        /// </summary>
        public void UpdateStudent()
        {
            if (CurrentStudent.Id == 0) return; // Проверка, что студент существует

            if (!ValidateCurrentStudent(out string error)) // Валидация текущего студента(дополнительно возвращает ошибку)
            {
                MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning); // Показ сообщения об ошибке
                return;
            }

            _db.UpdateStudent(CurrentStudent); // Обновление информации о текущем студенте в базе данных
            LoadStudents(); // Перезагрузка списка студентов в коллекцию
            ClearCurrentStudent(); // Очистка текущего студента
        }

        /// <summary>
        /// Метод для удаления студента.
        /// </summary>
        /// <param name="student">Объект студента.</param>
        public void DeleteStudent(Student student)
        {
            if (student == null) return; // Проверка, что студент не null

            var result = MessageBox.Show($"Удалить студента {student.Name}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question); // Подтверждение удаления студента

            if (result == MessageBoxResult.Yes) // Если пользователь подтвердил удаление
            {
                _db.DeleteStudent(student.Id); // Удаление студента из базы данных
                LoadStudents(); // Перезагрузка списка студентов в коллекцию
                if (CurrentStudent.Id == student.Id)
                    ClearCurrentStudent(); // Очистка текущего студента, если он был удален из коллекции
            }
        }

        /// <summary>
        /// Метод для поиска студентов по имени.
        /// </summary>
        public void Search()
        {
            Students.Clear(); // Очистка текущей коллекции студентов
            var results = _db.SearchByName(SearchText); // Поиск студентов по имени в базе данных
            foreach (var s in results)
                Students.Add(s); // Добавление найденных студентов в коллекцию
        }

        /// <summary>
        /// Метод для сортировки студентов по возрасту.
        /// </summary>
        public void SortByAge()
        {
            Students.Clear(); // Очистка текущей коллекции студентов
            var sorted = _db.SortByAge();  // Получение отсортированного списка студентов по возрасту из базы данных
            foreach (var s in sorted)
                Students.Add(s); // Добавление отсортированных студентов в коллекцию
        }

        /// <summary>
        ///  Метод для сброса поиска и перезагрузки всех студентов.
        /// </summary>
        public void Refresh() 
        {
            SearchText = ""; // Сброс текста поиска
            LoadStudents(); // Перезагрузка всех студентов из базы данных
        }

        /// <summary>
        /// Метод для валидации текущего студента.
        /// </summary>
        /// <param name="error">Текст ошибки при неудачной валидации.</param>
        /// <returns>Факт непройденной или нет валидации и текст ошибки в error</returns>
        private bool ValidateCurrentStudent(out string error)
        {
            error = ""; // Инициализация текста ошибки

            if (string.IsNullOrWhiteSpace(CurrentStudent.Name)) // Проверка, что имя не пустое
            {
                error = "Имя обязательно";
                return false;
            }

            if (CurrentStudent.Age <= 0) // Проверка, что возраст положительный
            {
                error = "Возраст должен быть положительным";
                return false;
            }

            if (CurrentStudent.Grade < 2 || CurrentStudent.Grade > 5) // Проверка, что оценка в диапазоне от 2 до 5
            {
                error = "Оценка должна быть от 2 до 5";
                return false;
            }

            if (!string.IsNullOrEmpty(CurrentStudent.Email) && !CurrentStudent.Email.Contains("@")) // Проверка корректности email
            {
                error = "Некорректный email";
                return false;
            }

            return true; // Валидация пройдена успешно
        }

        /// <summary>
        /// Метод для очистки текущего студента.
        /// </summary>
        private void ClearCurrentStudent()
        {
            CurrentStudent = new Student();
        }

        /// <summary>
        /// Событие для уведомления об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; // Обязательное объявление события

        /// <summary>
        /// Метод для вызова события PropertyChanged.
        /// </summary>
        /// <param name="propertyName">Имя string измененного свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Вызов события
        }

        #endregion

        // todo:

    }
}