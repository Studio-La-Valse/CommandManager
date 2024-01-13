using StudioLaValse.CommandManager.Private;

namespace StudioLaValse.CommandManager
{
    public static class CommandManagerExtensions
    {
        public static ICommandManager OnCommitDo(this ICommandManager commandManager, Action action)
        {
            return new CommandManagerWithCallback(commandManager, action);
        }

        public static ITransaction ThrowIfNoTransactionOpen(this ICommandManager commandManager)
        {
            if (commandManager.TryGetOpenTransaction(out var transaction))
            {
                return transaction;
            }

            throw new Exception("No transacion open.");
        }
    }
}
