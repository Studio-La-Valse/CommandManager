namespace StudioLaValse.CommandManager.Private
{
    internal class Transaction : ITransaction
    {
        private readonly Queue<BaseCommand> commandsToExecute;
        private readonly Stack<BaseCommand> finishedCommands;
        private readonly ICommandManager commandManager;

        public string Name { get; }

        public Transaction(ICommandManager commandManager, string name)
        {
            commandsToExecute = new Queue<BaseCommand>();
            finishedCommands = new Stack<BaseCommand>();
            this.commandManager = commandManager;
            Name = name;
        }

        public void Enqueue(BaseCommand command)
        {
            commandsToExecute.Enqueue(command);
        }

        public void Commit()
        {
            finishedCommands.Clear();

            while (commandsToExecute.Count != 0)
            {
                var command = commandsToExecute.Dequeue();
                command.Do();

                finishedCommands.Push(command);
            }
        }

        public void RollBack()
        {
            while (finishedCommands.Count != 0)
            {
                var undoCommand = finishedCommands.Pop();
                undoCommand.Undo();
            }
        }

        public void Dispose()
        {
            commandManager.Commit();
        }
    }
}
