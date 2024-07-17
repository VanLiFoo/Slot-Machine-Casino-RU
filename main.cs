using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Threading;

class Program
{
    static void Kazino(float money)
    {
        sbyte complexity;
        Random rnd = new Random();
        Dictionary<string, float[]> map = new Dictionary<string, float[]>()
        {
            ["[7]"] = [9, 7, 4.5F],
            ["[A]"] = [7, 5, 2.5F],
            ["[B]"] = [5, 3, 0.5F],
            ["[C]"] = [3, 1, 0],
            ["[D]"] = [1, 0, 0]
        };

        Console.WriteLine("Добро пожаловать в казино, изначально вы имеете 100$, но за каждое вращение вы тратите 10$.\n\nТаблица уровней:\nN1 - [7]\nN2 - [A]\nN3 - [B]\nN4 - [C]\nN5 - [D]\n");

        while (true)
        {
            Console.WriteLine("Выбирете сложность от 0 до 2, где 0 - самая легкая, 2 - самая сложная.");
            try
            {
                complexity = sbyte.Parse(Console.ReadLine());
                if (complexity >= 0 && complexity < map["[7]"].Length)
                {
                    break;
                }
            }
            catch {}
            
        }

        while (money > 0)
        {
            Console.Clear();
            money -= 10;
            string result = "";

            for (short i = 0; i < 3; i++)
            {
                short rnd_num = (short)rnd.Next(100);
                string rnd_symbol = rnd_num <= 10 ? "[7]" :
                                    rnd_num <= 30 ? "[A]" :
                                    rnd_num <= 50 ? "[B]" :
                                    rnd_num <= 70 ? "[C]" : "[D]";
                money += map[rnd_symbol][complexity];
                result += rnd_symbol;
            }
            money = money >= 0 ? money : 0;

            Console.WriteLine($"Ваш результат: {result}\nВаш баланс теперь: {money}$\nСыграть еще? (y/n)");

            while (true)
            {
                char choice = Console.ReadKey(true).KeyChar;
                if (choice == 'y')
                {
                    Console.WriteLine("Продолжаем");
                    Thread.Sleep(700);
                    break;
                }
                if (choice == 'n')
                {
                    Console.WriteLine("Хорошо, до свидания!");
                    Thread.Sleep(1500);
                    return;
                }
                else
                {
                    Console.WriteLine("\nВведите корректное значение (y/n)");
                }
            }
        }

        Console.WriteLine("У вас закончились деньги. Игра окончена.");
        Console.ReadLine();
    }

    static void Main(string[] args)
    {
        Kazino(100);
    }
}
