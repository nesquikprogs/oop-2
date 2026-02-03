namespace DatabaseLab.Models
{
    public class Student
    {
        public int ID { get; set; } // Уникальный идентификатор, автоинкремент в БД
        public string Name { get; set; } // Имя студента, строка
        public int Age { get; set; } // Возраст, int число
        public double Grade { get; set; } // Средняя оценка, double число
        public string Email { get; set; } // Email, строка
    }
}