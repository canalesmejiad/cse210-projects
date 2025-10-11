using System;
using System.Buffers;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n==== ETERNAL QUEST ====");
            Console.WriteLine($"Score: {manager.TotalScore}");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goal");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Choose: ");

            switch (Console.ReadLine())
            {
                case "1": manager.CreateGoal(); break;
                case "2": manager.ListGoals(); break;
                case "3": manager.RecordEvent(); break;
                case "4": manager.SaveGoals(); break;
                case "5": manager.LoadGoals(); break;
                case "0": running = false; break;
                default: Console.WriteLine("Invalid."); break;
            }

        }
    }
}