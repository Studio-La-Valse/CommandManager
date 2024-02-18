namespace StudioLaValse.CommandManager.Tests
{
    [TestClass]
    public class LazyTransactionTests
    {
        [TestMethod]
        public void TestLazyTransaction()
        {
            var circle = new TestCircle()
            {
                Radius = 50,
                X = 50,
                Y = 50
            };

            var commandManager = CommandManager.CreateLazy();
            using (var transaction = commandManager.OpenTransaction("Test"))
            {
                var command = new SimpleCommand<TestCircle>(
                    (c) => c.Radius = 100, 
                    (c) => c.Radius = 50, 
                    circle);
                transaction.Enqueue(command);

                //The transaction is lazy, so the radius must not have changed.
                Assert.AreEqual(circle.Radius, 50);
            }

            Assert.AreEqual(circle.Radius, 100);

            commandManager.Undo();
            Assert.AreEqual(circle.Radius, 50);

            commandManager.Redo();
            Assert.AreEqual(circle.Radius, 100);

            using (var transaction = commandManager.OpenTransaction("Test"))
            {
                var command = new SimpleCommand<TestCircle>(
                    (c) => c.X = 100, 
                    (c) => c.X = 50, 
                    circle);
                transaction.Enqueue(command);

                command = new SimpleCommand<TestCircle>(
                    (c) => c.Y = 100, 
                    (c) => c.Y = 50, 
                    circle);
                transaction.Enqueue(command);
            }

            Assert.AreEqual(circle.X, 100);
            Assert.AreEqual(circle.Y, 100);

            commandManager.Undo();
            Assert.AreEqual(circle.X, 50);
            Assert.AreEqual(circle.Y, 50);

            commandManager.Redo();
            Assert.AreEqual(circle.X, 100);
            Assert.AreEqual(circle.Y, 100);

            commandManager.Undo();
            Assert.AreEqual(circle.X, 50);
            Assert.AreEqual(circle.Y, 50);

            commandManager.Undo();
            Assert.AreEqual(circle.Radius, 50);
        }

        [TestMethod]
        public void TestLazyTransaction2()
        {
            var circle = new TestCircle()
            {
                Radius = 50,
                X = 50,
                Y = 50
            };

            var commandManager = CommandManager.CreateLazy();
            using (var transaction = commandManager.OpenTransaction("Test"))
            {
                var command = new SimpleCommand<TestCircle>(
                    (c) => c.Radius = 100, 
                    (c) => c.Radius = 50, 
                    circle);
                transaction.Enqueue(command);
            }

            commandManager.Undo();

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