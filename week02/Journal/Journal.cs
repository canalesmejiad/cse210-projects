using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private readonly List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\n(Your journal is empty.)\n");
            return;
        }

        Console.WriteLine("\n----- Journal -----\n");
        foreach (var e in _entries)
        {
            Console.WriteLine(e.ToString());
        }
        Console.WriteLine("-------------------\n");
    }

    public void SaveToFile(string filename)
    {
        using (var writer = new StreamWriter(filename))
        {
            foreach (var e in _entries)
            {
                writer.WriteLine(e.ToDelimited());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        var loaded = new List<Entry>();
        using (var reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                loaded.Add(Entry.FromDelimited(line));
            }
        }
        _entries.Clear();
        _entries.AddRange(loaded);
    }
}