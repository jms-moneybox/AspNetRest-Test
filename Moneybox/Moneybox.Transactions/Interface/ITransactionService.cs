using Moneybox.Transactions.Model;
using System.Collections;
using System.Collections.Generic;

namespace Moneybox.Transactions.Interface
{
    public interface ITransactionService
    {
        /// <summary>
        /// Creates and persists a new transaction record
        /// </summary>
        void Create(Transaction transaction);

        /// <summary>
        /// Returns a transaction for the given id or null if no matching transaction exists
        /// </summary>
        Transaction GetById(long transactionId);

        /// <summary>
        /// Returns all transactions
        /// </summary>
        /// <returns></returns>
        IList<Transaction> GetAll();

        /// <summary>
        /// Updates the existing transaction record
        /// </summary> 
        void Update(Transaction transaction);

        /// <summary>
        /// The id of the transaction record to delete
        /// </summary> 
        void Delete(long transactionId);
    }
}
