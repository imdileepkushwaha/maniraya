using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

/// <summary>
/// Lightweight PDF writer (Helvetica / Helvetica-Bold) with boxes, colors, and tables.
/// No external PDF library — suitable for WhatsApp invoice attachments.
/// </summary>
public class SimplePdfBuilder
{
    public const float PageWidth = 595f;
    public const float PageHeight = 842f;

    readonly StringBuilder _content = new StringBuilder(8192);
    readonly float _marginLeft;
    readonly float _marginRight;

    public SimplePdfBuilder(float marginLeft = 28f, float marginRight = 28f)
    {
        _marginLeft = marginLeft;
        _marginRight = marginRight;
    }

    public float ContentLeft
    {
        get { return _marginLeft; }
    }

    public float ContentRight
    {
        get { return PageWidth - _marginRight; }
    }

    public float ContentWidth
    {
        get { return ContentRight - ContentLeft; }
    }

    public void FillRect(float x, float y, float w, float h, float r, float g, float b)
    {
        _content.Append(F(r)).Append(' ').Append(F(g)).Append(' ').Append(F(b)).Append(" rg\n");
        _content.Append(F(x)).Append(' ').Append(F(y)).Append(' ').Append(F(w)).Append(' ').Append(F(h)).Append(" re f\n");
    }

    public void StrokeRect(float x, float y, float w, float h, float r, float g, float b, float lineWidth = 0.6f)
    {
        _content.Append(F(lineWidth)).Append(" w\n");
        _content.Append(F(r)).Append(' ').Append(F(g)).Append(' ').Append(F(b)).Append(" RG\n");
        _content.Append(F(x)).Append(' ').Append(F(y)).Append(' ').Append(F(w)).Append(' ').Append(F(h)).Append(" re S\n");
    }

    public void FillStrokeRect(float x, float y, float w, float h, float fr, float fg, float fb, float sr, float sg, float sb, float lineWidth = 0.6f)
    {
        _content.Append(F(lineWidth)).Append(" w\n");
        _content.Append(F(fr)).Append(' ').Append(F(fg)).Append(' ').Append(F(fb)).Append(" rg\n");
        _content.Append(F(sr)).Append(' ').Append(F(sg)).Append(' ').Append(F(sb)).Append(" RG\n");
        _content.Append(F(x)).Append(' ').Append(F(y)).Append(' ').Append(F(w)).Append(' ').Append(F(h)).Append(" re B\n");
    }

    public void Line(float x1, float y1, float x2, float y2, float r, float g, float b, float lineWidth = 0.5f)
    {
        _content.Append(F(lineWidth)).Append(" w\n");
        _content.Append(F(r)).Append(' ').Append(F(g)).Append(' ').Append(F(b)).Append(" RG\n");
        _content.Append(F(x1)).Append(' ').Append(F(y1)).Append(" m ").Append(F(x2)).Append(' ').Append(F(y2)).Append(" l S\n");
    }

