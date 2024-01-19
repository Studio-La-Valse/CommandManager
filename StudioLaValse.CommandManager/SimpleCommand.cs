namespace StudioLaValse.CommandManager
{
    /// <summary>
    /// An implementation for the <see cref="BaseCommand"/> class.
    /// </summary>
    public class SimpleCommand : BaseCommand
    {
        private readonly Action _do;
        private readonly Action undo;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="_do"></param>
        /// <param name="undo"></param>
        public SimpleCommand(Action _do, Action undo)
        {
            this._do = _do;
            this.undo = undo;
        }

        /// <inheritdoc/>
        public override void Do()
        {
            _do();
        }

        /// <inheritdoc/>
        public override void Undo()
        {
            undo();
        }
    }

    /// <summary>
    /// An implementation for the <see cref="BaseCommand"/> class.
    /// </summary>
    public class SimpleCommand<TElement> : BaseCommand
    {
        private readonly Action<TElement> @do;
        private readonly Action<TElement> undo;
        private readonly TElement element;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="_do"></param>
        /// <param name="undo"></param>
        /// <param name="element"></param>
        public SimpleCommand(Action<TElement> _do, Action<TElement> undo, TElement element)
        {
            @do = _do;
            this.undo = undo;
            this.element = element;
        }

        /// <inheritdoc/>
        public override void Do()
        {
            @do(element);
        }

        /// <inheritdoc/>
        public override void Undo()
        {
            undo(element);
        }
    }
}
