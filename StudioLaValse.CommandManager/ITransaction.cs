namespace StudioLaValse.CommandManager
{
    public interface ITransaction : IDisposable
    {
        string Name { get; }
        void Enqueue(BaseCommand command);
        void Commit();
        void RollBack();
    }
}