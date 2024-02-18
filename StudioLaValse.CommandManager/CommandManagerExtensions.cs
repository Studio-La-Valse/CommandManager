namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// Extensions for the <see cref="ICommandManager"/> interface.
    /// </summary>
    public static class CommandManagerExtensions
    {
        /// <summary>
        /// Get the currently open transaction. 
        /// </summary>
        /// <param name="commandManager"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if there is no transaction open.</exception>
        public static ITransaction ThrowIfNoTransactionOpen(this ICommandManager commandManager)
        {
            if (commandManager.TryGetOpenTransaction(out var transaction))
            {
                return transaction;
            }

            throw new InvalidOperationException("No transacion open.");
        }
    }
}
