using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private readonly List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What did I learn today that I want to remember?",
        "How did I help someone today?",
        "Where did I notice beauty today?"
    };

    private readonly Random _random = new Random();

    public string GetRandomPrompt()
    {
        int i = _random.Next(_prompts.Count);
        return _prompts[i];
    }
}