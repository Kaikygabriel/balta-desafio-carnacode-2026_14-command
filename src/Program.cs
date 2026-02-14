using src.Editors;

Console.WriteLine("=== Editor de Texto - Problema de Undo/Redo ===\n");

var app = new EditorApplication();

Console.WriteLine("=== Operações ===");
app.TypeText("Hello");
app.TypeText(" World");
app.ShowContent();

app.DeleteCharacters(6); // Deletar " World"
app.ShowContent();

app.MakeBold(0, 5); // Negrito em "Hello"

Console.WriteLine("\n=== Tentando Desfazer ===");
app.Undo(); 
app.Undo();