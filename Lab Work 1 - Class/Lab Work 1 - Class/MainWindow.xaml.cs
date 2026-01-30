using System;
using System.Windows;
using System.Windows.Controls;

namespace Rectangle   
{
    public partial class MainWindow : Window
    {
        private Rectangle figure; 

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Создать фигуру".
        /// </summary>
        /// <param name="sender">Тот, от кого исходит действие(сама кнопка).</param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                // Считываем и парсим значения из текстовых полей
                double a = double.Parse(txtA.Text);
                double b = double.Parse(txtB.Text);
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);

                figure = new Rectangle(a, b, x, y); // Создаем новый объект GeometricFigure

                // Обновляем представление прямоугольника на Canvas
                Canvas.SetLeft(rect, figure.X);
                Canvas.SetTop(rect, figure.Y);
                rect.Width = figure.SideA;
                rect.Height = figure.SideB;

                txtInfo.Text = $"Площадь: {figure.CalculateArea():F2}   |   Периметр: {figure.CalculatePerimeter():F2}"; // Отображаем площадь и периметр
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning); // Показываем сообщение об ошибке
            }
        }
    }
}