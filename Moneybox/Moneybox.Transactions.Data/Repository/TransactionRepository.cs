using Moneybox.Transactions.Service.Interface;
using System;
using System.Collections.Generic;
using Dapper;
using DapperExtensions;
using System.Linq;

namespace Moneybox.Transactions.Data.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionsDbContext _context;

        public TransactionRepository(TransactionsDbContext context)
        {
            _context = context;
        }

        public IList<Model.Transaction> All()
        {
            return _context.DbConnection.GetList<Model.Transaction>().ToList();
        }

        public void Delete(long transactionId)
        {
            _context.DbConnection.Execute("DELETE FROM [Transaction] WHERE TransactionId = " + transactionId);
        }

        public Model.Transaction GetById(long transactionId)
        {
            return _context.DbConnection.Get<Model.Transaction>(transactionId);
        }

        public void Insert(Model.Transaction transaction)
        {
            //_context.DbConnection.Insert(transaction);
            transaction.TransactionId = _context.DbConnection.Query<long>(
                @"INSERT INTO [Transaction] 
            ( TransactionDate, Description,TransactionAmount,CreatedDate,ModifiedDate,CurrencyCode,Merchant) VALUES 
            ( @TransactionDate, @Description,@TransactionAmount,@CreatedDate,@ModifiedDate,@CurrencyCode,@Merchant );
            select last_insert_rowid()", transaction).First();
        }

        public void Update(Model.Transaction transaction)
        {
            //_context.DbConnection.Update<Model.Transaction>(transaction);
            _context.DbConnection.Execute(@"UPDATE [Transaction] 
              SET
            TransactionDate = @TransactionDate, 
            Description = @Description,
            TransactionAmount = @TransactionAmount,
            CreatedDate = @CreatedDate,
            ModifiedDate = @ModifiedDate,
            CurrencyCode = @CurrencyCode,
            Merchant = @Merchant
            WHERE TransactionId = @transactionId
            ", transaction);
        }
    }


}
