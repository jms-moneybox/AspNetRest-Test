using System;

namespace Moneybox.Transactions.Model
{
    public class Transaction
    {
        public long TransactionId { get; set; }

        /// <summary>
        /// The date the transaction was executed
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Describes the transaction (optional)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The monetary value of the transaction
        /// </summary>
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// The date the record was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// The date the record was modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// The currency of the TransactionAmount
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// The name of the merchant (optional)
        /// </summary>
        public string Merchant { get; set; }
    }
}
