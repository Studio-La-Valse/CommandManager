using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.CommandManager.Private
{
    internal class CommandManagerWithCallback : ICommandManager
    {
        private readonly ICommandManager source;
        private readonly Action action;

        public CommandManagerWithCallback(ICommandManager source, Action action)
        {
            this.source = source;
            this.action = action;
        }


        public void Undo()
        {
            source.Undo();
            action();
        }

        public void Redo()
        {
            source.Redo();
            action();
        }

        public ITransaction OpenTransaction(string name)
        {
            return source.OpenTransaction(name);
        }

        public bool TryGetOpenTransaction([NotNullWhen(true)] out ITransaction? transaction)
        {
            return source.TryGetOpenTransaction(out transaction);
        }

        public void Commit()
        {
            source.Commit();
            action();
        }
    }
}
