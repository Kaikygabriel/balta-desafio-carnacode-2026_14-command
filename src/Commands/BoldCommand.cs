using src.Editors;
using src.Interfaces;

namespace src.Commands;

public sealed class BoldCommand  : ICommand
{
    private string _content;
    private string _selectedText;
    private int _cursorPosition;
    private TextEditor _editor;
    private int _start;
    private int _lenght;
    
    public BoldCommand(string content, string selectedText, int cursorPosition, TextEditor editor)
    {
        _content = content;
        _selectedText = selectedText;
        _cursorPosition = cursorPosition;
        _editor = editor;
    }
    
    public void SetBold(int start, int length)
    {
        Console.WriteLine($"[Editor] Aplicando negrito de {start} a {start + length}");
        _start = start;
        _lenght = length;
    }
    public void Fazer()
    {
        SetBold(_start, _lenght);
    }

    public void Desfazer()
    {
        Console.WriteLine($"[Editor] Removendo negrito de {_start} a {_start + _lenght}");
    }
}