using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Dapper;
using DapperExtensions.Mapper;
using Moneybox.Transactions.Model;

namespace Moneybox.Transactions.Data
{
    public class TransactionsDbContext
    {
        public SQLiteConnection DbConnection { get; set; }

        public TransactionsDbContext(string dbName)
        {
            CreateAndOpenDb(dbName);
        }

        private void CreateAndOpenDb(string dbName)
        {
            string dbFilePath = string.Format("c:/temp/{0}.sqlite", dbName);
            var mustSeedDatabase = false;

            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                mustSeedDatabase = true;
            }

            DbConnection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", dbFilePath));

            DbConnection.Open();

            if (mustSeedDatabase)
            {
                SeedDatabase();
            }
        }

        private void SeedDatabase()
        {
            // sql lite doesnt seem to like bigint with identity columns
            DbConnection.Execute(@"
CREATE TABLE [Transaction](
        TransactionId     [integer] PRIMARY KEY AUTOINCREMENT,
        TransactionDate   [datetime] NOT NULL,
        Description       [nvarchar](255) NULL,
        TransactionAmount [decimal] NOT NULL,
        CreatedDate       [datetime] NOT NULL,
        ModifiedDate      [datetime] NOT NULL,
        CurrencyCode      [nvarchar](255) NOT NULL,
        Merchant          [nvarchar](255) NOT NULL
        
)
");
        }


    }


    public class TransactionMapper : ClassMapper<Transaction>
    {
        public TransactionMapper()
        {
            Table("Transaction");
            Map(f => f.TransactionId).Key(KeyType.Identity);
        }
    }
}
