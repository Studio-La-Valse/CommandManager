namespace StudioLaValse.CommandManager
{
    public class SimpleCommand : BaseCommand
    {
        private readonly Action _do;
        private readonly Action undo;

        public SimpleCommand(Action _do, Action undo)
        {
            this._do = _do;
            this.undo = undo;
        }

        public override void Do()
        {
            _do();
        }

        public override void Undo()
        {
            undo();
        }
    }
}
