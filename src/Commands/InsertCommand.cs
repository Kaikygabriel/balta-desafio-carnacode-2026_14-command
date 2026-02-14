using src.Editors;
using ICommand = src.Interfaces.ICommand;

namespace src.Commands;

public sealed class InsertCommand : ICommand
{
    private string _content;
    private string _selectedText;
    private int _cursorPosition;
    private TextEditor _editor;
    private string _text;
    
    public InsertCommand(string content, string selectedText, int cursorPosition, TextEditor editor)
    {
        _content = content;
        _selectedText = selectedText;
        _cursorPosition = cursorPosition;
        _editor = editor;
    }

    public void InsertText(string text)
    {
        _cursorPosition += text.Length;
        Console.WriteLine($"[Editor] Texto inserido: '{text}'");
        Console.WriteLine($"[Editor] Conteúdo atual: '{_content}'");
        _text = text;
    }
    
    public void Fazer()
    {
        InsertText(_text);
    }

    public void Desfazer()
    {
        _editor.SetCursorPosition(_cursorPosition + _text.Length);
        _editor.DeleteText(_text.Length);
    }
}