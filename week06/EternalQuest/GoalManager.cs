using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    public List<Goal> Goals { get; private set; } = new();
    public int TotalScore { get; private set; }


    private static int PromptInt(string label, int? min = null, int? max = null)
    {
        while (true)
        {
            Console.Write(label);
            var raw = Console.ReadLine()?.Trim();
            if (int.TryParse(raw, out int val))
            {
                if ((min == null || val >= min) && (max == null || val <= max))
                    return val;
            }
            Console.WriteLine($"Ingrese un entero válido{(min != null ? $" ≥ {min}" : "")}{(max != null ? $" y ≤ {max}" : "")}.");
        }
    }

    private static string PromptNonEmpty(string label)
    {
        while (true)
        {
            Console.Write(label);
            var s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
            Console.WriteLine("No puede estar vacío.");
        }
    }


    public void CreateGoal()
    {
        Console.WriteLine("Select type: 1.Simple 2.Eternal 3.Checklist");
        string type = (Console.ReadLine() ?? "").Trim();

        string name = PromptNonEmpty("Name: ");
        string desc = PromptNonEmpty("Description: ");
        int points = PromptInt("Points: ", min: 0);

        switch (type)
        {
            case "1":
                Goals.Add(new SimpleGoal(name, desc, points));
                break;

            case "2":
                Goals.Add(new EternalGoal(name, desc, points));
                break;

            case "3":
                int target = PromptInt("Target count: ", min: 1);
                int bonus = PromptInt("Bonus: ", min: 0);
                Goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;

            default:
                Console.WriteLine("Invalid type");
                break;
        }
    }


    public void ListGoals()
    {
        Console.WriteLine("\nYour Goals:");
        if (Goals.Count == 0)
        {
            Console.WriteLine("(none)");
            return;
        }
        for (int i = 0; i < Goals.Count; i++)
            Console.WriteLine($"{i + 1}. {Goals[i].GetStatus()}");
    }


    public void RecordEvent()
    {
        if (Goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        ListGoals();
        int choice = PromptInt("Select goal #: ", min: 1, max: Goals.Count) - 1;

        int gained = Goals[choice].RecordEvent();
        TotalScore += gained;
        if (TotalScore < 0) TotalScore = 0;

        Console.WriteLine($"Points earned: {gained}. Total: {TotalScore}");
    }

    public void SaveGoals(string path = "goals.txt")
    {
        try
        {
            using StreamWriter sw = new(path);
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
            Console.WriteLine($"Goals saved to '{path}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save failed: {ex.Message}");
        }
    }

    // ========= Cargar (robusto) =========
    public void LoadGoals(string path = "goals.txt")
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("No save file.");
            return;
        }

        try
        {
            var lines = File.ReadAllLines(path);
            if (lines.Length == 0)
            {
                Console.WriteLine("Save file is empty.");
                return;
            }

            // score
            if (!int.TryParse(lines[0].Trim(), out int score))
            {
                Console.WriteLine("Invalid score in save file.");
                return;
            }
            TotalScore = score;

            Goals.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');
                string type = parts[0].Trim();

                if (type == "Simple")
                {
                    if (parts.Length < 5) { WarnLine(i, "Simple expects 5 fields"); continue; }

                    string name = parts[1].Trim();
                    string desc = parts[2].Trim();

                    if (!int.TryParse(parts[3].Trim(), out int pts)) { WarnLine(i, "Invalid points"); continue; }
                    if (!bool.TryParse(parts[4].Trim(), out bool done)) { WarnLine(i, "Invalid complete flag"); continue; }

                    var g = new SimpleGoal(name, desc, pts);
                    g.SetComplete(done);                 // << usar método en vez de set directo
                    Goals.Add(g);
                }
                else if (type == "Eternal")
                {
                    if (parts.Length < 4) { WarnLine(i, "Eternal expects 4 fields"); continue; }

                    string name = parts[1].Trim();
                    string desc = parts[2].Trim();
                    if (!int.TryParse(parts[3].Trim(), out int pts)) { WarnLine(i, "Invalid points"); continue; }

                    Goals.Add(new EternalGoal(name, desc, pts));
                }
                else if (type == "Checklist")
                {
                    if (parts.Length < 7) { WarnLine(i, "Checklist expects 7 fields"); continue; }

                    string name = parts[1].Trim();
                    string desc = parts[2].Trim();

                    if (!int.TryParse(parts[3].Trim(), out int pts)) { WarnLine(i, "Invalid points"); continue; }
                    if (!int.TryParse(parts[4].Trim(), out int target)) { WarnLine(i, "Invalid target"); continue; }
                    if (!int.TryParse(parts[5].Trim(), out int current)) { WarnLine(i, "Invalid current"); continue; }
                    if (!int.TryParse(parts[6].Trim(), out int bonus)) { WarnLine(i, "Invalid bonus"); continue; }

                    var g = new ChecklistGoal(name, desc, pts, target, bonus);
                    g.SetCurrent(current);               // << usar método en vez de set directo
                    Goals.Add(g);
                }
                else
                {
                    WarnLine(i, $"Unknown type '{type}'");
                }
            }

            Console.WriteLine($"Goals loaded from '{path}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load failed: {ex.Message}");
        }

        static void WarnLine(int lineNo, string msg) =>
            Console.WriteLine($"Warning (line {lineNo + 1}): {msg}. Skipped.");
    }
}