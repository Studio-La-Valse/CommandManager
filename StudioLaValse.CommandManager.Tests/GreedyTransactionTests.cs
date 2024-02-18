namespace StudioLaValse.CommandManager.Tests
{
    [TestClass]
    public class GreedyTransactionTests
    {
        [TestMethod]
        public void TestGreedyTransaction()
        {
            var circle = new TestCircle()
            {
                Radius = 50,
                X = 50,
                Y = 50
            };

            var commandManager = CommandManager.CreateGreedy();
            using (var transaction = commandManager.OpenTransaction("Test"))
            {
                var command = new SimpleCommand<TestCircle>(
                    (c) => c.Radius = 100,
                    (c) => c.Radius = 50,
                    circle);
                transaction.Enqueue(command);

                Assert.AreEqual(circle.Radius, 100);

                transaction.RollBack();

                Assert.AreEqual(circle.Radius, 50);

                transaction.Enqueue(command);
            }

            Assert.AreEqual(circle.Radius, 100);

            commandManager.Undo();

            Assert.AreEqual(circle.Radius, 50);

            using (var transaction = commandManager.OpenTransaction("Test"))
            {
                var command = new SimpleCommand<TestCircle>(
                    (c) => c.X = 100,
                    (c) => c.X = 50,
                    circle);
                transaction.Enqueue(command);
            }

            Assert.ThrowsException<InvalidOperationException>(commandManager.Redo);

            commandManager.Undo();
            Assert.AreEqual(circle.X, 50);
            Assert.AreEqual(circle.Radius, 50);

            Assert.ThrowsException<InvalidOperationException>(commandManager.Undo);
        }
    }
}