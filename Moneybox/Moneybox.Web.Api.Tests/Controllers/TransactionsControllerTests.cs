using Moneybox.Transactions.Service.Interface;
using Moq;
using Moneybox.Transactions.Interface;
using Moneybox.Transactions.Service;
using Moneybox.Transactions.Model;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace Moneybox.Web.Api.Tests.Controllers
{
    [TestClass]
    public class TransactionsControllerTests
    {
        private Mock<ITransactionRepository> _transactionRepositoryMock;
        private ITransactionService _transactionService;

        [TestInitialize]
        public void SetUp()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object);
        }

        #region Get Tests

        [TestMethod]
        public void Get_ReturnsListOfTransactions()
        {
            // arrange
            var allTransactions = new List<Transaction>(){
                new Transaction
                {
                    TransactionId = 1
                },
                new Transaction
                {
                    TransactionId = 2
                },
            };

            _transactionRepositoryMock.Setup(x => x.All()).Returns(allTransactions);

            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            var result = subject.Get();

            // assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(allTransactions, result.ToList());
            _transactionRepositoryMock.Verify(x => x.All(), Times.Once);
        }

        [TestMethod]
        public void GetById_ReturnsMatchingTransaction()
        {
            // arrange
            var allTransactions = new List<Transaction>(){
                new Transaction
                {
                    TransactionId = 1
                },
                new Transaction
                {
                    TransactionId = 2
                },
            };

            _transactionRepositoryMock.Setup(x => x.GetById(It.IsAny<long>()))
                .Returns<long>((id) => allTransactions.FirstOrDefault(t=>t.TransactionId == id));

            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            var result = subject.Get(2);

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2,result.TransactionId);
            _transactionRepositoryMock.Verify(x => x.GetById(2), Times.Once);
            _transactionRepositoryMock.Verify(x => x.All(), Times.Never);
        }


        #endregion

        #region Post Tests
        [TestMethod]
        public void Post_NewTransaction_InsertsTransaction()
        {
            // arrange
            var newTransaction = new Transaction
            {
                CurrencyCode = "GBP",
                TransactionAmount = 54.99M,
                CreatedDate = DateTime.UtcNow
            };

            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            subject.Post(newTransaction);

            // assert
            _transactionRepositoryMock.Verify(x => x.Insert(newTransaction), Times.Once);

        }

        #endregion

        #region Post Tests
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Post_ExistingTransaction_ThrowsException()
        {
            // arrange
            var existingTransaction = new Transaction
            {
                TransactionId = 1
            };

            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            subject.Post(existingTransaction);

            // assert
            Assert.IsTrue(false, "exception should be thrown");

        }

        #endregion

        #region Put Tests

        [TestMethod]
        public void Post_ExistingTansaction_UpdatesTransaction()
        {
            // arrange
            var existingTransaction = new Transaction
            {
                TransactionId = 2,
                CurrencyCode = "GBP",
                TransactionAmount = 54.99M,
                CreatedDate = DateTime.UtcNow
            };

            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            subject.Put(existingTransaction.TransactionId, existingTransaction);

            // assert
            _transactionRepositoryMock.Verify(x => x.Update(existingTransaction), Times.Once);
        }


        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void Post_MismatchingId_ThrowsException()
        {
            // arrange
            var existingTransaction = new Transaction
            {
                TransactionId = 2,
                CurrencyCode = "GBP",
                TransactionAmount = 54.99M,
                CreatedDate = DateTime.UtcNow
            };

            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            subject.Put(99, existingTransaction);

            // assert 
            Assert.IsTrue(false, "exception should be thrown");
        }



        #endregion

        #region Delete Tests

        [TestMethod]
        public void Delete_Id_DeletesRecord()
        {
            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            subject.Delete(101);

            // assert 
            _transactionRepositoryMock.Verify(x => x.Delete(101), Times.Once);
        }


        [TestMethod,ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Delete_NoId_ThrowsException()
        {
            var subject = new Api.Controllers.TransactionsController(_transactionService);

            // act
            subject.Delete(0);

            // assert
            Assert.IsTrue(false, "exception should be thrown");
        }

        #endregion


    }
}
