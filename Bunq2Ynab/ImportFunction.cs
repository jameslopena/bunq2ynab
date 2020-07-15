using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Bunq2Ynab
{
    public static class ImportFunction
    {

        static BunqClient bunq = new BunqClient();
        static YnabClient ynab = new YnabClient();

        [FunctionName("ImportFunction")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                var transactions = bunq.GetTransactions();
                var response = ynab.SendTransactions(transactions);
                log.Info($"Sent {response.Data.TransactionIds.Count} transactions. Skipped {response.Data.DuplicateImportIds?.Count}");
            }
            catch (Exception e)
            {
                log.Info($"Error {e.Message}");
                throw;
            }
        }
    }
}
