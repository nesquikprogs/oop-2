using DatabaseLab.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System;

namespace DatabaseLab.ViewModels
{
    /// <summary>
    /// ViewModel для главного окна. Хранит базу данных студентов только в ObservableCollection.
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Поля и свойства

        /// <summary>
        /// Поле заголовка окна.
        /// </summary>
        private string _windowTitle = "База студентов";

        /// <summary>
        /// Свойство заголовка окна.
        /// </summary>
        public string WindowTitle
        {
            get => _windowTitle;
            set { _windowTitle = value; OnPropertyChanged(nameof(WindowTitle)); }
        }

        /// <summary>
        /// Коллекция студентов.
        /// </summary>
        public ObservableCollection<Student> Students { get; private set; }
            = new ObservableCollection<Student>();

        /// <summary>
        /// Поле текущего студента для добавления/редактирования.
        /// </summary>
        private Student _currentStudent = new Student { Age = 0, Grade = 0 };

        /// <summary>
        /// Свойство текущего студента для добавления/редактирования.
        /// </summary>
        public Student CurrentStudent
        {
            get => _currentStudent;
            set
            {
                _currentStudent = value ?? new Student();
                OnPropertyChanged(nameof(CurrentStudent));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(AgeText));
                OnPropertyChanged(nameof(GradeText));
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Свойство имени текущего студента.
        /// </summary>
        public string Name
        {
            get => CurrentStudent.Name;
            set { CurrentStudent.Name = value ?? ""; OnPropertyChanged(nameof(Name)); }
        }

        /// <summary>
        /// Свойство возраста текущего студента в текстовом формате.
        /// </summary>
        public string AgeText
        {
            get => CurrentStudent.Age == 0 ? "" : CurrentStudent.Age.ToString();
            set
            {
                if (int.TryParse(value, out int age)) CurrentStudent.Age = age;
                else CurrentStudent.Age = 0;
                OnPropertyChanged(nameof(AgeText));
            }
        }

        /// <summary>
        /// Свойство оценки текущего студента в текстовом формате.
        /// </summary>
        public string GradeText
        {
            get => CurrentStudent.Grade == 0 ? "" : CurrentStudent.Grade.ToString();
            set
            {
                if (int.TryParse(value, out int grade)) CurrentStudent.Grade = grade;
                else CurrentStudent.Grade = 0;
                OnPropertyChanged(nameof(GradeText));
            }
        }

        /// <summary>
        /// Свойство электронной почты текущего студента.
        /// </summary>
        public string Email
        {
            get => CurrentStudent.Email;
            set { CurrentStudent.Email = value?.Trim() ?? ""; OnPropertyChanged(nameof(Email)); }
        }

        /// <summary>
        /// Свойство текста поиска студентов по имени.
        /// </summary>
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        /// <summary>
        /// Поле для управления базой данных.
        /// </summary>
        private readonly DatabaseManager _db;

        #endregion

        #region Конструктор

        public MainViewModel()
        {
            _db = new DatabaseManager();  // Для работы с файлом
        }

        #endregion

        #region Методы работы с файлом

        /// <summary>
        /// Метод установки пути к файлу базы данных и загрузки студентов из него.
        /// </summary>
        public void SetFilePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return;

            _db.SetFilePath(path);
            LoadStudents();
            WindowTitle = $"База студентов — {System.IO.Path.GetFileName(path)}";
        }

        /// <summary>
        /// Метод загрузки коллекции студентов из файла
        /// </summary>
        public void LoadStudents()
        {
            Students.Clear();
            var loaded = _db.LoadFromFile(); // возвращает ObservableCollection<Student>
            foreach (var s in loaded)
                Students.Add(s);
        }

        /// <summary>
        /// Метод сохранения коллекции студентов в файл
        /// </summary>
        private void SaveStudents()
        {
            _db.SaveToFile(Students);
        }

        #endregion

        #region Методы CRUD

        /// <summary>
        /// Метод добавления нового студента.
        /// </summary>
        public void AddStudent()
        {
            if (!ValidateCurrentStudent(out string error))
            {
                MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CurrentStudent.Id = GetNextId();
            Students.Add(new Student
            {
                Id = CurrentStudent.Id,
                Name = CurrentStudent.Name,
                Age = CurrentStudent.Age,
                Grade = CurrentStudent.Grade,
                Email = CurrentStudent.Email
            });

            SaveStudents();
            ClearCurrentStudent();
        }

        /// <summary>
        /// Метод обновления информации о студенте.
        /// </summary>
        public void UpdateStudent()
        {
            if (CurrentStudent.Id == 0) return;

            if (!ValidateCurrentStudent(out string error))
            {
                MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var student = Students.FirstOrDefault(s => s.Id == CurrentStudent.Id);
            if (student != null)
            {
                student.Name = CurrentStudent.Name;
                student.Age = CurrentStudent.Age;
                student.Grade = CurrentStudent.Grade;
                student.Email = CurrentStudent.Email;
            }

            SaveStudents();
            ClearCurrentStudent();
        }

        /// <summary>
        /// Метод удаления студента.
        /// </summary>
        /// <param name="student">Объект нужного студента</param>
        public void DeleteStudent(Student student)
        {
            if (student == null) return;

            var result = MessageBox.Show($"Удалить студента {student.Name}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Students.Remove(student);
                SaveStudents();
                if (CurrentStudent.Id == student.Id) ClearCurrentStudent();
            }
        }

        #endregion

        #region Поиск и сортировка

        /// <summary>
        /// Метод поиска студентов по имени.
        /// </summary>
        public void Search()
        {
            var filtered = Students
                .Where(s => s.Name.ToLower().Contains(SearchText.Trim().ToLower()))
                .ToList();

            Students.Clear();
            foreach (var s in filtered) Students.Add(s);
        }

        /// <summary>
        /// Метод сортировки студентов по возрасту.
        /// </summary>
        public void SortByAge()
        {
            var sorted = Students.OrderBy(s => s.Age).ToList();
            Students.Clear();
            foreach (var s in sorted) Students.Add(s);
        }

        /// <summary>
        /// Метод сброса поиска и обновления списка студентов.
        /// </summary>
        public void Refresh()
        {
            SearchText = "";
            LoadStudents();
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Метод валидации данных текущего студента.
        /// </summary>
        /// <param name="error">Текст ошибки.</param>
        /// <returns>Результат валидации.</returns>
        private bool ValidateCurrentStudent(out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(CurrentStudent.Name))
            {
                error = "Имя обязательно";
                return false;
            }
            if (CurrentStudent.Age <= 0)
            {
                error = "Возраст должен быть положительным";
                return false;
            }
            if (CurrentStudent.Grade < 2 || CurrentStudent.Grade > 5)
            {
                error = "Оценка должна быть от 2 до 5";
                return false;
            }
            if (!string.IsNullOrEmpty(CurrentStudent.Email) && !CurrentStudent.Email.Contains("@"))
            {
                error = "Некорректный email";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод очистки данных текущего студента.
        /// </summary>
        private void ClearCurrentStudent()
        {
            CurrentStudent = new Student();
        }

        /// <summary>
        /// Метод получения следующего уникального идентификатора для нового студента.
        /// </summary>
        /// <returns></returns>
        private int GetNextId()
        {
            return Students.Any() ? Students.Max(s => s.Id) + 1 : 1;
        }

        #endregion

        /// <summary>
        /// Свойства и методы для реализации INotifyPropertyChanged.
        /// </summary>
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
