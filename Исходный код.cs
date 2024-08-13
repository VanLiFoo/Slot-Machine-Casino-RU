using System;
using System.Threading;
using System.IO;

class Program
{
    class Symbol
    {
        public string Character { get; set; }
        public byte Payouts { get; set; }
    }

    static void Kazino(float money = 100)
    {
        Console.Title = "Добро пожаловать в наше Казино";

        Creating_a_shortcut();

        byte complexity = GetComplexity();
        var rnd = new Random();

        Console.WriteLine("Добро пожаловать в казино, изначально вы имеете 100$, но за каждое вращение вы тратите 10$.\n\nТаблица уровней:\nN1 - [7]\nN2 - [A]\nN3 - [B]\nN4 - [C]\nN5 - [D]\n");
        Console.ReadLine();

        while (money > 0)
        {
            string result = "";
            float difference = 0;
        
            Console.Clear();

            for (byte i = 0; i < 3; i++)
            {
                Symbol symbol = GetRandomSymbol(complexity, rnd);
                difference += symbol.Payouts;
                result += symbol.Character;
            }

            difference -= 10;
            money += difference;
            money = money >= 0 ? money : 0;
            string sign = difference >= 0 ? "+" : "";

            Console.Title = $"Казино. Баланс - {money}$";

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

    static byte GetComplexity()
    {
        while (true)
        {
            Console.WriteLine("Выберите сложность от 0 до 2, где 0 - самые высокие шансы, 2 - самые низкие шансы.");
            if (byte.TryParse(Console.ReadLine(), out byte complexity) && complexity >= 0 && complexity <= 2)
            {
                return complexity;
            }
            Console.WriteLine("Введите корректное значение сложности.");
        }
    }

    static Symbol GetRandomSymbol(in byte complexity, Random rnd)
    {
        byte[,] chance = new byte[,] { { 10, 5, 0}, { 25, 20, 15 }, { 40, 35, 30 }, { 55, 50, 45 } };
        byte rnd_num = (byte)rnd.Next(100);
        return rnd_num <= chance[0, complexity] ? new Symbol { Character = "[7]" , Payouts = 9 } :
               rnd_num <= chance[1, complexity] ? new Symbol { Character = "[A]" , Payouts = 7 } :
               rnd_num <= chance[2, complexity] ? new Symbol { Character = "[B]" , Payouts = 5 } :
               rnd_num <= chance[3, complexity] ? new Symbol { Character = "[C]" , Payouts = 3 } :
               new Symbol { Character = "[D]" , Payouts = 1 };
    }

    static bool PlayAgain()
    {
        while (true)
        {
            char choice = Console.ReadKey(true).KeyChar;
            if (choice == 'y')
            {
                Console.WriteLine("Продолжаем");
                //Thread.Sleep(700);
                return true;
            }
            if (choice == 'n')
            {
                return false;
            }
            Console.WriteLine("\nВведите корректное значение (y/n)");
        }
    }

    static void Creating_a_shortcut()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string shortcutPath = Path.Combine(desktopPath, "Kazino.url");

        if (File.Exists(shortcutPath)) { return; }

        string targetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Kazino.exe");
        string shortcutContent = $"[InternetShortcut]\nURL=file:///{targetPath}\nIconFile={targetPath}\nIconIndex=0";

        File.WriteAllText(shortcutPath, shortcutContent);
    }

    static void Main(string[] args)
    {
        Kazino();
    }
}