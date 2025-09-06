using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int number = randomGenerator.Next(1, 11);

        Console.WriteLine($"What is the magic number? {number}");


        Console.WriteLine("What is your guess? ");
        string guess = Console.ReadLine();
        int guessNumber = int.Parse(guess);

        while (guessNumber != number)
        {
            if (guessNumber > number)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("Higher");
            }
            Console.WriteLine("Try again: ");
            guess = Console.ReadLine();
            guessNumber = int.Parse(guess);
        }
        Console.WriteLine("You guessed it!!");
    }
}