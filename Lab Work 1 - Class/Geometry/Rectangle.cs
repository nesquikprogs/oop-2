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



    }
}