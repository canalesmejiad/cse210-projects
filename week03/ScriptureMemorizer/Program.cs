using System;

class Program
{
    static void Main()
    {
        // Ejemplo: Proverbios 3:5–6
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
            Console.WriteLine(scripture.GetProgressLine(30));
            Console.WriteLine();

            if (scripture.AllWordsHidden)
            {
                Console.WriteLine("(All words are hidden. Program will end.)");
                break;
            }

            Console.Write("Press SPACE (then Enter) to hide more words, or type 'exit' to quit: ");
            var input = Console.ReadLine() ?? string.Empty;

            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            // Si el usuario presiona espacios (o deja vacío), ocultamos más palabras
            if (input.Length == 0 || string.IsNullOrWhiteSpace(input))
            {
                scripture.HideRandomWords(3); // oculta 3 palabras por ciclo
            }
            // Cualquier otro texto se ignora y se vuelve a mostrar el pasaje
        }
    }
}