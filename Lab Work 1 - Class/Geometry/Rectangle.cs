using System;

namespace GeometricFigureApp
{

    // Класс Геометрическая фигура (прямоугольник)
    // Поддерживает задание сторон, координат на плоскости
    // Вычисление площади и периметра
    public class GeometricFigure
    {
        // Приватные поля для инкапсуляции
        private double _sideA; // Длина стороны A
        private double _sideB; // Длина стороны B
        private double _x;     // Координата X 
        private double _y;     // Координата Y

        // Конструктор с параметрами.
        public GeometricFigure(double sideA, double sideB, double x, double y)
        {
            if (sideA <= 0 || sideB <= 0)
                throw new ArgumentException("Стороны должны быть положительными числами.");

            _sideA = sideA;
            _sideB = sideB;
            _x = x;
            _y = y;
        }

        // Свойства для доступа и изменения данных с валидацией
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

        public double X // Свойство для координаты X
        {
            get => _x; // Геттер
            set => _x = value; // Сеттер
        }

        public double Y // Свойство для координаты Y
        {
            get => _y; // Геттер
            set => _y = value; // Сеттер
        }

    }
}