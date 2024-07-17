using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    class Symbol
    {
        public string Character { get; set; }
        public float[] Payouts { get; set; }
    }

    static void Kazino(float money = 100)
    {
        sbyte complexity = GetComplexity();
        Random rnd = new Random();

        List<Symbol> symbols = new List<Symbol>
        {
            new Symbol { Character = "[7]", Payouts = new float[] { 9, 7, 4.5F } },
            new Symbol { Character = "[A]", Payouts = new float[] { 7, 5, 2.5F } },
            new Symbol { Character = "[B]", Payouts = new float[] { 5, 3, 0.5F } },
            new Symbol { Character = "[C]", Payouts = new float[] { 3, 1, 0 } },
            new Symbol { Character = "[D]", Payouts = new float[] { 1, 0, 0 } }
        };

        Console.WriteLine("Добро пожаловать в казино, изначально вы имеете 100$, но за каждое вращение вы тратите 10$.\n\nТаблица уровней:\nN1 - [7]\nN2 - [A]\nN3 - [B]\nN4 - [C]\nN5 - [D]\n");
        Console.ReadLine();

        while (money > 0)
        {
            string result = "";
            float difference = 0;
            Console.Clear();

            for (byte i = 0; i < 3; i++)
            {
                Symbol symbol = GetRandomSymbol(rnd);
                difference += symbol.Payouts[complexity];
                result += symbol.Character;
            }

            difference -= 10;
            money += difference;
            money = money >= 0 ? money : 0;
            string sign = difference >= 0 ? "+" : "";

            Console.WriteLine($"Ваш результат: {result}\nВаш баланс теперь: {money}$ ({sign}{difference}$)\nСыграть еще? (y/n)");

            if (!PlayAgain())
            {
                Console.WriteLine("Хорошо, до свидания!");
                Thread.Sleep(1500);
                return;
            }
        }

        Console.WriteLine("У вас закончились деньги. Игра окончена.");
        Console.ReadLine();
    }

    static sbyte GetComplexity()
    {
        while (true)
        {
            Console.WriteLine("Выберите сложность от 0 до 2, где 0 - самые высокие шансы, 2 - самые низкие шансы.");
            if (sbyte.TryParse(Console.ReadLine(), out sbyte complexity) && complexity >= 0 && complexity <= 2)
            {
                return complexity;
            }
            Console.WriteLine("Введите корректное значение сложности.");
        }
    }

    static Symbol GetRandomSymbol(Random rnd)
    {
        byte rnd_num = (byte)rnd.Next(100);
        return rnd_num <= 10 ? new Symbol { Character = "[7]", Payouts = new float[] { 9, 7, 4.5F } } :
               rnd_num <= 25 ? new Symbol { Character = "[A]", Payouts = new float[] { 7, 5, 2.5F } } :
               rnd_num <= 45 ? new Symbol { Character = "[B]", Payouts = new float[] { 5, 3, 0.5F } } :
               rnd_num <= 70 ? new Symbol { Character = "[C]", Payouts = new float[] { 3, 1, 0 } } :
                               new Symbol { Character = "[D]", Payouts = new float[] { 1, 0, 0 } };
    }

    static bool PlayAgain()
    {
        while (true)
        {
            char choice = Console.ReadKey(true).KeyChar;
            if (choice == 'y')
            {
                Console.WriteLine("Продолжаем");
                Thread.Sleep(700);
                return true;
            }
            if (choice == 'n')
            {
                return false;
            }
            Console.WriteLine("\nВведите корректное значение (y/n)");
        }
    }

    static void Main(string[] args)
    {
        Kazino();
    }
}
