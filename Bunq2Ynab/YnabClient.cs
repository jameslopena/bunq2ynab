using System;
using System.Collections.Generic;
using System.Linq;
using YNAB.Dotnet.Api;
using YNAB.Dotnet.Client;
using YNAB.Dotnet.Model;

namespace Bunq2Ynab
{
    internal class YnabClient
    {

        TransactionsApi apiInstance;
        AccountsApi accountsInstance;

        public YnabClient()
        {
            Configuration.ApiKey.Add("Authorization", Settings.YnabApiKey);
            Configuration.ApiKeyPrefix.Add("Authorization", "Bearer");
            apiInstance = new TransactionsApi();
            accountsInstance = new AccountsApi();
        }

        public SaveTransactionsResponse SendTransactions(List<TransactionModel> transactions)
        {
            var budgetId = new Guid(Settings.YnabBudgetGuid);

            var accounts = accountsInstance.GetAccounts(budgetId);
            var bunqAccountGuid = accounts.Data.Accounts.FirstOrDefault(a => a.Note?.Equals("#bunqimport", StringComparison.InvariantCultureIgnoreCase) == true)?.Id;
            if (bunqAccountGuid == null)
                throw new Exception("Bunq account not found.");

            var saveTransactions = new SaveTransactionsWrapper();
            saveTransactions.Transactions = transactions.Select(t => new SaveTransaction()
            {

                AccountId = bunqAccountGuid,
                Date = t.TransactionDate,
                Amount = (int)(t.Amount * 1000),
                PayeeName = t.Payee,
                ImportId = $"BunqTransaction:{t.Id}",
                Memo = t.Description
            }).ToList();

            return apiInstance.CreateTransaction(budgetId, saveTransactions);
        }
    }
}