    public void Text(string text, float x, float y, float size, bool bold = false, float r = 0.12f, float g = 0.16f, float b = 0.22f)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        _content.Append("BT\n");
        _content.Append(bold ? "/F2 " : "/F1 ").Append(F(size)).Append(" Tf\n");
        _content.Append(F(r)).Append(' ').Append(F(g)).Append(' ').Append(F(b)).Append(" rg\n");
        _content.Append("1 0 0 1 ").Append(F(x)).Append(' ').Append(F(y)).Append(" Tm\n");
        _content.Append("(").Append(EscapePdfText(text)).Append(") Tj\n");
        _content.Append("ET\n");
    }

    public void TextRight(string text, float rightX, float y, float size, bool bold = false, float r = 0.12f, float g = 0.16f, float b = 0.22f)
    {
        float width = EstimateWidth(text, size, bold);
        Text(text, rightX - width, y, size, bold, r, g, b);
    }

    public void TextCenter(string text, float centerX, float y, float size, bool bold = false, float r = 0.12f, float g = 0.16f, float b = 0.22f)
    {
        float width = EstimateWidth(text, size, bold);
        Text(text, centerX - (width / 2f), y, size, bold, r, g, b);
    }

    /// <summary>
    /// Draws wrapped text; returns Y ready for the next line below the last drawn baseline.
    /// </summary>
    public float TextWrapped(string text, float x, float y, float maxWidth, float size, float lineHeight, bool bold = false, float r = 0.12f, float g = 0.16f, float b = 0.22f)
    {
        foreach (string line in Wrap(text, maxWidth, size, bold))
        {
            Text(line, x, y, size, bold, r, g, b);
            y -= lineHeight;
        }

        return y;
    }

    public IList<string> Wrap(string text, float maxWidth, float size, bool bold = false)
    {
        var lines = new List<string>();
        if (string.IsNullOrWhiteSpace(text))
        {
            lines.Add(string.Empty);
            return lines;
        }

        string[] words = text.Replace("\r", " ").Replace("\n", " ").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var current = new StringBuilder();
        foreach (string word in words)
        {
            string candidate = current.Length == 0 ? word : current + " " + word;
            if (EstimateWidth(candidate, size, bold) <= maxWidth || current.Length == 0)
            {
                current.Length = 0;
                current.Append(candidate);
            }
            else
            {
                lines.Add(current.ToString());
                current.Length = 0;
                current.Append(word);
            }
        }

        if (current.Length > 0)
        {
            lines.Add(current.ToString());
        }

        if (lines.Count == 0)
        {
            lines.Add(string.Empty);
        }

        return lines;
    }

    public static float EstimateWidth(string text, float size, bool bold = false)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0f;
        }

        // Approximate Helvetica average glyph width.
        float factor = bold ? 0.55f : 0.50f;
        return text.Length * size * factor;
    }

    public byte[] Build()
    {
        string stream = _content.ToString();
        byte[] streamBytes = Encoding.ASCII.GetBytes(stream);

        var objects = new List<byte[]>();
        objects.Add(Encoding.ASCII.GetBytes("1 0 obj<< /Type /Catalog /Pages 2 0 R >>endobj\n"));
        objects.Add(Encoding.ASCII.GetBytes("2 0 obj<< /Type /Pages /Kids [3 0 R] /Count 1 >>endobj\n"));
        objects.Add(Encoding.ASCII.GetBytes(
            "3 0 obj<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Contents 4 0 R /Resources<< /Font<< /F1 5 0 R /F2 6 0 R >> >> >>endobj\n"));
        objects.Add(Encoding.ASCII.GetBytes(
            "4 0 obj<< /Length " + streamBytes.Length + " >>stream\n" + stream + "\nendstream\nendobj\n"));
        objects.Add(Encoding.ASCII.GetBytes(
            "5 0 obj<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>endobj\n"));
        objects.Add(Encoding.ASCII.GetBytes(
            "6 0 obj<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold >>endobj\n"));

        var pdf = new List<byte>();
        Action<string> appendAscii = s =>
        {
            byte[] b = Encoding.ASCII.GetBytes(s);
            pdf.AddRange(b);
        };

        appendAscii("%PDF-1.4\n");
        var offsets = new List<int>();
        offsets.Add(0);

        for (int i = 0; i < objects.Count; i++)
        {
            offsets.Add(pdf.Count);
            pdf.AddRange(objects[i]);
        }

        int xrefPos = pdf.Count;
        appendAscii("xref\n0 " + (objects.Count + 1) + "\n");
        appendAscii("0000000000 65535 f \n");
        for (int i = 1; i < offsets.Count; i++)
        {
            appendAscii(offsets[i].ToString("0000000000", CultureInfo.InvariantCulture) + " 00000 n \n");
        }

        appendAscii("trailer<< /Size " + (objects.Count + 1) + " /Root 1 0 R >>\n");
        appendAscii("startxref\n" + xrefPos + "\n%%EOF\n");

        return pdf.ToArray();
    }

    static string F(float value)
    {
        return value.ToString("0.###", CultureInfo.InvariantCulture);
    }

    static string EscapePdfText(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        var sb = new StringBuilder(value.Length);
        foreach (char ch in value)
        {
            if (ch == '\\' || ch == '(' || ch == ')')
            {
                sb.Append('\\').Append(ch);
            }
            else if (ch == '₹')
            {
                sb.Append("Rs ");
            }
            else if (ch >= 32 && ch <= 126)
            {
                sb.Append(ch);
            }
            else if (ch == '\t')
            {
                sb.Append(' ');
            }
            else if (ch == '–' || ch == '—' || ch == '−')
            {
                sb.Append('-');
            }
            else if (ch == '’' || ch == '‘')
            {
                sb.Append('\'');
            }
            else if (ch == '“' || ch == '”')
            {
                sb.Append('"');
            }
            else
            {
                sb.Append('?');
            }
        }

        return sb.ToString();
    }
}
