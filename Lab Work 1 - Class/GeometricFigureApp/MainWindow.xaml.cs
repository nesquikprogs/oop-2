using System;
using System.Windows;
using System.Windows.Controls;

namespace Rectangle   
{
    public partial class MainWindow : Window 
    {
        private Rectangle rectangle; 

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Создать фигуру".
        /// </summary>
        /// <param name="sender">Тот, от кого исходит действие(сам объект кнопки).</param>
        /// <param name="e">Дополнительная информация о событии.</param>
        private void btnCreate_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                // Считываем и парсим значения из текстовых полей
                double a = double.Parse(txtA.Text);
                double b = double.Parse(txtB.Text);
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);

                rectangle = new Rectangle(a, b, x, y); // Создаем новый объект GeometricFigure

                // Обновляем представление прямоугольника на Canvas
                Canvas.SetLeft(rect, rectangle.X); // Устанавливаем позицию по X
                Canvas.SetTop(rect, rectangle.Y); // Устанавливаем позицию по Y
                rect.Width = rectangle.SideA; // Устанавливаем ширину
                rect.Height = rectangle.SideB; // Устанавливаем высоту

                txtInfo.Text = $"Площадь: {rectangle.CalculateArea():F2}   |   Периметр: {rectangle.CalculatePerimeter():F2}"; // Отображаем площадь и периметр
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); // Показываем сообщение об ошибке
            }
        }
    }
}