// Extra Feature: Added a level and badge system that rewards the player for reaching specific score milestones.
// The user levels up every 1000 points and earns badges at 500, 1500, 3000, and 5000 points.

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== Eternal Quest ===");
            Console.WriteLine($"Score: {manager.TotalScore} | Level: {manager.Level}");
            var badges = manager.Badges.ToList();
            if (badges.Count > 0)
                Console.WriteLine("Badges: " + string.Join(", ", badges));

            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("0. Exit");
            Console.Write("Choose: ");

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