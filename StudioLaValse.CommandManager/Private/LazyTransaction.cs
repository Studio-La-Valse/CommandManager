namespace StudioLaValse.CommandManager.Private
{
    internal class LazyTransaction : BaseTransaction
    {
        private readonly List<BaseCommand> commandsToExecute;
        private readonly Stack<BaseCommand> finishedCommands;


        public LazyTransaction(BaseCommandManager commandManager, string name) : base(commandManager, name)
        {
            commandsToExecute = new List<BaseCommand>();
            finishedCommands = new Stack<BaseCommand>();
        }

        public override void Enqueue(BaseCommand command)
        {
            commandsToExecute.Insert(0, command);
        }

        public override void Commit()
        {
            finishedCommands.Clear();

            while (commandsToExecute.Count != 0)
            {
                var command = commandsToExecute[0];
                command.Do();
                commandsToExecute.RemoveAt(0);

                finishedCommands.Push(command);
            }
        }

        public override void RollBack()
        {
            commandsToExecute.Clear();

            while (finishedCommands.Count != 0)
            {
                var undoCommand = finishedCommands.Pop();
                undoCommand.Undo();

                commandsToExecute.Add(undoCommand);
            }
        }
    }
}
