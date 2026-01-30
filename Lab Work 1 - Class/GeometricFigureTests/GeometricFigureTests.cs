using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rectangle;  

namespace GeometricFigureTests
{

    [TestClass]
    public class  GeometricFigureTests
    {

        /// <summary>
        /// Тест для проверки правильности вычисления площади прямоугольника.
        /// </summary>
        [TestMethod]
        public void CalculateArea_ValidSides_ReturnsCorrectArea()
        {
            // Arrang - подготовка данных (объекта класса)
            var rectangle = new Rectangle.Rectangle(5.0, 4.0, 0, 0);
            // Act - выполнение действия
            var area = rectangle.CalculateArea();
            // Assert - проверка результата
            Assert.AreEqual(20, area);
        }

        /// <summary>
        /// Тест для проверки правильности вычисления периметра прямоугольника.
        /// </summary>
        [TestMethod]
        public void CalculatePerimeter_ValidSides_ReturnsCorrectPerimeter()
        {
            // Arrang - подготовка данных (объекта класса)
            var rectangle = new Rectangle.Rectangle(5.0, 4.0, 0, 0);
            // Act - выполнение действия
            var perimeter = rectangle.CalculatePerimeter();
            // Assert - проверка результата
            Assert.AreEqual(18, perimeter);
        }

        /// <summary>
        /// Тест для проверки правильности перемещения прямоугольника.
        /// </summary>
        [TestMethod]
        public void Move_ValidDeltas_UpdatesCoordinates()
        {
            // Arrang - подготовка данных (объекта класса)
            var rectangle = new Rectangle.Rectangle(5.0, 4.0, 1, 1);
            // Act - выполнение действия
            rectangle.Move(3.0, 4.0);
            // Assert - проверка результата
            Assert.AreEqual(4.0, rectangle.X);
            Assert.AreEqual(5.0, rectangle.Y);
        }
    }

}