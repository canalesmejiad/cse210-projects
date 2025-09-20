using System;
using System.Text;

class Program
{
    static void Main()
    {
        var reference = new Reference("Proverbs", 3, 5, 6);
        string text =
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
            "In all thy ways acknowledge him, and he shall direct thy paths.";

        var scripture = new Scripture(reference, text);

        var inputBuffer = new StringBuilder();

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.WriteLine("[Space] hide words   |   Type 'exit' then [Enter] to quit");
            if (inputBuffer.Length > 0)
            {
                Console.WriteLine($"Input: {inputBuffer}");
            }

            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Spacebar)
            {
                scripture.HideRandomWords(2);

                if (scripture.AllWordsHidden)
                {
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine("\n(All words are hidden. Program will end.)");
                    break;
                }

                inputBuffer.Clear();
                continue;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                var typed = inputBuffer.ToString().Trim();
                if (typed.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                inputBuffer.Clear();
                continue;
            }

            if (key.Key == ConsoleKey.Backspace)
            {
                if (inputBuffer.Length > 0)
                    inputBuffer.Remove(inputBuffer.Length - 1, 1);
                continue;
            }

            // Agrega caracteres imprimibles al buffer (para poder escribir "exit")
            if (!char.IsControl(key.KeyChar))
            {
                inputBuffer.Append(key.KeyChar);
            }
        }
    }
}