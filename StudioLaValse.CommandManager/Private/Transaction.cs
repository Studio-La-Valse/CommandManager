namespace StudioLaValse.CommandManager.Private
{
    internal class Transaction : ITransaction
    {
        private readonly List<BaseCommand> commandsToExecute;
        private readonly Stack<BaseCommand> finishedCommands;
        private readonly ICommandManager commandManager;

        public string Name { get; }

        public Transaction(ICommandManager commandManager, string name)
        {
            commandsToExecute = new List<BaseCommand>();
            finishedCommands = new Stack<BaseCommand>();

            this.commandManager = commandManager;
            
            Name = name;
        }

        public void Enqueue(BaseCommand command)
        {
            commandsToExecute.Insert(0, command);
        }

        public void Commit()
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

        public void RollBack()
        {
            while (finishedCommands.Count != 0)
            {
                var undoCommand = finishedCommands.Pop();
                undoCommand.Undo();

                commandsToExecute.Add(undoCommand);
            }
        }

        public void Dispose()
        {
            commandManager.Commit();
        }
    }
}
