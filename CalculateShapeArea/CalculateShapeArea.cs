using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CalculateShapeArea
{
    /// <summary>
    /// Абстрактный класс, обеспечивающий вычисление площади фигур, которые можно описать набором отрезков. Например, стороны треугольника, радиус круга, полуоси эллипса и т.д.
    /// </summary>
    public abstract class Shape
    {
        /// <summary>
        /// Набор отрезков, описывающих фигуру
        /// </summary>
        protected List<double> _measurments;
        /// <summary>
        /// Устанавливает и возвращает нередактируемый список отрезков, описывающих фигуру.
        /// </summary>
        /// <exception cref="ArgumentException">Устанавливаемый набор отрезков не описывает действительную фигуру</exception>
        public ReadOnlyCollection<double> Measurments
        {
            set
            {
                if (IsValid(value.ToList()))
                {
                    _measurments = value.ToList();
                }
                else
                {
                    throw new ArgumentException("Shape measurments are invalid");
                }
            }
            get
            {
                return _measurments.AsReadOnly();
            }
        }
        /// <summary>
        /// Свойство, возвращающее площадь фигуры
        /// </summary>
        public double Area { get { return CalculateArea(); } }
        /// <summary>
        /// Абстрактный метод, обеспечивающий вычисление площади фигуры
        /// </summary>
        /// <returns>Площадь фигуры</returns>
        protected abstract double CalculateArea();
        /// <summary>
        /// Абстрактный метод, проверяющий список переданных отрезков на то, что они описывают действительную фигуру
        /// </summary>
        /// <param name="measurments">Список отрезков для проверки</param>
        /// <returns>True - список отрезков описывает действительную фигуру, False - список отрезков не описывает действительную фигуру</returns>
        protected abstract bool IsValid(List<double> measurments);
    }

    /// <summary>
    /// Класс, реализующий операции создания и вычиления площади для круга
    /// </summary>
    public class Circle : Shape
    {
        /// <summary>
        /// Конструктор нового экземпляра Circle с переданным радиусом
        /// </summary>
        /// <param name="radius">Радиус круга</param>
        public Circle(double radius)
        {
            Measurments = new List<double>() { radius }.AsReadOnly();
        }

        /// <summary>
        /// Выполняет проверку переданного списка отрезков на описание действительного круга: длина списка - 1, double.MaxValue >= значение длины > 0
        /// </summary>
        /// <param name="measurments">Список отрезков, содержащий радиус</param>
        /// <returns>True - список отрезков описывает действительный круг, False - список отрезков не описывает действительный круг</returns>
        protected override bool IsValid(List<double> measurments)
        {
            if (measurments.Count == 1 && measurments[0] > 0 && measurments[0] <= double.MaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Выполняет рассчёт площади круга по его радиусу
        /// </summary>
        /// <returns>Площадь круга</returns>
        protected override double CalculateArea()
        {
            return Math.PI * Measurments[0] * Measurments[0];
        }
    }

    /// <summary>
    /// Класс, реализующий операции создания и вычиления площади для треугольника
    /// </summary>
    public class Triangle : Shape
    {
        /// <summary>
        /// Создаёт новый экземпляр Triangle с переданными сторонами
        /// </summary>
        /// <param name="side1">Сторона треугольника A</param>
        /// <param name="side2">Сторона треугольника B</param>
        /// <param name="side3">Сторона треугольника C</param>
        public Triangle(double side1, double side2, double side3)
        {
            Measurments = new List<double>() { side1, side2, side3 }.AsReadOnly();
        }

        /// <summary>
        /// Выполняет проверку переданного списка отрезков на описание действительного треугольника: длина списка - 3, double.MaxValue >= значение длины каждой из сторон > 0,
        /// Сумма длинн каждых 2 сторон больше длинны третьей стороны.
        /// </summary>
        /// <param name="measurments">Список отрезков, содержащий стороны треугольника</param>
        /// <returns>True - список отрезков описывает действительный треугольник, False - список отрезков не описывает действительный треугольник</returns>
        protected override bool IsValid(List<double> measurments)
        {
            if (measurments.Count == 3 &&
                measurments[0] > 0 &&
                measurments[1] > 0 &&
                measurments[2] > 0 &&
                measurments[0] <= double.MaxValue &&
                measurments[1] <= double.MaxValue &&
                measurments[2] <= double.MaxValue &&
                measurments[0] + measurments[1] > measurments[2] &&
                measurments[0] + measurments[2] > measurments[1] &&
                measurments[1] + measurments[2] > measurments[0]
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Определяет является ли треугольник прямоугольным.
        /// </summary>
        /// <returns>True - треугольник является прямоугольным, False - треугольник не является прямоугольным, null - невозможно определить тип треугольника из-за переполнения double</returns>
        public bool? IsRightTriangle()
        {
            var orderedMeasurments = _measurments.OrderByDescending(m => m).ToList();

            double csqr = Math.Pow(orderedMeasurments[0], 2);
            double bsqr = Math.Pow(orderedMeasurments[1], 2);
            double asqr = Math.Pow(orderedMeasurments[2], 2);

            if (double.IsInfinity(csqr) || double.IsInfinity(bsqr + asqr))
            {
                return null;
            }

            if (csqr == bsqr + asqr)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Выполняет рассчёт площади треугольника по 3 его сторонам
        /// </summary>
        /// <returns>Площадь треугольника. Возвращает double.PositiveInfinity, если в ходе вычисления произошло переполнение double</returns>
        protected override double CalculateArea()
        {
            double p = (Measurments[0] + Measurments[1] + Measurments[2]) / 2;
            return Math.Sqrt(p * (p - Measurments[0]) * (p - Measurments[1]) * (p - Measurments[2]));
        }
    }
}