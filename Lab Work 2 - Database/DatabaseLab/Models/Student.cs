namespace DatabaseLab.Models
{
    /// <summary>
    /// Класс записи о студенте. В Модели MVVM представляет Model.
    /// </summary>
    public class Student
    {

        #region Свойства

        /// <summary>
        /// Свойство уникального идентификатора студента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Свойство имени студента.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Свойство возраста студента.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Свойство оценки студента.
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        ///  СВойство электронной почты студента.
        /// </summary>
        public string Email { get; set; } = "";

        #endregion
    }
}