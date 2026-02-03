using DatabaseLab.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System;
using System.Collections.Generic;

namespace DatabaseLab.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseLab.Interfaces.IStudentRepository _db;

        public ObservableCollection<Student> Students { get; } = new ObservableCollection<Student>();

        private Student _currentStudent = new Student();
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

        public string Name
        {
            get => CurrentStudent.Name;
            set { CurrentStudent.Name = value?.Trim() ?? ""; OnPropertyChanged(nameof(Name)); }
        }

        public string AgeText
        {
            get => CurrentStudent.Age.ToString();
            set
            {
                if (int.TryParse(value, out int age))
                    CurrentStudent.Age = age;
                OnPropertyChanged(nameof(AgeText));
            }
        }

        public string GradeText
        {
            get => CurrentStudent.Grade.ToString("F1");
            set
            {
                string cleaned = value?.Replace(".", ",") ?? "";
                if (double.TryParse(cleaned, out double grade))
                    CurrentStudent.Grade = grade;
                OnPropertyChanged(nameof(GradeText));
            }
        }

        public string Email
        {
            get => CurrentStudent.Email;
            set { CurrentStudent.Email = value?.Trim() ?? ""; OnPropertyChanged(nameof(Email)); }
        }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        public MainViewModel()
        {
            _db = new DatabaseLab.Models.DatabaseManager();
            LoadStudents();
        }

        private void LoadStudents()
        {
            Students.Clear();
            foreach (var s in _db.GetAllStudents())
                Students.Add(s);
        }

        public void AddStudent()
        {
            if (!ValidateCurrentStudent(out string error))
            {
                MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _db.AddStudent(CurrentStudent);
            LoadStudents();
            ClearCurrentStudent();
        }

        public void UpdateStudent()
        {
            if (CurrentStudent.Id == 0) return;

            if (!ValidateCurrentStudent(out string error))
            {
                MessageBox.Show(error, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _db.UpdateStudent(CurrentStudent);
            LoadStudents();
            ClearCurrentStudent();
        }

        public void DeleteStudent(Student student)
        {
            if (student == null) return;

            var result = MessageBox.Show($"Удалить студента {student.Name}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _db.DeleteStudent(student.Id);
                LoadStudents();
                if (CurrentStudent.Id == student.Id)
                    ClearCurrentStudent();
            }
        }

        public void Search()
        {
            Students.Clear();
            var results = _db.SearchByName(SearchText);
            foreach (var s in results)
                Students.Add(s);
        }

        public void SortByAge()
        {
            Students.Clear();
            var sorted = _db.SortByAge();
            foreach (var s in sorted)
                Students.Add(s);
        }

        public void Refresh()
        {
            SearchText = "";
            LoadStudents();
        }

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

            if (CurrentStudent.Grade < 0 || CurrentStudent.Grade > 10)
            {
                error = "Оценка должна быть от 0 до 10";
                return false;
            }

            // Можно добавить проверку email
            if (!string.IsNullOrEmpty(CurrentStudent.Email) && !CurrentStudent.Email.Contains("@"))
            {
                error = "Некорректный email";
                return false;
            }

            return true;
        }

        private void ClearCurrentStudent()
        {
            CurrentStudent = new Student();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}