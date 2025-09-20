using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private readonly Reference _reference;
    private readonly List<Word> _words;
    private readonly Random _rand = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(w => new Word(w))
            .ToList();
    }

    public bool AllWordsHidden => _words.All(w => w.IsHidden);

    public void HideRandomWords(int count)
    {
        if (AllWordsHidden || count <= 0) return;

        // Obtiene índices de palabras visibles
        var visible = Enumerable
            .Range(0, _words.Count)
            .Where(i => !_words[i].IsHidden)
            .ToList();

        if (visible.Count == 0) return;

        int toHide = Math.Min(count, visible.Count);

        // Barajar y tomar 'toHide' índices
        for (int i = 0; i < toHide; i++)
        {
            int pickIndex = _rand.Next(visible.Count);
            int wordIndex = visible[pickIndex];
            _words[wordIndex].Hide();
            visible.RemoveAt(pickIndex);
        }
    }

    public string GetDisplayText()
    {
        string refText = _reference.GetDisplayText();
        string body = string.Join(' ', _words.Select(w => w.GetDisplayText()));
        return $"{refText}\n{body}";
    }

    public string GetProgressLine(int barWidth = 30)
    {
        int total = _words.Count;
        int hidden = _words.Count(w => w.IsHidden);
        double percent = total == 0 ? 0 : (hidden * 100.0 / total);

        int filled = total == 0 ? 0 : (int)Math.Round((percent / 100.0) * barWidth);
        if (filled > barWidth) filled = barWidth;

        string bar = "[" + new string('#', filled) + new string('-', barWidth - filled) + "]";
        return $"Hidden {hidden}/{total} ({percent:0.#}%) {bar}";
    }
}