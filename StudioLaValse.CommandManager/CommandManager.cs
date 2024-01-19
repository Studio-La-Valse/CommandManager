using System.Diagnostics.CodeAnalysis;
using StudioLaValse.CommandManager.Private;

namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// The default implementation for the <see cref="ICommandManager"/> interface.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        private readonly Stack<ITransaction> finishedTransactions;
        private readonly Stack<ITransaction> undoneTransactions;


        private CommandManager()
        {
            finishedTransactions = new Stack<ITransaction>();
            undoneTransactions = new Stack<ITransaction>();
        }
        /// <summary>
        /// Create a default <see cref="ICommandManager"/>
        /// </summary>
        /// <returns></returns>
        public static ICommandManager Create() =>
            new CommandManager();


        private ITransaction? openTransaction;

        /// <inheritdoc/>
        public ITransaction OpenTransaction(string name)
        {
            if (TryGetOpenTransaction(out _))
            {
                throw new InvalidOperationException("Another transaction is still open.");
            }

            openTransaction = new Transaction(this, name);
            return openTransaction;
        }

        /// <inheritdoc/>
        public bool TryGetOpenTransaction([NotNullWhen(true)] out ITransaction? transaction)
        {
            transaction = openTransaction;
            return transaction is not null;
        }

        /// <inheritdoc/>
        public void Commit()
        {
            if (!TryGetOpenTransaction(out var transaction))
            {
                throw new InvalidOperationException("A transaction has not been opened yet.");
            }

            try
            {
                transaction.Commit();
                undoneTransactions.Clear();
                finishedTransactions.Push(transaction);
            }
            catch
            {
                transaction.RollBack();
                throw;
            }
            finally
            {
                openTransaction = null;
            }
        }

        /// <inheritdoc/>
        public void Undo()
        {
            if (TryGetOpenTransaction(out _))
            {
                throw new InvalidOperationException("Another transaction is still open.");
            }

            if (finishedTransactions.Count == 0)
            {
                throw new InvalidOperationException("Nothing to undo");
            }

            try
            {
                var transaction = finishedTransactions.Pop();
                transaction.RollBack();

                undoneTransactions.Push(transaction);
            }
            catch
            {
                finishedTransactions.Clear();
                undoneTransactions.Clear();
                throw;
            }
            finally
            {

            }
        }

        /// <inheritdoc/>
        public void Redo()
        {
            if (TryGetOpenTransaction(out _))
            {
                throw new InvalidOperationException("Another transaction is still open.");
            }

            if (undoneTransactions.Count == 0)
            {
                throw new InvalidOperationException("Nothing to redo");
            }

            var transaction = undoneTransactions.Pop();

            try
            {
                transaction.Commit();
                finishedTransactions.Push(transaction);
            }
            catch
            {
                transaction.RollBack();
                undoneTransactions.Clear();
                throw;
            }
            finally
            {

            }
        }
    }
}
