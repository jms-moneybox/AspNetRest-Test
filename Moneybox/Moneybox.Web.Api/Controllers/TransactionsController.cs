using Moneybox.Transactions.Data.Repository;
using Moneybox.Transactions.Interface;
using Moneybox.Transactions.Model;
using Moneybox.Transactions.Service;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Moneybox.Web.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController():this(new TransactionService(new TransactionRepository(new Transactions.Data.TransactionsDbContext("moneybox"))))
        {
            // todo : remove this once dependency injection is set up            
        }

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
                 
        public IEnumerable<Transaction> Get()
        {
            return _transactionService.GetAll();
        }
         
        public Transaction Get(long id)
        {
            return _transactionService.GetById(id);
        }
         
        public Transaction Post([FromBody]Transaction transaction)
        {
            _transactionService.Create(transaction);
            return transaction;
        }
         
        public void Put(long id, [FromBody]Transaction transaction)
        {
            if(id != transaction.TransactionId)
            {
                throw new ArgumentException("TransactionId does not match");
            }

            _transactionService.Update(transaction);
        }
         
        public void Delete(long id)
        {
             _transactionService.Delete(id);
        }
        
    }
}
