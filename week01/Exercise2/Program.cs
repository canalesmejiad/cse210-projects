using System;
using Microsoft.VisualBasic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a grade porcentage:");
        string input = Console.ReadLine();
        int grade = int.Parse(input);

        if (grade >= 90)
        {
            Console.WriteLine("Your grade is A");
        }
        else if (grade >= 80)
        {
            Console.WriteLine("Your grade is B");
        }
        else if (grade >= 70)
        {
            Console.WriteLine("Your grade is C");
        }
        else if (grade >= 60)
        {
            Console.WriteLine("Your grade is D");
        }
        else
        {
            Console.WriteLine("Your grade is F");
        }
    }
}