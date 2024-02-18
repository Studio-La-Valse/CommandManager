namespace StudioLaValse.CommandManager.Private
{
    internal abstract class BaseTransaction : ITransaction
    {
        private readonly BaseCommandManager commandManager;

        public BaseTransaction(BaseCommandManager commandManager, string name)
        {
            this.commandManager = commandManager;
            Name = name;
        }

        public string Name { get; }

        public abstract void Enqueue(BaseCommand command);

        public abstract void RollBack();

        public abstract void Commit();

        public void Dispose()
        {
            commandManager.Commit();
        }
    }
}
