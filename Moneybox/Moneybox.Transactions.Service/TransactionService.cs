using System;
using System.Collections.Generic;
using Moneybox.Transactions.Interface;
using Moneybox.Transactions.Model;
using Moneybox.Transactions.Service.Interface;

namespace Moneybox.Transactions.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public void Create(Transaction transaction)
        {
            if (transaction.TransactionId != 0) {
                throw new ArgumentOutOfRangeException("TransactionId", "Must be 0");
            }

            // can do validation here if needed

            _transactionRepository.Insert(transaction);
        }

        public void Delete(long transactionId)
        {
            if (transactionId < 1)
            {
                throw new ArgumentOutOfRangeException("TransactionId", "Must be greater than 0");
            }

            _transactionRepository.Delete(transactionId);
        }

        public IList<Transaction> GetAll()
        {
            return _transactionRepository.All();
        }

        public Transaction GetById(long transactionId)
        {
            return _transactionRepository.GetById(transactionId);
        }

        public void Update(Transaction transaction)
        {
            if (transaction.TransactionId < 1) {
                throw new ArgumentOutOfRangeException("TransactionId", "Must be greater than 0");
            }

            _transactionRepository.Update(transaction);
        }
    }
}
