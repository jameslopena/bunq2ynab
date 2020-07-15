using Bunq.Sdk.Context;
using Bunq.Sdk.Model.Generated.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bunq2Ynab
{
    internal class BunqClient
    {
        public BunqClient()
        {
            var apiContext = ApiContext.Create(ApiEnvironmentType.PRODUCTION, Settings.BunqApiKey, "Bunq2Ynab");
            BunqContext.LoadApiContext(apiContext);
        }

        public List<TransactionModel> GetTransactions()
        {
            var payments = Payment.List(int.Parse(Settings.MoneyTaryAccountId));
            return payments.Value.Select(p => new TransactionModel(p.Id.ToString(), DateTime.Parse(p.Created).ToUniversalTime(), p.CounterpartyAlias?.LabelMonetaryAccount?.DisplayName, decimal.Parse(p.Amount.Value), p.Description?.Trim())).ToList();
        }
    }
}
