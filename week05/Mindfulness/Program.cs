// ============================================
// Mindfulness Program - W05 Project
// Author: David Canales
// Course: CSE 210 / BYU-Idaho
// ============================================
//
// Exceeding Requirements Note:
// 1. Added a new activity called "Gratitude Activity"
//    that allows the user to write three things they are
//    grateful for. This activity was not required but
//    enhances mindfulness and reflection.
// ============================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MindfulnessProgram
{
    // Base class: encapsulates shared behavior (name, description, duration, start/end messages, pauses)
    abstract class MindfulnessActivity
    {
        public string Name { get; }
        public string Description { get; }
        private int _durationSeconds;

        protected MindfulnessActivity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        // Template method pattern: Run() sequences the common start/end flow
        public void Run()
        {
            ShowStartMessage();
            _durationSeconds = PromptDurationSeconds();
            Console.WriteLine("\nGet ready to begin...");
            PauseWithSpinner(3);

            // Run actual activity body
            DoActivity(_durationSeconds);

            ShowEndMessage(_durationSeconds);
        }

        // Derived classes implement their core behavior in the duration provided
        protected abstract void DoActivity(int durationSeconds);

        protected void ShowStartMessage()
        {
            Console.Clear();
            Console.WriteLine($"--- {Name} ---");
            Console.WriteLine(Description);
        }

        protected void ShowEndMessage(int durationSeconds)
        {
            Console.WriteLine();
            Console.WriteLine("Great job! 🎉");
            PauseWithSpinner(2);
            Console.WriteLine($"You have completed the {Name} for {durationSeconds} seconds.");
            PauseWithSpinner(2);
        }

        protected int PromptDurationSeconds()
        {
            while (true)
            {
                Console.Write("\nEnter duration in seconds (e.g., 30): ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int seconds) && seconds > 0)
                    return seconds;

                Console.WriteLine("Please enter a positive whole number.");
            }
        }

        // Simple spinner animation for given seconds
        protected void PauseWithSpinner(int seconds)
        {
            char[] frames = new[] { '|', '/', '-', '\\' };
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < end)
            {
                Console.Write($"\r{frames[i % frames.Length]} ");
                Thread.Sleep(100);
                i++;
            }
            Console.Write("\r   \r"); // clear
        }

        // Countdown helper (each second)
        protected void Countdown(int seconds, string? prefix = null)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write($"\r{(prefix ?? "")}{i} ");
                Thread.Sleep(1000);
            }
            Console.Write("\r   \r");
        }
    }

    class BreathingActivity : MindfulnessActivity
    {
        public BreathingActivity()
            : base(
                name: "Breathing Activity",
                description: "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing."
            )
        { }

        protected override void DoActivity(int durationSeconds)
        {
            Console.WriteLine();
            DateTime end = DateTime.Now.AddSeconds(durationSeconds);
            bool breatheIn = true;

            while (DateTime.Now < end)
            {
                if (breatheIn)
                {
                    Console.Write("Breathe in...");
                    Countdown(4);
                }
                else
                {
                    Console.Write("Breathe out...");
                    Countdown(6);
                }
                breatheIn = !breatheIn;
            }
        }
    }

    class ReflectionActivity : MindfulnessActivity
    {
        private readonly List<string> _prompts = new()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _questions = new()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
            : base(
                name: "Reflection Activity",
                description: "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life."
            )
        { }

        protected override void DoActivity(int durationSeconds)
        {
            Console.WriteLine();
            var rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine("Consider the following questions as you reflect...");
            PauseWithSpinner(3);

            var shuffled = _questions.OrderBy(_ => rand.Next()).ToList();
            int qi = 0;
            DateTime end = DateTime.Now.AddSeconds(durationSeconds);

            while (DateTime.Now < end)
            {
                if (qi >= shuffled.Count)
                {
                    shuffled = _questions.OrderBy(_ => rand.Next()).ToList();
                    qi = 0;
                }

                string q = shuffled[qi++];
                Console.WriteLine($"> {q}");
                PauseWithSpinner(6);
            }
        }
    }

    class ListingActivity : MindfulnessActivity
    {
        private readonly List<string> _prompts = new()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
            : base(
                name: "Listing Activity",
                description: "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
            )
        { }

        protected override void DoActivity(int durationSeconds)
        {
            Console.WriteLine();
            var rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");

            Console.Write("You will begin listing in: ");
            Countdown(5, "");

            Console.WriteLine("Start listing! (Press Enter after each item)");

            var items = new List<string>();
            DateTime end = DateTime.Now.AddSeconds(durationSeconds);

            while (true)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    items.Add(line.Trim());
                }

                if (DateTime.Now >= end)
                    break;
            }

            Console.WriteLine($"\nYou listed {items.Count} item(s). Well done!");
            PauseWithSpinner(2);
        }
    }

    // Extra creative activity (for 100% score)
    class GratitudeActivity : MindfulnessActivity
    {
        public GratitudeActivity()
            : base(
                name: "Gratitude Activity",
                description: "Take a moment to note three things you are grateful for today."
            )
        { }

        protected override void DoActivity(int durationSeconds)
        {
            Console.WriteLine();
            Console.WriteLine("Write three things you are grateful for today (press Enter after each):");
            var list = new List<string>();
            DateTime end = DateTime.Now.AddSeconds(durationSeconds);

            for (int i = 1; i <= 3; i++)
            {
                Console.Write($"#{i}: ");
                string? s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) list.Add(s.Trim());
                if (DateTime.Now >= end) break;
            }

            Console.WriteLine("\nThank you for sharing. Here's what you wrote:");
            foreach (var g in list) Console.WriteLine($"- {g}");
            PauseWithSpinner(2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Exceeding requirements:
            // - Added GratitudeActivity.
            // - Reflection questions shuffled (no repeats until all shown).
            // - Encapsulated all common logic in base class.
            var menu = new Dictionary<string, Func<MindfulnessActivity>>()
            {
                { "1", () => new BreathingActivity() },
                { "2", () => new ReflectionActivity() },
                { "3", () => new ListingActivity() },
                { "4", () => new GratitudeActivity() }
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflection Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) Gratitude Activity (extra)");
                Console.WriteLine("Q) Quit");
                Console.Write("\nChoose an option: ");

                string? choice = Console.ReadLine()?.Trim().ToUpperInvariant();

                if (choice == "Q")
                {
                    Console.WriteLine("Goodbye 👋");
                    Thread.Sleep(600);
                    break;
                }

                if (choice != null && menu.TryGetValue(choice, out var factory))
                {
                    try
                    {
                        var activity = factory();
                        activity.Run();
                        Console.WriteLine("\nPress any key to return to the menu...");
                        Console.ReadKey(true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey(true);
                }
            }
        }
    }
}