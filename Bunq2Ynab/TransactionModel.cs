using System;
using System.Linq;

namespace Bunq2Ynab
{
    public class TransactionModel
    {
        public string Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public TransactionModel(string id, DateTime transactionDate, string payee, decimal amount, string description)
        {
            Id = id;
            TransactionDate = transactionDate;
            Payee = ConvertPayee(payee);
            Amount = amount;
            Description = description;
        }

        private string ConvertPayee(string payee)
        {
            foreach (var payeeTransform in Settings.PayeeTransforms.Split('|'))
            {
                var x = payeeTransform.Split(',');
                if (payee.StartsWith(x.First(),StringComparison.InvariantCultureIgnoreCase))
                {
                    return x.Last();
                }
            }
            
            return payee;
        }
    }
}
