using DatabaseLab.Models;
using DatabaseLab.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

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
        private readonly MainViewModel _vm; // отношение композиция

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор главного окна.
        /// </summary>
        public MainWindow() // Создает MainViewModels
        {
            InitializeComponent();
            _vm = new MainViewModel(); // Инициализация ViewModel
            DataContext = _vm; // Установка контекста для Binding
                               // DataContex определяет контекст, в котором будет поле Student к которому будет привязано содержимое таблица
                               //< DataGrid x: Name = "dgStudents""
                               //ItemsSource = "{Binding Students}"
        }

        #endregion

        #region Обработчики событий

        /// <summary>
        /// Обработчик события клика по кнопке "About" для отображения информации о разработчике.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Разработчик: Александров Данил\nГруппа: ВМК-22\nЛабораторная работа: База студентов на C# и WPF",
                "Сведения о разработчике",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        /// <summary>
        /// Обработчик события клика по кнопке "Help" для отображения краткой справки.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Программа позволяет:\n" +
                "• Добавлять, изменять и удалять студентов\n" +
                "• Искать студентов по имени\n" +
                "• Сортировать студентов по возрасту\n" +
                "Все данные сохраняются в файле students.json.",
                "Краткая справка",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        /// <summary>
        /// Обработчик события выбора файла для бд.
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON files (*.json)|*.json";
            ofd.Title = "Выберите файл для базы студентов";

            if (ofd.ShowDialog() == true)
            {
                _vm.SetFilePath(ofd.FileName); // Выбираем файл бд
            }
        }

        /// <summary>
        /// Обработчик события клика кнопки "CreateNewFile"
        /// </summary>
        /// <param name="sender">То, что вызывает событие (сама кнопка).</param>
        /// <param name="e">Дополнительные аргументы события.</param>
        private void CreateNewFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            sfd.Title = "Создать новый файл базы студентов";

            if (sfd.ShowDialog() == true)
            {
                string path = sfd.FileName;
                System.IO.File.WriteAllText(path, "[]"); 
                _vm.SetFilePath(path);                   
            }
        }

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