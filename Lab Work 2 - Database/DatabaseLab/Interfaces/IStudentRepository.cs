using DatabaseLab.Models;
using System.Collections.Generic;

namespace DatabaseLab.Interfaces
{
    /// <summary>
    /// Интерфейс для управления репозиторием студентов. В модели MVVM представляет Model.
    /// </summary>
    public interface IStudentRepository
    {

        #region Объявления методов

        /// <summary>
        /// Объявление метода для добавления нового студента.
        /// </summary>
        /// <param name="student">Объект студента.</param>
        void AddStudent(Student student);

        /// <summary>
        /// Объявление метода для обновления информации о существующем студенте.
        /// </summary>
        /// <param name="student"></param>
        void UpdateStudent(Student student);

        /// <summary>
        /// Объявление метода для удаления студента по его идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор студента.</param>
        void DeleteStudent(int id);

        /// <summary>
        /// Объявление метода для получения всех студентов.
        /// </summary>
        /// <returns>Список list всех студентов.</returns>
        List<Student> GetAllStudents();

        /// <summary>
        /// Объявление метода для поиска студентов по имени.
        /// </summary>
        /// <param name="searchText">Строка для поиска (имя).</param>
        /// <returns>Список list найденных студентов.</returns>
        List<Student> SearchByName(string searchText);

        /// <summary>
        /// Объявление метода для сортировки студентов по возрасту.
        /// </summary>
        /// <returns>Возвращает новый list отсортированный по критерию возратса.</returns>
        List<Student> SortByAge();

        #endregion

    }
}