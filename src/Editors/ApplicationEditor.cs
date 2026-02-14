using src.Interfaces;

namespace src.Editors;
public class EditorApplication
{
    private List<ICommand> _commands = new();
    private TextEditor _editor;

    public EditorApplication()
    {
        _editor = new TextEditor();
    }

    public void TypeText(string text)
    {
        var command = _editor.InsertText(text);
        _commands.Add(command);
    }

    public void DeleteCharacters(int count)
    {
        // Problema: Não há registro do que foi deletado
        // Como restaurar o texto deletado?
        var command = _editor.DeleteText(count);
        _commands.Add(command);

    }

    public void MakeBold(int start, int length)
    {
        // Problema: Como reverter esta formatação?
        var command=   _editor.SetBold(start, length);
        _commands.Add(command);

    }

    // Problema: Como implementar Undo/Redo sem refatorar tudo?
    public void Undo()
    {
        Console.WriteLine("Passou");
        var command = _commands.LastOrDefault();
        if (command is null)
        {
            Console.WriteLine("n passou");
            return;
        }
        command.Desfazer();
        _commands.Remove(command);
    }

    public void Redo()
    {
        var command = _commands.LastOrDefault();
        if (command is not null)
        {
            command.Fazer();
            _commands.Remove(command);
        }
    }

    public void ShowContent()
    {
        Console.WriteLine($"\n=== Conteúdo do Editor ===");
        Console.WriteLine($"'{_editor.GetContent()}'");
        Console.WriteLine($"Cursor na posição: {_editor.GetCursorPosition()}\n");
    }
}
