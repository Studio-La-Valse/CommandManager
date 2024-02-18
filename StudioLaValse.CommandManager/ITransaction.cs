namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// The transaction interface.
    /// </summary>
    public interface ITransaction : IDisposable
    {
        /// <summary>
        /// The name of the transaction.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Enqueue a new undoable command.
        /// </summary>
        /// <param name="command"></param>
        void Enqueue(BaseCommand command);
        /// <summary>
        /// Roll back the transaction.
        /// </summary>
        void RollBack();
    }
}