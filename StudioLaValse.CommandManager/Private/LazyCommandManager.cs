namespace StudioLaValse.CommandManager.Private
{
    internal class LazyCommandManager : BaseCommandManager
    {
        public LazyCommandManager()
        {

        }


        public override ITransaction OpenTransaction(string name)
        {
            if (TryGetOpenTransaction(out _))
            {
                throw new InvalidOperationException("Another transaction is still open.");
            }

            openTransaction = new LazyTransaction(this, name);
            return openTransaction;
        }
    }
}
