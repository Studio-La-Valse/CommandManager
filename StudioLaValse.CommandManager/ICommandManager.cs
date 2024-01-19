using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// The command manager interface.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Open a new transaction.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if another transaction is still open.</exception>
        ITransaction OpenTransaction(string name);
        /// <summary>
        /// Try get the currently open transaction.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>True if there is currently a transaction open.</returns>
        bool TryGetOpenTransaction([NotNullWhen(true)] out ITransaction? transaction);
        /// <summary>
        /// Commit the currently open transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if no transaction is currently open.</exception>
        void Commit();
        /// <summary>
        /// Undo the last committed transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if there is currently still a transaction open.</exception>
        void Undo();
        /// <summary>
        /// Redo the last undone transaction.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if there is currently still a transaction open.</exception>
        void Redo();
    }
}