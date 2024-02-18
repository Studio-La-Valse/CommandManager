namespace StudioLaValse.CommandManager.Tests
{
    [TestClass]
    public class CommandManagerTests
    {
        [TestMethod]
        public void TestCommandManager()
        {
            var commandManager = CommandManager.CreateLazy();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                commandManager.ThrowIfNoTransactionOpen();
            });

            using (var transaction = commandManager.OpenTransaction("Test"))
            {
                commandManager.ThrowIfNoTransactionOpen();

                Assert.ThrowsException<InvalidOperationException>(() =>
                {
                    commandManager.OpenTransaction("Fail");
                });
            }
        }
    }
}