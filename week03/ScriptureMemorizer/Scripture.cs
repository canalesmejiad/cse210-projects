using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private static readonly Random _rng = new Random();

    private readonly Reference _reference;
    private readonly List<Word> _words;

    public Scripture(Reference reference, string fullText)
    {
        _reference = reference;
        _words = Tokenize(fullText);
    }

    // Tokenize while keeping punctuation attached to its word,
    // so rendering/underscoring preserves commas, semicolons, etc.
    private static List<Word> Tokenize(string text)
    {
        // Split on spaces only; keep punctuation inside tokens.
        return text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(token => new Word(token))
            .ToList();
    }

    public bool AllWordsHidden => _words.All(w => w.IsHidden || AllLettersAreNonAlphabetic(w));

    private static bool AllLettersAreNonAlphabetic(Word w)
    {
        // Helper to treat tokens that contain no letters as effectively "hidden".
        // We inspect the display of the *original*, which Word does not expose.
        // Instead, we'll simulate: if GetDisplayText() for an unhidden word equals its
        // hidden form (i.e., contains no letters to underscore), consider it non-alphabetic.
        // To avoid exposing original, we approximate by checking: if removing all
        // letters from the token would leave the same length.
        // Simplify by recreating logic: we can’t access original, so fall back to:
        // treat punctuation-only tokens as hidden when their underscore form equals original.
        // Practical approach: if after hiding, no underscores added => non-alphabetic.
        string hiddenForm = HidePreview(w);
        string shownForm = ShowPreview(w);
        return hiddenForm == shownForm; // no letters were present
    }

    private static string HidePreview(Word w)
    {
        // emulate hidden output
        // We can't access original, but Word.GetDisplayText() returns either original or hidden form.
        // Temporarily hide using reflection-free trick: if we ask hidden state? We can’t toggle it.
        // Instead, we reproduce the replacement rule on ShowPreview result:
        var shown = ShowPreview(w);
        var chars = shown.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsLetter(chars[i])) chars[i] = '_';
        }
        return new string(chars);
    }

    private static string ShowPreview(Word w) => w.GetDisplayText(); // unhidden returns original

    public void HideRandomWords(int count)
    {
        // For the core requirement, it is acceptable to pick any tokens at random, even if already hidden.
        // To make it a bit nicer, prefer not-hidden words when available.
        var candidates = _words.Where(w => !w.IsHidden).ToList();
        if (candidates.Count == 0) return;

        count = Math.Max(1, count);

        for (int i = 0; i < count; i++)
        {
            var pick = candidates[_rng.Next(candidates.Count)];
            pick.Hide();

            // shrink candidates to reduce repeats
            candidates.Remove(pick);
            if (candidates.Count == 0) break;
        }
    }

    public string GetDisplayText()
    {
        string text = string.Join(' ', _words.Select(w => w.GetDisplayText()));
        return $"{_reference}\n{text}";
    }
}