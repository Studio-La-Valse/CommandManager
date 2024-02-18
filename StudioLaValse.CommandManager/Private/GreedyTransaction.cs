namespace StudioLaValse.CommandManager.Private
{
    internal class GreedyTransaction : BaseTransaction
    {
        private readonly List<BaseCommand> commandsToExecute;
        private readonly Stack<BaseCommand> finishedCommands;


        public GreedyTransaction(BaseCommandManager commandManager, string name) : base(commandManager, name)
        {
            commandsToExecute = new List<BaseCommand>();
            finishedCommands = new Stack<BaseCommand>();
        }

        public override void Enqueue(BaseCommand command)
        {
            command.Do();
            finishedCommands.Push(command);
        }

        public override void Commit()
        {
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
            while (finishedCommands.Count != 0)
            {
                var undoCommand = finishedCommands.Pop();
                undoCommand.Undo();

                commandsToExecute.Add(undoCommand);
            }
        }
    }
}
