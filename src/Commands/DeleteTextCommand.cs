using src.Editors;
using ICommand = src.Interfaces.ICommand;

namespace src.Commands;

public sealed class DeleteTextCommand : ICommand
{
    private string _content;
    private string _selectedText;
    private int _cursorPosition;
    private TextEditor _editor;
    private int _lenght;
    private string _text;
    
    public DeleteTextCommand(string content, string selectedText, int cursorPosition, TextEditor editor)
    {
        _content = content;
        _selectedText = selectedText;
        _cursorPosition = cursorPosition;
        _editor = editor;
    }

    public void DeleteText(int length)
    {
        if (_cursorPosition >= length)
        {
            _text = _content.Substring(_cursorPosition - length,length);
            _content = _content.Remove(_cursorPosition - length, length);
            _cursorPosition -= length;
            Console.WriteLine($"[Editor] {length} caracteres deletados");
            Console.WriteLine($"[Editor] Conteúdo atual: '{_content}'");
        }    
    }
    
    public void Fazer()
    {
        DeleteText(_lenght);
    }

    public void Desfazer()
    {
        _editor.SetCursorPosition(_cursorPosition);
        _editor.InsertText(_text);
    }
}