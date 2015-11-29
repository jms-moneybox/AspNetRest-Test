using Moneybox.Transactions.Interface;
using Moneybox.Transactions.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Moneybox.Web.Api.Controllers
{
    public class TransactionsController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            if(transactionService == null) { throw new ArgumentNullException("transactionService"); }
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
         
        public void Post([FromBody]Transaction transaction)
        {
            _transactionService.Create(transaction);
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
