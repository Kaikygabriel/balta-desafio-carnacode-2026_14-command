using src.Commands;
using src.Interfaces;

namespace src.Editors;

public class TextEditor
{
    private string _content;
    private string _selectedText;
    private int _cursorPosition;

    public TextEditor()
    {
        _content = "";
        _cursorPosition = 0;
    }

    public ICommand InsertText(string text)
    {
        var command = new InsertCommand(_content, _selectedText, _cursorPosition,this);
        command.InsertText(text);
        return command;
    }

    public ICommand DeleteText(int length)
    {
        var command = new DeleteTextCommand(_content, _selectedText, _cursorPosition,this);
        command.DeleteText(length);
        return command; 
    }

    public ICommand SetBold(int start, int length)
    {
            
        var command = new BoldCommand(_content, _selectedText, _cursorPosition,this);
        command.SetBold(start,length);
        return command; 

    }

    public void RemoveBold(int start, int length)
    {
        Console.WriteLine($"[Editor] Removendo negrito de {start} a {start + length}");
    }

    public void SetCursorPosition(int position)
    {
        _cursorPosition = position;
    }

    public string GetContent()
    {
        return _content;
    }

    public int GetCursorPosition()
    {
        return _cursorPosition;
    }
}
