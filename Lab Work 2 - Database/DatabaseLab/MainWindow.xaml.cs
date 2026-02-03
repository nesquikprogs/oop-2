using DatabaseLab.Models;
using DatabaseLab.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseLab
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            _vm = new MainViewModel();
            DataContext = _vm;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _vm.AddStudent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            _vm.UpdateStudent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _vm.DeleteStudent(dgStudents.SelectedItem as Student);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _vm.Search();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            _vm.SortByAge();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _vm.Refresh();
        }

        private void dgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStudents.SelectedItem is Student selected)
            {
                _vm.CurrentStudent = selected;
            }
        }
    }
}