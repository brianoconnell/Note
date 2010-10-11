using System;
using Moq;
using NHibernate;
using NUnit.Framework;
using Ninject;
namespace Note.Core.Test
{
    [TestFixture]
    public class CommandInvokerTests
    {
        [Test]
        public void HandlerIsInvokedWhenCommandIsSent()
        {
            var kernel = new StandardKernel();

            var mockCommandHandler = new Mock<ICommandHandler<string>>();
            Mock<ISession> mockSession = CreateMockSession();
            kernel.Bind<ICommandHandler<string>>().ToConstant(mockCommandHandler.Object);


            var commandInvoker = new CommandInvoker(kernel, mockSession.Object);
            commandInvoker.Execute("nothing");

            mockCommandHandler.Verify(x => x.Handle("nothing"), Times.Once());
        }

        [Test]
        public void TransactionIsCommittedWhenCommandIsSent()
        {
            var kernel = new StandardKernel();

            var mockCommandHandler = new Mock<ICommandHandler<string>>();
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();
            mockSession.Setup(x => x.BeginTransaction()).Returns(mockTransaction.Object);
            kernel.Bind<ICommandHandler<string>>().ToConstant(mockCommandHandler.Object);


            var commandInvoker = new CommandInvoker(kernel, mockSession.Object);
            commandInvoker.Execute("nothing");

            mockTransaction.Verify(x => x.Commit(), Times.Once());
        }

        [Test]
        public void TransactionIsRolledBackWhenExceptionOccurs()
        {
            var kernel = new StandardKernel();

            var mockCommandHandler = new Mock<ICommandHandler<string>>();
            mockCommandHandler.Setup(x => x.Handle("nothing")).Throws<Exception>();
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();
            mockSession.Setup(x => x.BeginTransaction()).Returns(mockTransaction.Object);
            kernel.Bind<ICommandHandler<string>>().ToConstant(mockCommandHandler.Object);


            var commandInvoker = new CommandInvoker(kernel, mockSession.Object);
            commandInvoker.Execute("nothing");

            mockTransaction.Verify(x => x.Rollback(), Times.Once());
        }

        private static Mock<ISession> CreateMockSession()
        {
            var mockSession = new Mock<ISession>();
            var mockTransaction = new Mock<ITransaction>();
            mockSession.Setup(x => x.BeginTransaction()).Returns(mockTransaction.Object);
            return mockSession;
        }
    }
}