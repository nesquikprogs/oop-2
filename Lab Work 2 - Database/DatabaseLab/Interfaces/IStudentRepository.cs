using DatabaseLab.Models;
using System.Collections.ObjectModel;

namespace DatabaseLab.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с репозиторием студентов.
    /// Теперь коллекция ObservableCollection<Student> является единственным источником данных.
    /// </summary>
    public interface IStudentRepository
    {
        /// <summary>
        /// Загружает коллекцию студентов из источника данных.
        /// </summary>
        ObservableCollection<Student> LoadStudents();

        /// <summary>
        /// Сохраняет коллекцию студентов в источник данных.
        /// </summary>
        /// <param name="students">Коллекция студентов</param>
        void SaveStudents(ObservableCollection<Student> students);
    }
}
