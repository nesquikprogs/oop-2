using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rectangle;
using System.Xml.Serialization;

namespace GeometricFigureTests
{

    [TestClass]
    public class GeometricFigureTests
    {

        #region Конструктор

        /// <summary>
        /// Тест для проверки корректного создания экземпляра класса с валидными параметрами.
        /// </summary>
        [TestMethod]
        public void Constructor_ValidParameters_CreatesInstance()
        {
            // Arrang & Act - создание объекта класса
            var rectangle = new Rectangle.Rectangle(5.0, 4.0, 0, 0);
            // Assert - проверка результата
            Assert.IsNotNull(rectangle);
            Assert.AreEqual(5.0, rectangle.SideA);
            Assert.AreEqual(4.0, rectangle.SideB);
            Assert.AreEqual(0, rectangle.X);
            Assert.AreEqual(0, rectangle.Y);
        }

        /// <summary>
        /// Тест для проверки выбрасывания исключения при создания экземпляра класса с некорректными параметрами.
        /// </summary>
        [TestMethod]
        public void Constructor_IncorrectValue_ThrowsArgumentException()
        {
            // Arrang & Act & Assert - проверка, что конструктор выбрасывает исключение при некорректной стороне
            Assert.ThrowsException<ArgumentException>(
                () => new Rectangle.Rectangle(4.0, 0, 0, 0),
                "Конструктор должен выбрасывать ArgumentException при некорректном значении какой-либо стороны"
                );
        }

        #endregion

        #region Свойство SideA

        /// <summary>
        /// Тест для проверки установки свойством корректного значения стороны A
        /// </summary>
        [TestMethod]
        public void SideA_CorrectcValue_SetA()
        {
            // Arrang 
            var rectangle = new Rectangle.Rectangle(4.0, 4.0, 0, 0);
            // Act
            rectangle.SideA = 6;
            // Assert
            Assert.AreEqual(6, rectangle.SideA);
        }


        /// <summary>
        /// Тест для проверки выбрасывания исключения при установке некорректного значения стороны A.
        /// </summary>
        [TestMethod]
        public void SideA_IncorrectValue_ThrowsArgumentException()
        {
            // Arrang & Act - создание объекта класса
            var rectangle = new Rectangle.Rectangle(4.0, 4.0, 0, 0);
            // Assert
            Assert.ThrowsException<ArgumentException>(
                () => rectangle.SideA = -5,
                "Свойство должно выбрасывать ArgumentException при некорректном значении стороны A"
                );
        }

        #endregion

        #region Свойство SideB

        /// <summary>
        /// Тест для проверки установки свойством корректного значения стороны B
        /// </summary>
        [TestMethod]
        public void SideB_CorrectcValue_SetB()
        {
            // Arrang 
            var rectangle = new Rectangle.Rectangle(4.0, 4.0, 0, 0);
            // Act
            rectangle.SideB = 6;
            // Assert
            Assert.AreEqual(6, rectangle.SideB);
        }

        /// <summary>
        /// Тест для проверки выбрасывания исключения при установке некорректного значения стороны B.
        /// </summary>
        [TestMethod]
        public void SideB_IncorrectValue_ThrowsArgumentException() 
        {
            // Arrang & Act - создание объекта класса
            var rectangle = new Rectangle.Rectangle(4.0, 4.0, 0, 0);
            // Assert
            Assert.ThrowsException<ArgumentException>(
                () => rectangle.SideB = 0,
                "Свойство должно выбрасывать ArgumentException при некорректном значении стороны B"
                );
        }

        #endregion

        #region Методы

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
            Assert.AreEqual(20, area, 0.0001);
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

        #endregion

    }

}