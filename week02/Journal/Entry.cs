using System;

public class Entry
{
    public string DateText { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    private const string Delim = "~|~";

    public Entry(string dateText, string prompt, string response)
    {
        DateText = dateText;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"[{DateText}]\nQ: {Prompt}\nA: {Response}\n";
    }

    public string ToDelimited()
    {
        return $"{DateText}{Delim}{Prompt}{Delim}{Response}";
    }

    public static Entry FromDelimited(string line)
    {
        var parts = line.Split(new[] { Delim }, StringSplitOptions.None);
        return new Entry(parts[0], parts[1], parts[2]);
    }
}