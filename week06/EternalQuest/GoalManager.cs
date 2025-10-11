using System;
using System.Collections.Generic;
using System.IO;

class GoalManager
{
    public List<Goal> Goals { get; set; } = new();
    public int TotalScore { get; set; }

    public void CreateGoal()
    {
        Console.WriteLine("Select type: 1.Simple 2.Eternal 3.Checklist");
        string type = Console.ReadLine();

        Console.Write("Name: "); string name = Console.ReadLine();
        Console.Write("Description: "); string desc = Console.ReadLine();
        Console.Write("Points: "); int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1": Goals.Add(new SimpleGoal(name, desc, points)); break;
            case "2": Goals.Add(new EternalGoal(name, desc, points)); break;
            case "3":
                Console.Write("Target count: "); int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus: "); int bonus = int.Parse(Console.ReadLine());
                Goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
            default: Console.WriteLine("Invalid type"); break;
        }
    }

    public void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        if (Goals.Count == 0) Console.WriteLine("(none)");
        for (int i = 0; i < Goals.Count; i++)
            Console.WriteLine($"{i + 1}. {Goals[i].GetStatus()}");
    }

    public void RecordEvent()
    {
        ListGoals();
        Console.Write("Select goal #: ");
        int choice = int.Parse(Console.ReadLine()) - 1;
        if (choice < 0 || choice >= Goals.Count) return;

        int gained = Goals[choice].RecordEvent();
        TotalScore += gained;
        Console.WriteLine($"Points earned: {gained}. Total: {TotalScore}");
    }

    public void SaveGoals()
    {
        using StreamWriter sw = new("goals.txt");
        sw.WriteLine(TotalScore);
        foreach (Goal g in Goals)
        {
            if (g is SimpleGoal sg)
                sw.WriteLine($"Simple|{sg.Name}|{sg.Description}|{sg.Points}|{sg.IsComplete}");
            else if (g is EternalGoal eg)
                sw.WriteLine($"Eternal|{eg.Name}|{eg.Description}|{eg.Points}");
            else if (g is ChecklistGoal cg)
                sw.WriteLine($"Checklist|{cg.Name}|{cg.Description}|{cg.Points}|{cg.TargetCount}|{cg.CurrentCount}|{cg.Bonus}");
        }
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt")) { Console.WriteLine("No save file."); return; }
        Goals.Clear();
        string[] lines = File.ReadAllLines("goals.txt");
        TotalScore = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0];
            if (type == "Simple")
            {
                var g = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])) { IsComplete = bool.Parse(parts[4]) };
                Goals.Add(g);
            }
            else if (type == "Eternal")
                Goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
            else if (type == "Checklist")
            {
                var g = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[6]))
                { CurrentCount = int.Parse(parts[5]) };
                Goals.Add(g);
            }
        }
        Console.WriteLine("Goals loaded.");
    }
}
