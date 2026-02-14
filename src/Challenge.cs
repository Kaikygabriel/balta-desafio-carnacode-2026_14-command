/*
// DESAFIO: Editor de Texto com Undo/Redo
// PROBLEMA: Um editor de texto precisa implementar operações de desfazer/refazer para
// múltiplas ações (digitar, deletar, formatar). O código atual chama métodos diretamente,
// tornando impossível desfazer operações ou implementar histórico de comandos

using System;
using System.Collections.Generic;
using System.Windows.Input;
using src.Commands;
using ICommand = src.Interfaces.ICommand;

namespace DesignPatternChallenge
{
    // Contexto: Editor de texto que precisa registrar e reverter operações
    // Usuário espera poder desfazer/refazer qualquer ação
    

    // Problema: Aplicação chama métodos diretamente, sem forma de desfazer
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
            _commands.LastOrDefault()?.Desfazer(); 
        }

        public void Redo()
        {
            _commands.LastOrDefault()?.Fazer();
        }

        public void ShowContent()
        {
            Console.WriteLine($"\n=== Conteúdo do Editor ===");
            Console.WriteLine($"'{_editor.GetContent()}'");
            Console.WriteLine($"Cursor na posição: {_editor.GetCursorPosition()}\n");
        }
    }

    // Tentativa ingênua: Guardar estados completos
    public class EditorWithStateHistory
    {
        private TextEditor _editor;
        private Stack<string> _contentHistory;

        public EditorWithStateHistory()
        {
            _editor = new TextEditor();
            _contentHistory = new Stack<string>();
        }

        public void TypeText(string text)
        {
            // Problema: Guardar estado completo consome muita memória
            _contentHistory.Push(_editor.GetContent());
            _editor.InsertText(text);
        }

        public void DeleteCharacters(int count)
        {
            _contentHistory.Push(_editor.GetContent());
            _editor.DeleteText(count);
        }

        public void Undo()
        {
            if (_contentHistory.Count > 0)
            {
                // Problema: Restaurar estado completo é ineficiente
                // Problema: Perde informações como posição do cursor
                string previousContent = _contentHistory.Pop();
                Console.WriteLine($"[Undo] Restaurando estado anterior");
                // Como restaurar? _editor é privado e não tem setter
            }
        }

        // Problema: Redo é ainda mais complicado - precisa de outra pilha
    }

    // Tentativa com registro de operações (sem pattern adequado)
    public class Operation
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public int Position { get; set; }
        public int Length { get; set; }
    }

    public class EditorWithOperationLog
    {
        private TextEditor _editor;
        private Stack<Operation> _operations;

        public EditorWithOperationLog()
        {
            _editor = new TextEditor();
            _operations = new Stack<Operation>();
        }

        public void TypeText(string text)
        {
            _operations.Push(new Operation 
            { 
                Type = "Insert", 
                Text = text,
                Position = _editor.GetCursorPosition()
            });
            _editor.InsertText(text);
        }

        public void Undo()
        {
            if (_operations.Count > 0)
            {
                var op = _operations.Pop();
                
                // Problema: Switch case gigante para cada tipo de operação
                switch (op.Type)
                {
                    case "Insert":
                        // Reverter inserção = deletar
                        _editor.SetCursorPosition(op.Position + op.Text.Length);
                        _editor.DeleteText(op.Text.Length);
                        break;
                    case "Delete":
                        // Reverter deleção = inserir de volta
                        _editor.SetCursorPosition(op.Position);
                        _editor.InsertText(op.Text);
                        break;
                    case "Bold":
                        // Reverter formatação
                        _editor.RemoveBold(op.Position, op.Length);
                        break;
                    // Adicionar novo tipo = modificar este switch
                }
            }
        }

        // Problema: Lógica de desfazer está acoplada à aplicação
        // Problema: Cada novo comando requer modificar o switch
    }

    class Program
    {
        static void Main(string[] args)
        {
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
            app.Undo(); // Não funciona!

            Console.WriteLine("\n=== PROBLEMAS ===");
            Console.WriteLine("✗ Impossível desfazer operações - não há histórico");
            Console.WriteLine("✗ Operações executadas diretamente sem encapsulamento");
            Console.WriteLine("✗ Não dá para parametrizar, enfileirar ou registrar operações");
            Console.WriteLine("✗ Difícil implementar macros (sequência de comandos)");
            Console.WriteLine("✗ Não há separação entre invocação e execução");
            Console.WriteLine("✗ Cada operação precisa saber como se reverter");
            Console.WriteLine("✗ Lógica de desfazer/refazer acoplada à aplicação");

            Console.WriteLine("\n=== Requisitos Não Atendidos ===");
            Console.WriteLine("• Undo/Redo de qualquer operação");
            Console.WriteLine("• Histórico de operações");
            Console.WriteLine("• Macros (executar múltiplos comandos de uma vez)");
            Console.WriteLine("• Comandos parametrizáveis");
            Console.WriteLine("• Log de auditoria");
            Console.WriteLine("• Transações (executar tudo ou nada)");

            // Perguntas para reflexão:
            // - Como encapsular operações como objetos?
            // - Como parametrizar, enfileirar e registrar requisições?
            // - Como implementar operações reversíveis (undo)?
            // - Como desacoplar emissor do comando de quem o executa?
        }
    }
}
*/
