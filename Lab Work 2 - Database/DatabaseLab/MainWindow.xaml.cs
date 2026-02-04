using DatabaseLab.Models;
using DatabaseLab.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseLab
{
    /// <summary>
    /// Класс главного окна приложения. В MVVM представляет View.
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Поля

        /// <summary>
        /// Поле для хранения экземпляра ViewModel главного окна.
        /// </summary>
        private readonly MainViewModel _vm; // Связь с ViewModel 

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор главного окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel(); // Инициализация ViewModel
            DataContext = _vm; // Установка контекста для Binding
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Обработчик события клика по кнопке "Add" для добавления нового студента.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _vm.AddStudent();
        }

        /// <summary>
        /// Обработчик события клика по кнопке "Update" для обновления информации о студенте.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _vm.UpdateStudent();
        }

        /// <summary>
        /// Обработчик события клика по кнопке "Delete" для удаления студента.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _vm.DeleteStudent(dgStudents.SelectedItem as Student); // Удаление выбранного студента с приведением типа
        }

        /// <summary>
        /// Обработчик события клика по кнопке "Search" для поиска студентов по имени.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _vm.Search();
        }

        /// <summary>
        /// Обработчик события клика по кнопке "Sort" для сортировки студентов по возрасту.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            _vm.SortByAge();
        }

        /// <summary>
        /// Обработчик события клика по кнопке "Refresh" для обновления списка студентов.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.Refresh();
        }

        /// <summary>
        /// Обработчик события изменения выбора в DataGrid студентов.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void dgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStudents.SelectedItem is Student selected) // Создание выбранного студента с приведением типа
            {
                _vm.CurrentStudent = selected; // Установка текущего студента в ViewModel
            }
        }

        #endregion

    }
}