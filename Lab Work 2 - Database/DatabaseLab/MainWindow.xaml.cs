using DatabaseLab.Models;
using System.Windows;
using System.Windows.Controls;

namespace DatabaseLab
{
    public partial class MainWindow : Window
    {
        private readonly DatabaseManager db = new DatabaseManager();

        public MainWindow()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            dgStudents.ItemsSource = db.GetAllStudents();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student
            {
                Name = txtName.Text.Trim(),
                Age = int.TryParse(txtAge.Text, out int age) ? age : 0,
                Grade = double.TryParse(txtGrade.Text.Replace(".", ","), out double grade) ? grade : 0,
                Email = txtEmail.Text.Trim()
            };

            db.AddStudent(student);
            RefreshData();
            ClearInputs();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is Student selected)
            {
                selected.Name = txtName.Text.Trim();
                selected.Age = int.TryParse(txtAge.Text, out int age) ? age : selected.Age;
                selected.Grade = double.TryParse(txtGrade.Text.Replace(".", ","), out double grade) ? grade : selected.Grade;
                selected.Email = txtEmail.Text.Trim();

                db.UpdateStudent(selected);
                RefreshData();
                ClearInputs();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is Student selected)
            {
                db.DeleteStudent(selected.Id);
                RefreshData();
                ClearInputs();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            dgStudents.ItemsSource = db.SearchByName(txtSearch.Text);
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            dgStudents.ItemsSource = db.SortByAge();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
            txtSearch.Text = "";
        }

        private void dgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgStudents.SelectedItem is Student selected)
            {
                txtName.Text = selected.Name;
                txtAge.Text = selected.Age.ToString();
                txtGrade.Text = selected.Grade.ToString("F1");
                txtEmail.Text = selected.Email;
            }
        }

        private void ClearInputs()
        {
            txtName.Text = txtAge.Text = txtGrade.Text = txtEmail.Text = "";
        }
    }
}