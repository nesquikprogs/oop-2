using DatabaseLab.Models;
using System.Collections.Generic;

namespace DatabaseLab.Interfaces
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
        List<Student> GetAllStudents();
        List<Student> SearchByName(string searchText);
        List<Student> SortByAge();
    }
}