using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.CommandManager
{
    public interface ICommandManager
    {
        ITransaction OpenTransaction(string name);
        bool TryGetOpenTransaction([NotNullWhen(true)] out ITransaction? transaction);
        void Commit();
        void Undo();
        void Redo();
    }
}