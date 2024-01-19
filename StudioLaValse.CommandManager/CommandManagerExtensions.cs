using StudioLaValse.CommandManager.Private;

namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// Extensions for the <see cref="ICommandManager"/> interface.
    /// </summary>
    public static class CommandManagerExtensions
    {
        /// <summary>
        /// Specify an action which will be executed directly after committing a transaction.
        /// </summary>
        /// <param name="commandManager"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ICommandManager OnCommitDo(this ICommandManager commandManager, Action action)
        {
            return new CommandManagerWithCallback(commandManager, action);
        }

        /// <summary>
        /// Get the currently open transaction. 
        /// </summary>
        /// <param name="commandManager"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Thrown if there is no transaction open.</exception>
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
