using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Змейка
{
    class Program
    {
        public static Walls walls; 
        public static Snake snake;
        public static Food food;
        public static Timer time; 

        static void Main()
        {
            Console.CursorVisible = false;//не видим нижнюю строку ввода

            int x = 0;//кол-во элементов стены по горизонтали
            int y = 0;//кол-во элемнтов станы по вертикали
            int s = 0;//скорость змейки
            Console.WriteLine("Выберите уровень сложности. Введите номер и нажмите Enter." +  //Выбор сложности
                "\n1 - Легко" +
                "\n2 - Средне" +
                "\n3 - Сложно");
            int Difficult = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (Difficult)
            {
                case 1:
                    x = 10;
                    y = 10;
                    s = 500;
                    break;

                case 2:
                    x = 15;
                    y = 15;
                    s = 200;
                    break;

                case 3:
                    x = 20;
                    y = 20;
                    s = 100;
                    break;
            }

            walls = new Walls(x, y, '#');     //Стены
            snake = new Snake(x / 2, y / 2, 3); //Змейка

            food = new Food(x, y, 'O');  //Еда
            food.CreateFood();

            time = new Timer(Moving.Move, null, 0, s);  //Скорость движения

            while (true)  //Направление движения
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.Rotation(key.Key);
                }
            }
        }
    }
    struct Point //структура для расположения элементов на консоли
    {
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }

        public static implicit operator Point((int, int, char) value) =>
              new Point { x = value.Item1, y = value.Item2, ch = value.Item3 };

        public static bool operator ==(Point a, Point b) =>
                (a.x == b.x && a.y == b.y) ? true : false;
        public static bool operator !=(Point a, Point b) =>
                (a.x != b.x || a.y != b.y) ? true : false;

        public void Draw()
        {
            DrawPoint(ch);
        }
        public void Clear()
        {
            DrawPoint(' ');
        }

        private void DrawPoint(char _ch)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(_ch);
        }
    }
}