using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.CommandManager.Private
{
    internal abstract class BaseCommandManager : ICommandManager
    {
        protected BaseTransaction? openTransaction;
        private readonly Stack<BaseTransaction> finishedTransactions;
        private readonly Stack<BaseTransaction> undoneTransactions;

        public BaseCommandManager()
        {
            finishedTransactions = new Stack<BaseTransaction>();
            undoneTransactions = new Stack<BaseTransaction>();
        }

        public abstract ITransaction OpenTransaction(string name);

        public bool TryGetOpenTransaction([NotNullWhen(true)] out ITransaction? transaction)
        {
            transaction = openTransaction;
            return transaction is not null;
        }

        public void Commit()
        {
            if (openTransaction is null)
            {
                throw new InvalidOperationException("A transaction has not been opened yet.");
            }

            try
            {
                openTransaction.Commit();
                undoneTransactions.Clear();
                finishedTransactions.Push(openTransaction);
            }
            catch
            {
                openTransaction.RollBack();
                throw;
            }
            finally
            {
                openTransaction = null;
            }
        }

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
