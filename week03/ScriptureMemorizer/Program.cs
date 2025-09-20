using System;

class Program
{
    static void Main()
    {
        // Example scripture: Proverbs 3:5–6 (range)
        var reference = new Reference("Proverbs", 3, 5, 6);
        string text =
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
            "In all thy ways acknowledge him, and he shall direct thy paths.";

        var scripture = new Scripture(reference, text);

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press Enter to hide words, or type 'quit' to end: ");

            string? input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) &&
                input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            // Hide a few random words each cycle (e.g., 3)
            scripture.HideRandomWords(3);

            if (scripture.AllWordsHidden)
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\n(All words are hidden. Program will end.)");
                break;
            }
        }
    }
}