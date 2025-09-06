using System;
using Microsoft.VisualBasic;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter a grade percentage:");
        string grade = Console.ReadLine();
        int percent = int.Parse(grade);

        string letter = "";

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        Console.WriteLine($"Your grade is {letter}");
        if (percent >= 70)
        {
            Console.WriteLine("You passed!!!");
        }
        else
        {
            Console.WriteLine("Better luck next time!!");
        }
    }
}