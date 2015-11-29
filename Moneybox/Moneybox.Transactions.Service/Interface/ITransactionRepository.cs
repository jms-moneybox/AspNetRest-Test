using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moneybox.Transactions.Model;

namespace Moneybox.Transactions.Service.Interface
{
    public interface ITransactionRepository
    {
        void Update(Transaction transaction);
        Transaction GetById(long transactionId);
        IList<Transaction> All();
        void Delete(long transactionId);
        void Insert(Transaction transaction);
    }
}
