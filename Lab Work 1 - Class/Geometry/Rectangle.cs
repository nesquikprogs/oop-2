using System;

namespace Rectangle
{

    /// <summary>
    /// Представляет геометрическую фигуру — прямоугольник, расположенный на плоскости.
    /// </summary>
    public class Rectangle
    {
        #region Поля (приватные, для инкапсуляции)

        /// <summary>
        /// Длина стороны A (ширина прямоугольника)
        /// </summary>
        private double _sideA;

        /// <summary>
        /// Длина стороны B (высота прямоугольника)
        /// </summary>
        private double _sideB;

        /// <summary>
        /// Координата X левого верхнего угла прямоугольника
        /// </summary>
        private double _x;

        /// <summary>
        /// Координата Y левого верхнего угла прямоугольника
        /// </summary>
        private double _y;

        #endregion

        #region Конструктор

        /// <summary>
        /// Создает новый экземпляр класса GeometricFigure с заданными сторонами и координатами.
        /// </summary>
        /// <param name="sideA">Длина стороны А (ширина). Должна быть больше 0.</param>
        /// <param name="sideB">Длина стороны В (высота). Должна быть больше 0.</param>
        /// <param name="x">Кооридината X левого верхнего угла.</param>
        /// <param name="y">Кооридината Y левого верхнего угла.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если sideA или sideB меньше или равны нулю.
        /// </exception>
        public Rectangle(double sideA, double sideB, double x, double y)
        {
            if (sideA <= 0 || sideB <= 0)
                throw new ArgumentException("Стороны должны быть положительными числами.");

            _sideA = sideA;
            _sideB = sideB;
            _x = x;
            _y = y;
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Получает или задает длину стороны A (ширина прямоугольника).
        /// </summary>
        /// <value>Должна быть больше 0.</value>
        /// <exception cref="ArgumentException">
        /// При попытке установить значение меньше или равное нулю.
        /// </exception>
        public double SideA // Свойство для стороны A
        {
            get => _sideA; // Геттер
            set // Сеттер с валидацией
            {
                if (value <= 0)
                    throw new ArgumentException("Сторона A должна быть положительной.");
                _sideA = value; // Установка значения, если валидация пройдена
            }
        }

        /// <summary>
        /// Получает или задает длину стороны B (высота прямоугольника).
        /// </summary>
        /// <value>Должна быть больше 0.</value>
        /// <exception cref="ArgumentException">
        /// При попытке установить значение меньше или равное нулю.
        /// </exception>
        public double SideB // Свойство для стороны B
        {
            get => _sideB; // Геттер
            set // Сеттер с валидацией
            {
                if (value <= 0)
                    throw new ArgumentException("Сторона B должна быть положительной.");
                _sideB = value; // Установка значения, если валидация пройдена
            }
        }

        /// <summary>
        /// Получает или задает координату X левого верхнего угла прямоугольника.
        /// </summary>
        public double X // Свойство для координаты X
        {
            get => _x; // Геттер
            set => _x = value; // Сеттер
        }

        /// <summary>
        /// Получает или задает координату Y левого верхнего угла прямоугольника.
        /// </summary>
        public double Y // Свойство для координаты Y
        {
            get => _y; // Геттер
            set => _y = value; // Сеттер
        }

        #endregion

        #region Методы

        /// <summary>
        /// Вычисляет периметр прямоугольника.
        /// </summary>
        /// <returns>Периметр</returns>
        public double CalculatePerimeter()
        {
            return 2 * (_sideA + _sideB); 
        }

        /// <summary>
        /// Вычисляет площадь прямоугольника.
        /// </summary>
        /// <returns>Площадь</returns>
        public double CalculateArea()
        {
            return _sideA * _sideB;
        }

        /// <summary>
        /// Перемещает прямоугольник на заданные смещения по осям X и Y.
        /// </summary>
        /// <param name="deltaX">Смещение по горизонтали.</param>
        /// <param name="deltaY">Смещение по вертикали</param>
        public void Move(double deltaX, double deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        #endregion

    }
}