using System;

public class Program
{
    static void Main(string[] args)
    {
        var journal = new Journal();
        var promptGen = new PromptGenerator();

        bool running = true;
        while (running)
        {
            ShowMenu();
            Console.Write("Choose an option (1-5): ");
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, promptGen);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    Console.Write("Enter filename to save:");
                    string saveName = Console.ReadLine()?.Trim();
                    journal.SaveToFile(saveName);
                    Console.WriteLine($"Save to \"{saveName}\".\n");
                    break;
                case "4":
                    Console.Write("Enter file to load: ");
                    string loadName = Console.ReadLine()?.Trim();
                    journal.LoadFromFile(loadName);
                    Console.WriteLine($"Loaded journal from \"{loadName}\".\n");
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Goodbye");
                    break;
                default:
                    Console.WriteLine("Please enter a valid option (1-5).\n");
                    break;

            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("=== Journal Menu ===");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal to a file");
        Console.WriteLine("5. Quit");
    }

    private static void WriteNewEntry(Journal journal, PromptGenerator promptGen)
    {
        string prompt = promptGen.GetRandomPrompt();
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        string dateText = DateTime.Now.ToString("yyyy-MM-dd");
        var entry = new Entry(dateText, prompt, response);
        journal.AddEntry(entry);

        Console.WriteLine("Entry added!\n");
    }
}
