using System;
using System.Text;

public class Word
{
    private readonly string _original;
    private bool _hidden;

    public Word(string token)
    {
        _original = token;
        _hidden = false;
    }

    public bool IsHidden => _hidden;

    public void Hide() => _hidden = true;

    // Display original text or a version with letters replaced by underscores,
    // preserving punctuation and spacing length.
    public string GetDisplayText()
    {
        if (!_hidden) return _original;

        var sb = new StringBuilder(_original.Length);
        foreach (char c in _original)
        {
            if (char.IsLetter(c))
                sb.Append('_');
            else
                sb.Append(c);
        }
        return sb.ToString();
    }
}