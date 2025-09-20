using System;

public class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int VerseStart { get; }
    public int? VerseEnd { get; }

    // Single-verse constructor  (e.g., "John 3:16")
    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verse;
        VerseEnd = null;
    }

    // Range constructor (e.g., "Proverbs 3:5-6")
    public Reference(string book, int chapter, int verseStart, int verseEnd)
    {
        Book = book;
        Chapter = chapter;
        VerseStart = verseStart;
        VerseEnd = verseEnd;
    }

    public override string ToString()
    {
        if (VerseEnd.HasValue && VerseEnd.Value != VerseStart)
            return $"{Book} {Chapter}:{VerseStart}-{VerseEnd.Value}";
        return $"{Book} {Chapter}:{VerseStart}";
    }
}