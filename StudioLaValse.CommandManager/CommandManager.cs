using StudioLaValse.CommandManager.Private;

namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// A factory class to create different types of command managers.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// Create a lazy <see cref="ICommandManager"/>. Commands that are enqueued to the <see cref="ITransaction"/> queue are executed on commit.
        /// <see cref="ITransaction.RollBack"/> function is only supported after the transaction has been commit and functions as an undo method. 
        /// /// </summary>
        /// <returns></returns>
        public static ICommandManager CreateLazy() =>
            new LazyCommandManager();

        /// <summary>
        /// Create a lazy <see cref="ICommandManager"/>. Commands that are enqueued to the <see cref="ITransaction"/> queue are executed immediately. 
        /// <see cref="ITransaction.RollBack"/> function reverts all executed commands prior to the transaction commit.
        /// </summary>
        /// <returns></returns>
        public static ICommandManager CreateGreedy() =>
            new GreedyCommandManager();
    }
}
