namespace StudioLaValse.CommandManager.Private
{
    internal class GreedyCommandManager : BaseCommandManager
    {
        public GreedyCommandManager()
        {

        }


        public override ITransaction OpenTransaction(string name)
        {
            if (TryGetOpenTransaction(out _))
            {
                throw new InvalidOperationException("Another transaction is still open.");
            }

            openTransaction = new GreedyTransaction(this, name);
            return openTransaction;
        }
    }
}
