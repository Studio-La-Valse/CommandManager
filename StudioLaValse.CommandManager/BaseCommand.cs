namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// The base classs for implementing undoable commands.
    /// </summary>
    public abstract class BaseCommand
    {
        /// <summary>
        /// Perform the specified action associated with the command.
        /// </summary>
        public abstract void Do();
        /// <summary>
        /// Perform the opposite of the speficied action.
        /// </summary>
        public abstract void Undo();
    }
}
